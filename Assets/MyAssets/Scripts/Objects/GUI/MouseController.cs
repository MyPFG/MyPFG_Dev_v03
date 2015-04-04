using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	public GameManager gameManager;
	//public GUITexture mouseSTD;
	// Use this for initialization


	//mouse cursor parameters
	public Color mouseOverObjectColor = Color.green;
	public Color mouseStdColor = Color.white;

	public Texture currentCursor;
	public Texture defaultCursor;
	//public GUITexture currentCursor;
	//public GUITexture defaultCursor;
	[HideInInspector] public Color currentMouseColor = Color.white;


	private bool showPointer;

	void Start () {
		//Screen.showCursor = false;

		//probably not needed
		//defaultCursor = guiTexture.texture;
		//currentCursor = defaultCursor;
//		Debug.Log (" Cursor hidden");
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (Input.mousePosition);
//		Debug.Log (currentCursor.name);
		//Debug.Log (gameObject.name);
/*
		//if not explorign (and probably not navigating) then hide mouse and exit mouse controller
		if (gameManager.gameStatus != Enums.GameStatus.Exploring){
			guiTexture.enabled = false;
			return;
		}


		guiTexture.enabled = true;

		//get mouse position
		Vector2 cursorPos = Input.mousePosition;

		//put mouse cursor in mouse position
//		guiTexture.pixelInset.x = pos.x;
		//Rect pos =guiTexture.pixelInset;
		//	pos.x = cursorPos.x;
		//pos.y = cursorPos.y;
		//pos.position.x = cursorPos.x;
		//pos.position.y = cursorPos.y;


		Rect pos = new Rect (cursorPos.x, cursorPos.y-32, guiTexture.pixelInset.width, guiTexture.pixelInset.height);
		//Rect pos = new Rect (-500, -50, 32, 32);



		guiTexture.pixelInset = pos;
		//Debug.Log ("mouse position#"+pos);
*/
	}

//	void OnGUI(){
	public void DrawMouseCursor(){
		//if not exploring (and probably not navigating) then hide mouse and exit mouse controller
		if (gameManager.gameStatus != Enums.GameStatus.Exploring 
		  &&gameManager.gameStatus != Enums.GameStatus.Inventory )
			return;
		Vector2 cursorPos = Input.mousePosition;
		GUI.color = currentMouseColor;
		//Rect pos = new Rect (cursorPos.x, cursorPos.y-32, 32f,32f);
		Rect pos = new Rect (cursorPos.x, Screen.height-cursorPos.y,32f,32f);
		GUI.DrawTexture(pos, currentCursor);
		//currentCursor.transform.position = new Vector3 (


	}

 	public void DefaultTexture (bool useDefault){
		if (useDefault)
			currentMouseColor = mouseStdColor;
		else
			currentMouseColor = mouseOverObjectColor;
	}


	public void SwapCursor( Texture newCursor){
		gameObject.guiTexture.texture = newCursor;
		currentCursor = newCursor;
		gameObject.guiTexture.enabled = false;

		//or maybe just 'currentCursor = newCursor'
		Debug.Log ("mouse changed to " + newCursor.name);
	}

	public void ResetCursor (){
//		gameObject.guiTexture.texture = defaultCursor;  //is this really needed? $%&
		currentCursor = defaultCursor;
	}
}


