using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text selectedColor,bestTime;
	GlobalVariables globalVar;	
	Image sounds;
	public Sprite soundsOn, soundsOff;
	float minutes,seconds;
	public Image stamina;

	void Start(){
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables> ();
		if (GameObject.FindGameObjectWithTag ("Sounds") != null) {
			sounds = GameObject.FindGameObjectWithTag ("Sounds").GetComponent<Image> ();
		}
	}

	void Update(){
		if (globalVar.time == 0f && globalVar.playerColor != "") {
			globalVar.isPlayerDead = false;
		}
	}

	public void StartGame(){
		globalVar.playerColor = selectedColor.text;
		DontDestroyOnLoad (GameObject.Find ("GlobalVariables"));
		SceneManager.LoadScene ("Gameplay");
	}

	public void BackToMenu(){
		Destroy (GameObject.Find ("GlobalVariables"));
		SceneManager.LoadScene ("Menu");

	}

	public void Restart(){
		DontDestroyOnLoad (GameObject.Find ("GlobalVariables"));
		SceneManager.LoadScene ("Gameplay");
		ResetVariables ();
		if (!globalVar.isSoundsOn) {
			AudioListener.volume = 0;
		}
	}

	public void PausedResume(bool isPaused){
		if (isPaused) {
			globalVar.isPaused = true;
		} else if (!isPaused) {
			globalVar.isPaused = false;
		}
	}

	public void ShowBestTime(){
		minutes = (Mathf.RoundToInt (globalVar.bestTime) % 3600) / 60;
		seconds = (Mathf.RoundToInt (globalVar.bestTime) % 3600) % 60;
		bestTime.text  = minutes.ToString () + ":" + seconds.ToString ();
	}

	public void OnOffSounds(){
		if (globalVar.isSoundsOn) {
			globalVar.isSoundsOn = false;
			sounds.GetComponent<Image> ().sprite = soundsOff;
			AudioListener.volume = 0;
		} else {
			globalVar.isSoundsOn = true;
			sounds.GetComponent<Image> ().sprite = soundsOn;
			AudioListener.volume = 1;
		}
	}

	void ResetVariables(){
		globalVar.time = 0;
		globalVar.defaultQuadrantTime = 8;
		globalVar.playerPos = "";
		globalVar.isFlip = false;
		globalVar.SaveLives = 1;
		globalVar.isPaused = false;
		globalVar.isPlayerDead = false;
		globalVar.bestTime = 0;
		globalVar.currentTime = 0;
		globalVar.powerUps = "";
		globalVar.powerUpsPos = "";
	}

}
