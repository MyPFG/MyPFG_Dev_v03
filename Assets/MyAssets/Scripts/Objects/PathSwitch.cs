using UnityEngine; 
using System.Collections;

public class PathSwitch : InteractiveObject {
	public GameObject [] blockers;
	bool used =false;
	
	new void OnMouseDown (){

		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)
			return;
		if (!used){
			base.OnMouseDown();
			foreach (GameObject b in blockers){
				b.SetActive (false);
				//Debug.Log (b.name);
				//o, para los que tengan animacion, disparar la animacion en lugar de hacerlo desaparecer (p.ej.una puerta que se abre)
			}
		}
		
		used = true;
	}
}
