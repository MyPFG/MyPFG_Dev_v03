#pragma strict

function Start () {

}

function Update () {
	var pos = Input.mousePosition;
	
	guiTexture.pixelInset.x = pos.x;
	guiTexture.pixelInset.y = pos.y - 32;
	Debug.Log ("mouse controller JS");
}