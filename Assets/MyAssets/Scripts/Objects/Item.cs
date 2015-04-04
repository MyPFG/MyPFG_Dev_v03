using UnityEngine;
using System.Collections;

public class Item : InteractiveObject {
	public int initialState = 0; //item initial state

	//metadata
	int [] location;
	int []  visibility;
	string [] itemName;    //name for the object in the current state
	string [] description; //description for the object in the current state
	//AnimationClip [] animationClip; //animation clip to play when the item is picked (by state)
	//float [] animationDelay; //to sync animations
	//AudioClip []  soundClip; //sound to play when item picked
	//float [] audioDelay; //as animationDelay
	//AnimationClip []  loopAnimation; //animation to be played and loop after main animation (is an idle)
	//AudioClip []  loopSoundFX //sund effect for loopAnimation
	//bool postLoop = false; // true means the object have idle animation
	//bool animates = true; // false = object have no animation at all
	
	//vars for track current state of the object. 'i state of arrays' (i = current)
	int currentState = 0;
	int currentLocation = 0;
	int currentVisibility = 0;
	string currentName; //current short description
	string currentDescrp; //current long description
	//AudioClip currentSound;
	//float currentAudioDelay = 0f;
	//AnimationClip currentAnimationClip;
	//float currentAnimationDelay = 0f;
	float currentAniLength; //to calculate delays
	//AnimationClip currentLoopAnimation;
	//AudioClip currentLoopSound;

    //vars for mouse functinallity
	private bool picked  = false;  //to prevent 'mouse over'
	private bool mouseDown; //to know if mouse is down
	private bool processing; //to suspend mouse over functionallities

	new void  OnMouseDown(){
		//check distance && game status before mouse down logic
		if (DistanceToPlayer()> interactionDistance || gameManager.gameStatus != Enums.GameStatus.Exploring)
			return;


		Debug.Log ("mouse down on " + this.name);

/* OBSOLETO
		//mouse down and object goes to inventory layer
		base.OnMouseDown();	

		gameObject.layer = LayerMask.NameToLayer ("Inventory");
*/
	}

}
