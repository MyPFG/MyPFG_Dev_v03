using UnityEngine;
using System.Collections;

public class ItemV2 : MonoBehaviour {
	public int initialState = 0; //item initial state
	public int currentState = 0;  //probably should be hidden once everything is tested

	public float interactionDistance = 5;

	//references to external (scripts and objects)
	GameManager gameManager;
	MouseController mouseController;
	public GameObject player;
	ItemStatusManager itemStatusManager;
	InventoryManager inventoryManager;
	public MainGUIManager mainGUIManager;

	//miscellanious parameters
	public GameObject aniObject;//para el caso de que la animacion no este en este objeto sino en uno de sus hijos si fuera un esqueleto, por ejemplo). Si es un hijo estara en esta var
	public bool is3D = true; //false means is an inventory objet (2d representation of an item for the inventory)
	public int positionInInventory = -1;

	bool useTexture;
	//metadata
	public int [] location;
	public int []  visibility;
	public string [] itemName;    //name for the object in the current state
	public string [] description; //description for the object in the current state
	public float showActionTextDelay = 2.0f;

	public AnimationClip [] animationClip; //animation clip to play when the item is picked (by state)
	public float [] animationDelay; //to sync animations
	public AudioClip []  soundClip; //sound to play when item picked
	public float [] audioDelay; //as animationDelay
	public AnimationClip []  loopAnimation; //animation to be played and loop after main animation (is an idle)
	public AudioClip []  loopSoundFX; //sund effect for loopAnimation
	public bool postLoop = false; // true means the object have idle animation
	public bool animates = false; //true; // false = object have no animation at all
	
	//vars for track current state of the object. 'i state of arrays' (i = current)

	int currentLocation = 0;
	int currentVisibility = 0;
	public string currentName; //current short description  //hide this in inspector $%&
	string currentDescrp; //current long description
	AudioClip currentSound;
	float currentAudioDelay = 0f;
	AnimationClip currentAnimationClip;
	float currentAnimationDelay = 0f;
	float currentAniLength; //to calculate delays
	//AnimationClip currentLoopAnimation;
	//AudioClip currentLoopSound;
	
	//vars for mouse functinallity
	private bool picked  = false;  //to prevent 'mouse over'
	private bool mouseDown; //to know if mouse is down
	private bool processing; //to suspend mouse over functionallities
	private Material originalMaterial;


	//vars for internal process
	//timer
	//timer1 for audio delay
	private bool timer1= false;
	private float timeLimit1;
	Texture aoTexture;

	//timer2 for animation (if animating then disable mouse functionallity)
	private bool timer2  = false;
	private float timeLimit2;


	private float soundFXVolume;


	//vars for 2d items, inventory and visibility management
	public int previousState; //for tracking the item previous state to know required action and new visibility (table 11-1 g 536)
	private int element = 0;  //item index when in inventory



	public void Start(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		player = GameObject.Find("CustomFPSController");
		mouseController = GameObject.Find("MouseManager").GetComponent<MouseController>();
		itemStatusManager = GetComponent <ItemStatusManager>();
		mainGUIManager = GameObject.Find("GameManager").GetComponent<MainGUIManager>();
		inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();

		soundFXVolume = gameManager.soundFXVolume;

		//load initial status and texts to 'currents'
		currentState = initialState;
		currentName = itemName [currentState];
		currentDescrp = description[currentState];

		if (is3D){ //only 3d objects have mesh renderer so only for them script will look and store the original
			originalMaterial = GetComponent<MeshRenderer>().material;//get the original material
		
			if (originalMaterial.mainTexture){ //get original texture if any
				aoTexture = originalMaterial.mainTexture;
				useTexture = true;
			}
			else {
				useTexture = false;
			}
		} else{ //aditionally non 3d objects (inventory items) are set invisible dependin on their initial status
			if (currentState == 0){
				guiTexture.enabled = false;
			}
		}

		if (aniObject == null)
			aniObject = gameObject; //if null (not set in editor) then it will be the actual object



	}

	void Update (){
		//Debug.Log (Time.time);
		//timer1 update
		if (timer1 && Time.time >timeLimit1){
			AudioSource.PlayClipAtPoint (currentSound, transform.position, soundFXVolume); //play delayed sound
			timer1 = false;
		}

		if (timer2 && Time.time > timeLimit2){
			processing = false;
			timer2 = false;
			Debug.Log ("time's up");
			gameManager.showActionMsg = false;
			mainGUIManager.ResetMouseOver();
		}
	}


	void  OnMouseDown(){

		gameManager.showText = false;
		LeaveObject (); //to recover object material if needed

		//check distance && game status before mouse down logic
		if (!ItemActive ())
			return;
	/*	   
		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)  //$%& esto es lo mismo para todos los casos de interaccion; crear una funcion 'CheckInteractiveStatus() true/false
			return;
    */

		//if in inventory mode and not an inventory object (defined by layer) then return;this if should be in ##ItemActive ()## $%&
		if (gameManager.gameStatus ==Enums.GameStatus.Inventory && gameObject.layer != 9)  //$%&igual que la CheckInteractiveStatus
			return;

		//Debug.Log ("mouse down on " + this.name);

		//process item status (pick response to the current cursor) and 

		gameManager.showText = false;
		timeLimit2 = Time.time +showActionTextDelay;//+ 2.0f;
		timer2 = true;
		itemStatusManager.ProcessState (this.gameObject, currentState,mouseController.currentCursor.name); //call to process click-on-object event


		 

		/* OBSOLETO
		//mouse down and object goes to inventory layer
		base.OnMouseDown();	

		gameObject.layer = LayerMask.NameToLayer ("Inventory");
*/
	}

	public void OnMouseEnter (){

		//Debug.Log ("item enter");
		/*
		if (is3D) {
			if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring){
				//Debug.Log ("return 1");
				return;
			}
		}
		*/

		if (!ItemActive ())
			return;

		//Debug.Log ("item process");
	/*	if (gameManager.gameStatus == Enums.GameStatus.Navigating || gameManager.gameStatus ==  Enums.GameStatus.Talking)
			return;

		if (gameManager.gameStatus ==Enums.GameStatus.Inventory && gameObject.layer != 9){
			//Debug.Log ("return 2");
			return;
		}
*/
		//these two lines are not needed if all state changes are managed by the script
		currentName = itemName [currentState];  
		currentDescrp = description [currentState]; 


		gameManager.showText = true;
		gameManager.name = currentName;// itemName[0];
		gameManager.desc = currentDescrp;// description [0];
		//gameManager.desc = description[0];
		HighlightObject();
		//message to show
		//Debug.Log (onMouseOverText[0]+ " a " +DistanceToPlayer () + " metros");		
		
	}
	
	public void OnMouseExit (){
		/*
		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)  //$%& maybe remove this conition and the return
			return;
			*/
		if (!ItemActive ())
			return;
		//Debug.Log ("mouse exit");
		gameManager.showText = false;
		LeaveObject();
		//faltaria el restaurar mouse
	}

	public float DistanceToPlayer(){
		Vector3 distance = transform.position - player.transform.position;
		return distance.magnitude;
	}

	protected void LeaveObject(){
		if (is3D){
			if (gameManager.materialChange)
				renderer.material = originalMaterial;
			if (gameManager.cursorColorChange)
				mouseController.DefaultTexture (true);

			gameManager.showText = false;
		} else {
			//here texture change for 2d objects (back to standar)
		}
	}
	
	////highlight object and cursor (and, in the future, change the cursor depending on the object type)
	void HighlightObject(){
		if (is3D){
			if (gameManager.materialChange)
				renderer.material = gameManager.mouseOverMaterial; //$%&better change ONLY material and keep diffuse texture
			if (gameManager.cursorColorChange)
				mouseController.DefaultTexture (false);
		} else{
			//guiTexture.color = Color.red;
			//here texture change for 2d objects (set highlighted)
		}

	}

	public void ChangeState (int newState, string actionMsg){  //or 'ProcessObject' function
		//for 2d management
		previousState = currentState;


		//set new state in object metadata
		currentState = newState; //first set current state to new
		//and then update all 'current' variables
		currentName =itemName[currentState];
		currentDescrp= description[currentState];
		currentSound = soundClip [currentState];
		currentAudioDelay =audioDelay[currentState];
		currentLocation = location [currentState];

		//then set & show action msg (and set enhanced time delay)
		gameManager.actionMsg = actionMsg;
		gameManager.showActionMsg = true;
		///adjust show action text show time delay  $%&THIS IS EXPERIMENTAL && NOT PROPERLY TESTED
		float timeToAdd = actionMsg.Length/20.0f + 1.5f;		
		if (timeToAdd >= 0)
			showActionTextDelay = timeToAdd;


		//if item is 2d then handle actions
		if (!is3D)
			Handle2DItem();
		else 
			Handle3DItem();
		//and now play the sound if any
		if (currentSound){
			if (currentAudioDelay ==0){ //to put delay if any
				AudioSource.PlayClipAtPoint(currentSound, transform.position, soundFXVolume);
			} else{
				timeLimit1 = Time.time + currentAudioDelay;
				timer1 = true;
			}
		}
		//and now process animations if any
		if (animates){
			currentAnimationClip = animationClip [currentState];
			currentAnimationDelay = animationDelay [currentState];
			WaitNSeconds (currentAnimationDelay); //instead commond yield new waitforseconds from js

			if (aniObject != null){
				Debug.Log ("ANIMATION");
				aniObject.animation.Play (currentAnimationClip.name);
				//currentAniLength = currentAnimationClip.length;

				//wait for the length of the animation and then play loop if any
				WaitNSeconds (currentAnimationDelay);//instead commond yield new waitforseconds from js
				if (postLoop) 
					aniObject.animation.Play (loopAnimation[currentState].name);

			}
		}//if (animates)

	}


	//return true if the item has to react to mouse events for a given general game status (distance to player, gameStatus, object type...)
	protected bool ItemActive(){
		if (is3D) {
			if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring){
				//Debug.Log ("return 1");
				return false;
			}

		}


		return true;
	}


	/*
	 * 	STATE CHANGE AND ACTIONS TO PERFORM GUIDE
	 * previous state --> current state = action
	 * 1 not in scene (0) --> is cursor (2) = put as cursor
	 * 2 not in scene (0) --> in inventory (1) = add to inventory && enable GUITexture
	 * 3 is cursor (2) --> not in scene (0) = reset cursor to default
	 * 4 is cursor (2) --> in inventory (1) = reset cursor && add to inventory && enable GUITexture
	 * 5 in inventory (1) --> not in scene (0) = remove from inventory && disable GUITexture
	 * 6 in inventory (1) -->is cursor (2)  = remove from inventory + disable GUITexture + put as cursor
	 * 
	 */

	void Handle2DItem(){
		// 1
		if (previousState == 0 && currentState ==2){
			mouseController.currentCursor = guiTexture.texture;
		}

		//2
		if (previousState == 0 && currentState ==1){
			inventoryManager.AddItem (gameObject,false);  //or gameManger.AddItem
			gameObject.guiTexture.enabled = true;
		}

		//3 
		if (previousState == 2 && currentState == 0){
			mouseController.ResetCursor();
		}

		//4
		if (previousState ==2 && currentState == 1){
			mouseController.ResetCursor();
			inventoryManager.AddItem(gameObject,true);
			gameObject.guiTexture.enabled = true;

		}

		//5
		if (previousState ==1 && currentState ==0){
			inventoryManager.RemoveItem(gameObject);
			gameObject.guiTexture.enabled = false;
		}

		//6
		if (previousState == 1 && currentState == 2){
			inventoryManager.RemoveItem (gameObject);
			gameObject.guiTexture.enabled = false;
			mouseController.currentCursor = guiTexture.texture;

		}

	}

	void Handle3DItem(){
		if (currentLocation != 0){
			gameObject.SetActive (false);
		}
	}

	IEnumerator WaitNSeconds(float n) //$%& untested
	{
		yield return new WaitForSeconds(n);
	}

}
