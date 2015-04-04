using UnityEngine;
using System.Collections;




public class ItemStatusManager : MonoBehaviour {
	//references to external scripts
	MouseController mouseController;
	ItemV2 itemV2;  //to update the item state when changes (remeber, one object is ItemV2 + ItemStatusManager)
	public GameManager gameManager;

	//object states
	public string[] statesNames;
	public string[] state0;
	public string[] state1;
	public string[] state2;
	public string[] state3;
	public string[] state4;
	public string[] state5;
	public int currentState; //current state (public for debug purpose) $%&deprecated?

	//message for each state (an array of messages for each one)
	public string[] repliesState0;
	public string[] repliesState1;
	public string[] repliesState2;
	public string[] repliesState3;
	public string[] repliesState4;
	public string[] repliesState5;

	public string[] genericReplies; //one generic reply for state

	//to store the states fromm the exposed in the inspector and process them secuentially. Array of arrais (i is state, j is 'arrows from one state'
	string [][] stateArray = new string[5][];
	string [][] replyArray = new string[5][]; //same as stateArray but for replies






	void Start () {
		mouseController = GameObject.Find("MouseManager").GetComponent<MouseController>();
		itemV2 = GetComponent <ItemV2>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();

		//load states in stateArray for easy parsing
		stateArray[0] = state0;
		stateArray[1] = state1;
		stateArray[2] = state2;

		stateArray[3] = state3;
		stateArray[4] = state4;


		//load replies in replyArray for easy parsing
		replyArray[0] = repliesState0;
		replyArray[1] = repliesState1;
		replyArray[2] = repliesState2;

		replyArray[3] = repliesState3;
		replyArray[4] = repliesState4;


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){
	

	}
	void OnMouseDown(){
		//not needed now??
/*
		//string [] readString = new string[10];
		string [] readString = new string[stateArray[currentState].Length];

		//parsing states test
		foreach (string str in stateArray[currentState]){
			readString = str.Split(new char[] {','});  //el parametro raro del split es porque la entrada es 'un array de separadores' y no un separador
			//readString = stateArray[currentState].Split("," .Chars[0]);
			print ("current state " + currentState);
			print ("cursor " + readString [0]);
			print ("new state " + readString [1]);

			//and retrieve auxiliary objects if any
			for (int i = 2; i < readString.Length; i = i+2){
				print ("auxiliary object = " + readString[i]);
				print (readString[i] + "'s new state = " + readString[i+1]);
				   
			}
		}
		
		//Debug.Log ("thislog->" + readString);
*/
	}

	public void ProcessState (GameObject inObject, int currentState, string picker){  //my objectLookup
//		Debug.Log ("Object: " + inObject);
//		Debug.Log ("State: " + currentState);
//		Debug.Log ("Picker: " + picker);
		int element = 0;
		string matchCursor = "";
		if (picker == mouseController.defaultCursor.name)
			matchCursor = "default";
		else
			matchCursor = picker;

		bool invalidPicker = false;

		foreach (string contents in stateArray[currentState]){
			//stateArray [currentState] es 'todas las flechitas que salen del current state'.... contents itera por cada una de ellas (que puede tener, admeas, distintas ramificaciones)
			string[] readString = contents.Split(new char[] {','});// readString is an array with all elements from a single transition splitted by ','

			//Debug.Log (readString[0]);  //the expected picker cursor

			if (readString[0] == matchCursor){//0 is always expected cursor name
				//Debug.Log ("Cursor " + matchCursor + " = expected for " + inObject.name);
			    //if matchCursor then do process, go to next state AND exit foreach

				int nextState = int.Parse(readString[1]);
				string message = replyArray [nextState][element];
				itemV2.ChangeState (nextState, message);//this call process current interaction main object
				//and now process auxiliary objects if any. They will be in pairs 'aux object name ' + 'object next state' from
				//2 to readString.length

				if (readString.Length > 2){  //then there are at least one auxiliary object
					for (int i = 2; i < readString.Length-1; i = i+2){ //to loop through each pair obejct-nextstate
						string auxObjectName = readString[i];
						int auxObjectNextState = int.Parse (readString[i+1]);

						GameObject auxObject = GameObject.Find(auxObjectName);
						auxObject.GetComponent<ItemV2>().ChangeState (auxObjectNextState,"");
						//auxObject.GetComponent<itemV2>().ChangeState(auxObjectNextState,"");
/*
						Debug.Log ("--new aux object found--");
						Debug.Log ("  aux object data");
						Debug.Log (readString[i] + "##" + readString[i+1]);
						Debug.Log ("
*/
					}
				}


				/* this better on itemV2.ChangeState
				gameManager.actionMsg = replyArray[currentState][element];
				string message = replyArray [currentState][element]; //as said above
				gameManager.actionMsg = message;

				///adjust show action text show time delay  $%&THIS IS EXPERIMENTAL && NOT PROPERLY TESTED
				float timeToAdd = message.Length/20.0f + 1.5f;

				if (timeToAdd >= 0)
					itemV2.showActionTextDelay = timeToAdd;
				*/



			}
			else
				//Debug.Log ("no cursor match for " + inObject.name);
				//HandleNoMatchReplies(picker); //change to show in the interface and only when no match at all and NOT once per no-match
				invalidPicker = true;

			//so far so good this only works if each state has only ONE arrow
		}

		if (invalidPicker){
			HandleNoMatchReplies(picker);
		}


	}

	void HandleNoMatchReplies(string picker){
		Debug.Log ("The " + picker +" does not seem to affect the " + GetComponent<ItemV2>().currentName);
	}
}

