using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {
	//reference to other scripts
	
	//referencia a gameManager para, de momento, coger el material 'on mouse over' y tambien el gameStatus
	public GameManager gameManager;	
	public MouseController mouseController;
	//reference to mouse cursor to change style (initialized on start)
	//public GUITexture gameCursor;

	//object parameters
	public string [] onUseText;
	public string [] onMouseOverText;
	public float interactionDistance = 5;
	
	private Material originalMaterial; 
	//private Texture originalTexture;
	  
	private GameObject player; 

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		//get reference to player for distance checks
		player = GameObject.Find("CustomFPSController");

		//gameCursor = GameObject.Find("GamePointer").GetComponent<GUITexture>();
		//Debug.Log (gameCursor.name);

		//save the std material and texture to restore after mouse over
		originalMaterial = GetComponent<MeshRenderer>().material;
		//originalTexture = originalMaterial.mainTexture;
	}


	// Update is called once per frame
	void Update () {
		if (gameManager.gameStatus != Enums.GameStatus.Exploring)
		{
			LeaveObject();
			//Debug.Log ("soltando objeto");
		}
	
	}
	
	
	
	public void OnMouseEnter (){
	
		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)
			return;


		HighlightObject();
		//message to show
		Debug.Log (onMouseOverText[0]+ " a " +DistanceToPlayer () + " metros");		

	}
	
	public void OnMouseExit (){
		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)
			return;
		//Debug.Log ("mouse exit");
		LeaveObject();
		//faltaria el restaurar mouse
	}
	
	public void OnMouseDown (){
		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)
			return;

		Debug.Log (onUseText[0]);

	}


	public float DistanceToPlayer(){
		Vector3 distance = transform.position - player.transform.position;
		return distance.magnitude;
	}


	//object and cursor backs to standar shader/texture/whatever
	void LeaveObject(){
		renderer.material = originalMaterial;
		mouseController.currentMouseColor = mouseController.mouseStdColor;

		//gameCursor.color = gameManager.mouseStdColor;
		//Debug.Log ("restauro material");
	}

	////highlight object and cursor (and, in the future, change the cursor depending on the object type)
	void HighlightObject(){
		renderer.material = gameManager.mouseOverMaterial;
		mouseController.currentMouseColor = mouseController.mouseOverObjectColor;
		//gameCursor.color = gameManager.mouseOverObjectColor;
	}
	
}
