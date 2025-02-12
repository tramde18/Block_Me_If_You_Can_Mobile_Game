using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	GlobalVariables globalVar;
	DataPersistence saveLoad;
	public GameObject gameoverUI,mainCanvas;
	public AudioClip tayaSFX,taloKanaSFX;
	public AudioSource audioManager;
	public Image staminaBar;
	float minutes,seconds;

	void Start(){
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables> ();
		saveLoad = GameObject.Find ("GlobalVariables").GetComponent<DataPersistence> ();
	}

	void Update(){
		if (staminaBar.fillAmount <= 0f && !globalVar.isPlayerDead) {
			PlaySFX (taloKanaSFX);
			globalVar.isPlayerDead = true;
			saveLoad.Save ();
			mainCanvas.GetComponent<Canvas> ().enabled = false;
			gameoverUI.SetActive (true);
			showScore ();
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" && !globalVar.isPlayerDead) {
			globalVar = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables> ();
			if(globalVar.SaveLives == 1){
				PlaySFX (taloKanaSFX);
				globalVar.isPlayerDead = true;
				saveLoad.Save ();
				mainCanvas.GetComponent<Canvas> ().enabled = false;
				globalVar.SaveLives--;
				gameoverUI.SetActive (true);
				showScore ();
			}else if (globalVar.SaveLives > 1) {
				PlaySFX (tayaSFX);
				globalVar.SaveLives--;
				Destroy (col.gameObject);
			}  
		}
	}

	void showScore(){
		Text score;
		score = GameObject.FindGameObjectWithTag ("YourTime").GetComponent<Text> ();

		minutes = (Mathf.RoundToInt (globalVar.currentTime) % 3600) / 60;
		seconds = (Mathf.RoundToInt (globalVar.currentTime) % 3600) % 60;
		score.text  = minutes.ToString () + ":" + seconds.ToString ();
	}

	void PlaySFX(AudioClip clip){
		audioManager.loop = false;
		audioManager.PlayOneShot(clip);
	}

}
