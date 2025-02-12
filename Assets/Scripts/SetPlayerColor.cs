using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerColor : MonoBehaviour {

	GlobalVariables globalVar;
	public Renderer pants,shirt;
	Color playerColor;

	void Start(){
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables>();

		if (globalVar.playerColor == "BLUE") {
			playerColor = Color.blue;
		} else if (globalVar.playerColor == "GREEN") {
			playerColor = Color.green;
		} else if (globalVar.playerColor == "BLACK") {
			playerColor = Color.black;
		}

		pants.material.color = playerColor;
		shirt.material.color = playerColor;

		if (!globalVar.isSoundsOn) {
			Camera.main.GetComponent<AudioListener> ().enabled = false;
		}
	}




}
