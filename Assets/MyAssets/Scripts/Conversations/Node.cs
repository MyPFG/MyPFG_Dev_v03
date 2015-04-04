using UnityEngine;
using System.Collections;

public class Node{
	int nodeId;
	string text;
	string tone;
	string speaker;

	public Node (int _nodeId, string _text, string _tone, string _speaker){
		nodeId = _nodeId;
		text = _text;
		tone = _tone;
		speaker = _speaker;
	}

	public void SetId (int _id){
		nodeId = _id;
	}
	public void SetText (string _text){
		text = _text;
	}
	public void SetTone (string _tone){
		tone = _tone;
	}
	public void SetSpeaker (string _speaker){
		speaker = _speaker;
	}


	public int GetId (){
		return nodeId;
	}
	public string GetText (){
		return text;
	}
	public string GetTone (){
		return tone;
	}
	public string GetSpeaker (){
		return speaker;
	}

}
