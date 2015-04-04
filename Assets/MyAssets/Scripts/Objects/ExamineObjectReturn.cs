using UnityEngine;
using System.Collections;

public class ExamineObjectReturn : MonoBehaviour {
	public InventoryManager inventoryManager;

	public void Start(){ 
		inventoryManager = GameObject.Find ("GameManager").GetComponent<InventoryManager>();
	}

	public void OnMouseDown(){

		inventoryManager.examinationCamera.camera.enabled = false;
		if (inventoryManager.objectUnderExamination != null)
			inventoryManager.objectUnderExamination.renderer.enabled = false;
	}
}
