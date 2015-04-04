using UnityEngine;
using System.Collections;

public class Arc {
	int origin;
	int destiny;
	string text;

	public Arc(int _origin, int _destiny, string _text){
		origin = _origin;
		destiny = _destiny;
		text = _text;
	}

	public void SetOrigin (int _origin){
		origin = _origin;
	}
	public void SetDestiny (int _destiny){
		destiny = _destiny;
	}
	public void SetText (string _text){
		text = _text;
	}

	public int GetOrigin (){
		return origin;
	}
	public int GetDestiny (){
		return destiny;
	}
	public string GetText (){
		return text;
	}

}
