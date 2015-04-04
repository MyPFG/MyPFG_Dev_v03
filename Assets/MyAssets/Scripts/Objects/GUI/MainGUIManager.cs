using UnityEngine;
using System.Collections;

public class MainGUIManager : MonoBehaviour {

	//references to other scripts
	public GameManager gameManager; //$%&move from public to Start()
	public MouseController mouseController;
	//styles
	public GUISkin guiSkin;
	public GUIStyle customGUIStyle;

	//auxiliar
	public GUIStyle boxStyleLabel;

	//privates
	private bool resetMO = false;

	void OnGUI(){
		//2nd try
		GUI.skin = guiSkin;
		Rect r; 

		if (gameManager.showTextSettings){  //if text is on in settings
			if (gameManager.showActionMsg){  //temporal show action msg toggle
				r = new Rect (Screen.width/2-300, Screen.height-47,600,35);
				GUI.Label (r, gameManager.actionMsg);
			}

			if (gameManager.showText && !gameManager.showActionMsg){  //is true when 'exploring'ç
				Debug.Log ("showing text");
				if (gameManager.showDescSettings){  //if desc is on in settings
					if (gameManager.showDesc){   // && and check if distance to target is under treshold -->see notes
						r = new Rect (Screen.width/2-250,Screen.height-37, 500,35);
						GUI.Label (r, gameManager.desc);
					}
					if (gameManager.showName){

						r = new Rect (Screen.width/2 - 250, Screen.height - 100, 500, 35);
						GUI.Label (r, gameManager.name, customGUIStyle);
					}
				}

			}
		
		}

		if (resetMO){
			r = new Rect (0,0,Screen.width, Screen.height);
			GUI.Box (r,"", customGUIStyle);
		}


		mouseController.DrawMouseCursor();
		 
		/* 1 st try (deprecated)
		if (!gameManager.showText || !gameManager.showTextSettings)
			return;


		GUI.skin = guiSkin;
		Rect r;
		r = new Rect (Screen.width/2 - 300, Screen.height - 47, 600, 32);
		GUI.Label (r,"very very very very very very very very very very veeeeeeeeeeery long test text");

		if (gameManager.showActionMsg){
			r = new Rect(Screen.width/2 - 250, 10, 500,42);
			GUI.Box (r, gameManager.actionMsg, boxStyleLabel);
		}

		if (gameManager.showName){
			if (resetMO){
				r = new Rect(0,0,Screen.width, Screen.height);
				//GUI.Box (r,"");
			
				Debug.Log ("REseting MO");
			}

			 r = new Rect(Screen.width/2 - 250, Screen.height - 90, 500,42);
			GUI.Label (r, gameManager.name,customGUIStyle);
			//GUI.Label (r, gameManager.name);
		}

		if (gameManager.showDesc && gameManager.showDescSettings){
			r = new Rect(Screen.width/2 - 250, Screen.height-65, 500,42);
			//GUI.Box (r, gameManager.desc);
			GUI.Label (r, gameManager.desc);
			//GUI.Box (r, gameManager.desc, boxStyleLabel);
		}
	*/

		
	}


	public void ResetMouseOver(){
		WaitNSeconds (0.5f);
		resetMO = true;
		StartCoroutine(WaitOneFrame());
		resetMO = false;

	}

	IEnumerator WaitNSeconds(float n)
	{
		yield return new WaitForSeconds(n);
	}

	IEnumerator WaitOneFrame()
	{
		
		//returning 0 will make it wait 1 frame
		yield return 0;
		
		//code goes here
		
		
	}
}
