using UnityEngine;
using System.Collections;

public class SceneShade : MonoBehaviour {

	InventoryManager inventoryManager;
	void Start () {
		inventoryManager = GameObject.Find ("GameManager").GetComponent<InventoryManager>();

	}

	void OnMouseDown (){
		//here turn off inventory and restore gamestatus to exploring $%& PENDING
	//	inventoryManager.ToggleInventoryLayers (false);
	}
	

}
