using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	//references to other scripts
	private GameManager gameManager;




	//visible references to external objects
	//public Camera mainCamera;
	//public Camera inventoryBackCamera;
	public GameObject playerController;
	public Camera mainCamera;
	public Camera inventoryCamera;
	public Camera examinationCamera;
	public GUITexture sceneShade;
	public GameObject objectUnderExamination;
	//private visible references to external objects


	//private vars
	//private int startPos = 140p
	private int inventoryEdge = 2;  //dimension of the inventory layour (real value es edge+1
	private int iconSize = 64;//90;
	private int inventoryOffset = 15;

//	private int xInitial = Screen.width/2;
//	private int yInitial = Screen.height/2;



	//private static int maxItems = 50;
	//private int inventoryOccupation = 0;
	//public GameObject [] currentInventoryObjects = new GameObject[maxItems];
	ArrayList currentInventoryObjects = new ArrayList();







	// Use this for initialization
	void Start () {


		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		//inventoryBackCamera = GameObject.Find ("CameraInventoryBCK").GetComponent<Camera>();
		mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		examinationCamera = GameObject.Find ("ExaminationCamera").GetComponent<Camera>();
		//playerController = GameObject.Find ("CustomFPSController").GetComponent<PlayerController>();
		playerController = GameObject.Find ("CustomFPSController");
		inventoryCamera = GameObject.Find ("InventoryCamera").GetComponent<Camera>();
		sceneShade = GameObject.Find ("SceneShade").GetComponent<GUITexture>();

		//inventoryBackCamera.enabled = false;
		//inventoryCamera.enabled = false;	
	
		//load all 2d objects into the inventory array (for testing purpose)
		GameObject [] iObjects = GameObject.FindGameObjectsWithTag ("InventoryObject");

		foreach (GameObject o in iObjects){
			if (o.GetComponent<ItemV2>().currentState==1){   //add only objects with current state 'in inventory' (remember:this is 'OnStart')
				this.AddItem (o,false);
			}
		}
		//OrderItems();
		//Debug.Log (iObjects);
	}



	// Update is called once per frame
	void Update () {
	
	}


	//interface layer
	public void ToggleInventoryLayers(){
//		inventoryBackCamera.enabled = status;

if (gameManager.gameStatus == Enums.GameStatus.Inventory)
	gameManager.gameStatus = Enums.GameStatus.Exploring;
else
	gameManager.gameStatus = Enums.GameStatus.Inventory;


		gameManager.statusInventory = !gameManager.statusInventory;

		inventoryCamera.enabled = gameManager.statusInventory;

		gameManager.TogglePlayerControl(!gameManager.statusInventory);
/*
		playerController.GetComponent<CharacterMotor>().enabled = !gameManager.statusInventory;
		playerController.GetComponent<MouseLookADV>().enabled = !gameManager.statusInventory;
		playerController.GetComponent<FPSInputController>().enabled = !gameManager.statusInventory;
		mainCamera.GetComponent<MouseLookADV>().enabled = !gameManager.statusInventory;
		sceneShade.enabled = gameManager.statusInventory;;
*/
		if (gameManager.statusInventory == true){ //this should be used to extend the 'exit inventory clicking over scene shader'
			//Debug.Log ("true");
			//gameManager.statusInventory = true;
			//gameManager.gameStatus = Enums.GameStatus.Inventory;

		} else{
			//Debug.Log ("false");
			//gameManager.statusInventory = false;
			//gameManager.gameStatus = Enums.GameStatus.Exploring;
		}

/*
		if (gameManager.gameStatus == Enums.GameStatus.Inventory){
			gameManager.statusInventory = false;
			gameManager.gameStatus = Enums.GameStatus.Exploring;
		} else{
			gameManager.statusInventory = true;
			gameManager.gameStatus = Enums.GameStatus.Inventory;
		}
*/
/*
		if (status == true){
			//playerController.GetComponent<CharacterMotor>.enabled = false;
			playerController.GetComponent<CharacterMotor>().enabled = false;
			playerController.GetComponent<MouseLookADV>().enabled = false;
			playerController.GetComponent<FPSInputController>().enabled = false;
			mainCamera.GetComponent<MouseLookADV>().enabled = false;

			Debug.Log ("inventory false");
		} else{
			playerController.GetComponent<CharacterMotor>().enabled = true;
			playerController.GetComponent<MouseLookADV>().enabled = true;
			playerController.GetComponent<FPSInputController>().enabled = true;
			mainCamera.GetComponent<MouseLookADV>().enabled = true;
			Debug.Log ("inventory TRUE");
		}
*/
	}


	//////Add one item to the current inventory objects array
	public void AddItem(GameObject item, bool alreadyInInventory){
		//Debug.Log ("Adding " + item.name + " to inventory");
		//if (!alreadyInInventory)
		//	item.GetComponent<ItemV2>().positionInInventory = currentInventoryObjects.Count;//arraylist version
			//item.GetComponent<ItemV2>().positionInInventory = inventoryOccupation; //non arraylist version

		//item.GetComponent<ItemV2>().positionInInventory = currentInventoryObjects.Count;
		currentInventoryObjects.Add(item);

		OrderItems ();
		//currentInventoryObjects[item.GetComponent<ItemV2>().positionInInventory] = item; //not arraylist version

		//inventoryOccupation ++;//not arraylist version

	}

	public void  RemoveItem (GameObject item){
		//Debug.Log ("Removing " + item.name + " from inventory");
		currentInventoryObjects.RemoveAt (item.GetComponent<ItemV2>().positionInInventory);
		//inventoryOccupation --;//not arraylist version
		OrderItems ();

	}


	public	void OrderItems(){  //old InventoryGrid (to arrange items over the background image)
		int xPosition = Screen.width /2 - iconSize*(inventoryEdge+1)/2 - inventoryOffset*(inventoryEdge+1)/2;// -iconSize/2;
		int yAnchor = Screen.height /2 + iconSize*(inventoryEdge)/2 - iconSize/2;// + inventoryOffset;// +iconSize/2;
		int yPosition = yAnchor;
	//	int xPosition = -startPos - iconSize/2;
	//	int offset = startPos - iconSize/2;
		//int itemsNumber = currentInventoryObjects.Length;
		int tempX = 0;
		int tempY = 0;
		//for (int i = 0; i < inventoryOccupation; i++){ ////not arraylist version
		for (int i = 0; i < currentInventoryObjects.Count; i++){
			Rect r = new Rect (xPosition,yPosition,iconSize, iconSize);
//arraylist version
			GameObject itemPointer =(GameObject) currentInventoryObjects[i];
			itemPointer.guiTexture.pixelInset = r;
			itemPointer.GetComponent<ItemV2>().positionInInventory = i;


			//currentInventoryObjects.Insert (itemTemp.GetComponent<ItemV2>().positionInInventory,itemTemp);
			//currentInventoryObjects[i] = r;


/////arrraylist version end
		  //currentInventoryObjects[i].guiTexture.pixelInset = r; //not arraylist
			//Debug.Log ("prcessed:" +currentInventoryObjects[i]);

			if (tempY < inventoryEdge){
				tempY ++;
				yPosition = yPosition - inventoryOffset - iconSize;
			} else{
				tempY = 0;
				tempX ++;
				yPosition = yAnchor;
				xPosition = xPosition + iconSize + inventoryOffset;

			}
		}



	}
}
