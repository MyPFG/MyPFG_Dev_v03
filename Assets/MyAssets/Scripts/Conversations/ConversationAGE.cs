using UnityEngine;
using System.Collections;


//Conversation agent (one per character)
public class ConversationAGE : ItemV2 {
	//references to external
	ConversationCON conversationController;


	//locals
	public  string file; //conversation raw data file WITHOUT extension AND language mark
	ArrayList nodes = new ArrayList(); //all nodes in conversation
	ArrayList arcs = new ArrayList();  //all arcs in conversation

	Node currentNode; //= new Node();
	ArrayList currentArcs; //= new ArrayList();
	int currentNodeId = 1;


	//redefined methods
	void Start () {
		base.Start ();

		string fileNameLang = file + "_SPA.txt";  //to be changed pending on the language parameter in settings
		string fullPath = Application.dataPath + "/MyAssets/RawDAta/Conversations/"+ fileNameLang;

		conversationController = GameObject.Find ("GameManager").GetComponent<ConversationCON>();
		//gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();

		LoadFromFileTXT (fullPath);

		currentNode = GetNodeById(currentNodeId);
		currentArcs = GetArcsFromNode (currentNodeId);



		//Debug.Log ("XX");
	}

	void OnMouseDown(){
		if (!ItemActive ())
			return;

		if (conversationController.gameManager.gameStatus != Enums.GameStatus.Exploring)
			return;

		LeaveObject();


		conversationController.activeConversation = this;
		UpdateGUI ();
		conversationController.EnableInterface();
	}


	//new methods
	//parse and load items from TXT file to conversation struct
	private void LoadFromFileTXT(string _fullPath){
		string line;

		System.IO.StreamReader file = new System.IO.StreamReader(_fullPath);
		line = file.ReadLine();

		while((line = file.ReadLine()) != null && line != "ARCS"){

			string [] tempStr = line.Split ('#');

			Node newNode = new Node (int.Parse(tempStr[0]), tempStr[1], tempStr[2], tempStr[3]);
			nodes.Add (newNode);

		}

		//Debug.Log ("nodes parsed");

		while((line = file.ReadLine()) != null && line != "END"){ 
			                                                         
			                                                          
			string [] tempStr = line.Split ('#');
	
			Arc newArc = new Arc (int.Parse (tempStr[0]),int.Parse(tempStr[1]), tempStr[2]);
			arcs.Add (newArc);

		}

		Debug.Log ("conversation parsed");
		}


	ArrayList GetArcsFromNode (int nodeId){
		//int origin = n.GetId();

		ArrayList arcsTemp = new ArrayList();
		for (int i = 0; i < this.arcs.Count; i++){
			Arc a = (Arc) arcs[i];
			if (a.GetOrigin() == nodeId){
				arcsTemp.Add (a);
			}
		}
		return arcsTemp;
	}

	Node GetNodeById (int nodeId){
		for (int i = 0; i < this.nodes.Count; i++){
			Node n = (Node) this.nodes[i];
			if (n.GetId() == nodeId)
				return n;
		}

		return null;
	}

	void UpdateGUI(){
		//first disable all response buttons
		for (int i = 0; i < ConversationCON.maxResponses; i++){
			//conversationController.responses[i].enbled = false;
			conversationController.buttonsGO[i].SetActive(false);
		}

		//now enable needed buttons and put text
		for (int i = 0; i < this.currentArcs.Count; i++){
			Arc arc = (Arc) currentArcs[i];
			conversationController.buttonsGO[i].SetActive(true);
			conversationController.responses[i].text = arc.GetText();
		}

		//and now the other parameters 
		conversationController.npcText.text = currentNode.GetText();
	}

	public void  ProcessResponseSelection (int response){
		Arc responseSelected = (Arc) currentArcs[response];

		currentNodeId = responseSelected.GetDestiny();
		currentNode = GetNodeById (currentNodeId);
		currentArcs.Clear ();
		currentArcs =GetArcsFromNode (currentNodeId);

		UpdateGUI();
	}


		                                                          	

}
