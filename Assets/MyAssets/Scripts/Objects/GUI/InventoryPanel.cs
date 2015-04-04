using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
	MouseController mouseController;
	InventoryManager inventoryManager;
	void Start(){
		mouseController = GameObject.Find("MouseManager").GetComponent<MouseController>();
		inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
	}

	void OnMouseDown () {
		Debug.Log ("PANEL HIT");

		if (mouseController.currentCursor ==mouseController.defaultCursor)
			return;

		//if not default cursor then is an objec. PUt it back to the inventory
		GameObject currentObject = GameObject.Find(mouseController.currentCursor.name);
		//currentObject.GetComponent<ItemV2>().currentState = 1; // set state 1 = in inventory
		currentObject.GetComponent<ItemV2>().ChangeState(1, "");
		mouseController.ResetCursor(); //back to default cursor
		//inventoryManager.AddItem(currentObject, true); //and put the object in the inventory array



	}

	void OnMouseEnter (){
		//Debug.Log (transform.name + " hit");
	}
}
