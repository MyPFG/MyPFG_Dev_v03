using UnityEngine;
using System.Collections;

public class TEST : MonoBehaviour {




	public void OnMouseEnter () {
		Debug.Log("enter");
		//Debug.Log (GetComponent<GUITexture>().texture.name);
		//guiTexture.texture = hoverTex;
	}
	
	public void  OnMouseExit(){
		//guiTexture.texture = normalTex;
		Debug.Log("exit");
	}

	public void OnMouseDown(){
		Debug.Log("clicked");
	}
}
