using UnityEngine;
using System.Collections;
	

public class GameManager : MonoBehaviour {
	//references to other scripts
	public InventoryManager inventoryManager;

	public Material mouseOverMaterial;
	public Color mouseOverColor;
	public bool cursorColorChange = true;
	public bool materialChange = true;



	 //hidden references to external
	GameObject playerController;
	Camera mainCamera;
	////// currentCursor will depend on specific object type and stored there. Changes default<->specific are managed in cursor manager

	//publicas a esconder en el editor
	public Enums.GameStatus gameStatus = Enums.GameStatus.Exploring;
	public bool statusInventory = false;

	//publics for the mainGUIManager script
	public bool showTextSettings = true;  //to disable on settings   //eq useText
	public bool showText = false;        //to disable when navigating
	//vars for show messages
	public bool showDescSettings = true;
	public bool showName = true;  //old 'showDesc'
	public bool showDesc = true;  //old 'showLongDesc'
	public bool showActionMsg= true;
	//and messages preloaded
	public string name = "Name"; //old shortDesc
	public string desc = "Long description"; //old longDesc
	public string actionMsg = "Action message";

	//publics for game sound options (maybe hidden in editor
	public float soundFXVolume = 1.0f;
	public float ambientSoundVolume = 1.0f;
	public float musicVolume = 1.0f;
	public float voiceVolume = 1.0f;  //for the conversation voices






	void Start(){
		//currentMouseTexture = GameObject.Find("DefaultCursor").GetComponent<GUITexture>();
		inventoryManager = GameObject.Find ("GameManager").GetComponent<InventoryManager>();
		playerController = GameObject.Find ("CustomFPSController");
		mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (Application.dataPath);

		if ( Input.GetKeyDown(KeyCode.I)){
			inventoryManager.ToggleInventoryLayers();
		}

		if (gameStatus==Enums.GameStatus.Inventory || gameStatus == Enums.GameStatus.Talking)
			;
		else {
			gameStatus = Enums.GameStatus.Exploring;
		
			if (Input.GetMouseButton (1)||Input.GetButton("Horizontal") || Input.GetButton ("Vertical")){
				gameStatus = Enums.GameStatus.Navigating;
			}

			if (Input.GetMouseButtonUp(1)||Input.GetButtonUp("Horizontal") || Input.GetButtonUp ("Vertical"))
				gameStatus = Enums.GameStatus.Exploring;
		}
/*
		if ( Input.GetKeyDown(KeyCode.I)){
			statusInventory = !statusInventory;
			inventoryManager.ToggleInventoryLayers(statusInventory);
		}


		//inventory opened bypass every other status change
		if (statusInventory)
			gameStatus = Enums.GameStatus.Inventory;
		else {
			gameStatus = Enums.GameStatus.Exploring;
		
			if (Input.GetMouseButton (1)||Input.GetButton("Horizontal") || Input.GetButton ("Vertical")){
				gameStatus = Enums.GameStatus.Navigating;
			}

			if (Input.GetMouseButtonUp(1)||Input.GetButtonUp("Horizontal") || Input.GetButtonUp ("Vertical"))
				gameStatus = Enums.GameStatus.Exploring;
		}
*/
	}


	/////COMMON METHODS
	public void TogglePlayerControl(bool newStat){
		playerController.GetComponent<CharacterMotor>().enabled = newStat;
		playerController.GetComponent<MouseLookADV>().enabled = newStat;
		playerController.GetComponent<FPSInputController>().enabled = newStat;
		mainCamera.GetComponent<MouseLookADV>().enabled = newStat;


	}

}
