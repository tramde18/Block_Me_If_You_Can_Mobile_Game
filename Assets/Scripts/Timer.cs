using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	
	GlobalVariables globalVar;
	Text QoneTimer,QtwoTimer,QthreeTimer,QfourTimer,timer;
	private int minutes,seconds;

	void Start(){
		timer = this.GetComponent<Text> ();	
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables>();
		QoneTimer = GameObject.FindGameObjectWithTag ("Quadrant1Timer").GetComponent<Text> ();
		QtwoTimer = GameObject.FindGameObjectWithTag ("Quadrant2Timer").GetComponent<Text> ();
		QthreeTimer = GameObject.FindGameObjectWithTag ("Quadrant3Timer").GetComponent<Text> ();
		QfourTimer = GameObject.FindGameObjectWithTag ("Quadrant4Timer").GetComponent<Text> ();
	}

	void Update(){
		if ((!globalVar.isPaused || !globalVar.isPlayerDead) && globalVar.playerPos != "") {
			globalVar.time += Time.deltaTime;
			minutes = (Mathf.RoundToInt (globalVar.time) % 3600) / 60;
			seconds = (Mathf.RoundToInt (globalVar.time) % 3600) % 60;
			timer.text = minutes.ToString () + ":" + seconds.ToString ();
		


			if (globalVar.playerPos == "Quadrant1") {
				if (globalVar.defaultQuadrantTime >= 0) {
					HideTimerInQuadrant ();
					if (!globalVar.isFlip) {
						QoneTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					} else {
						QthreeTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					}	
				}
			} else if (globalVar.playerPos == "Quadrant2") {
				if (globalVar.defaultQuadrantTime >= 0) {
					HideTimerInQuadrant ();
					if (!globalVar.isFlip) {
						QtwoTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					} else {
						QfourTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					}	
				}
			} else if (globalVar.playerPos == "Quadrant3") {
				if (globalVar.defaultQuadrantTime >= 0) {
					HideTimerInQuadrant ();
					if (!globalVar.isFlip) {
						QthreeTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					} else {
						QoneTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					}	
				}
			} else if (globalVar.playerPos == "Quadrant4") {
				if (globalVar.defaultQuadrantTime >= 0) {
					HideTimerInQuadrant ();
					if (!globalVar.isFlip) {
						QfourTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					} else {
						QtwoTimer.text = Mathf.RoundToInt (globalVar.defaultQuadrantTime -= Time.deltaTime).ToString ();
					}	
				}
			} else {
				HideTimerInQuadrant ();
			}
		}
	} 

	void HideTimerInQuadrant(){
		QoneTimer.text = "";
		QtwoTimer.text = "";
		QthreeTimer.text = "";
		QfourTimer.text = "";
	}

}
