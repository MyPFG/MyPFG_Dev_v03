using UnityEngine;
using System.Collections;

public class LightSwitch : InteractiveObject {
	public Light [] lights;

	// Update is called once per frame

	
/*	
	void OnMouseEnter (){
		//base.OnMouseEnter();
		//base.Resaltar ();
		base.OnMouseEnter();
	}
*/	
	new void  OnMouseDown(){

		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)
			return;

		base.OnMouseDown();			
		foreach (Light l in lights){
			l.enabled = !l.enabled;
			//Debug.Log ("luz " + l.name + "#"+l.enabled);
		}
	}
	
}
