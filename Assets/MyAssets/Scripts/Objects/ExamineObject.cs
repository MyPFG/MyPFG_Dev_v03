using UnityEngine;
using System.Collections;

public class ExamineObject : MonoBehaviour {

	//references to external
	public InventoryManager inventoryManager;

	public GameObject detailModel;



	public void Start(){ 
		inventoryManager = GameObject.Find ("GameManager").GetComponent<InventoryManager>();

		//---
		if (detailModel != null)
			detailModel.SetActive (false);
	}


	public void OnMouseOver () {
		if(Input.GetMouseButtonDown(1)){
			InitExamination();
		}
	}



	void InitExamination(){
		//detailModel.renderer.enabled = true;
		//---
		detailModel.SetActive (true);
		inventoryManager.examinationCamera.camera.enabled = true;
		inventoryManager.objectUnderExamination = detailModel;
	}


}
