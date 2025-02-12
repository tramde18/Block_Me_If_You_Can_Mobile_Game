using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour {

	public Image[] Quadrant;
	public Sprite[] powerUps;
	GlobalVariables globalVar;
	public Image staminaBar;
	public GameObject[] enemy;
	float stamina;
	GameObject slideBtn;

	void Start(){
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables> ();
		slideBtn = GameObject.FindGameObjectWithTag ("SlideBtn");
	}

	public void GeneratePowerups(){
		int generateUps = Random.Range (0, 2);
		int generateQuadrant = Random.Range (0, 4);

		Quadrant [0].GetComponent<Image> ().enabled = false;
		Quadrant [1].GetComponent<Image> ().enabled = false;
		Quadrant [2].GetComponent<Image> ().enabled = false;
		Quadrant [3].GetComponent<Image> ().enabled = false;
		Quadrant [0].GetComponent<BoxCollider> ().enabled = false;
		Quadrant [1].GetComponent<BoxCollider> ().enabled = false;
		Quadrant [2].GetComponent<BoxCollider> ().enabled = false;
		Quadrant [3].GetComponent<BoxCollider> ().enabled = false;

		Quadrant [generateQuadrant].sprite = powerUps [generateUps];
		Quadrant [generateQuadrant].GetComponent<Image> ().enabled = true;
		Quadrant [generateQuadrant].GetComponent<BoxCollider> ().enabled = true;

		if (generateUps == 0) {
			globalVar.powerUps = "Freeze";
		} else {
			globalVar.powerUps = "Stamina";
		}

		globalVar.powerUpsPos = Quadrant [generateQuadrant].tag.ToString ();
		slideBtn.GetComponent<Button>().interactable = true;

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			//GameObject.FindGameObjectWithTag(globalVar.powerUpsPos).GetComponent<Image> ().enabled = false;
			//GameObject.FindGameObjectWithTag(globalVar.powerUpsPos).GetComponent<BoxCollider> ().enabled = false;
			this.GetComponent<Image> ().enabled = false;
			this.GetComponent<BoxCollider> ().enabled = false;
			Debug.Log ("Ups");
			if (globalVar.powerUps == "Stamina") {
				stamina = staminaBar.fillAmount;
				staminaBar.fillAmount = (stamina + 0.10f);
				globalVar.powerUps = "";
			} else if (globalVar.powerUps == "Freeze") {
				StartCoroutine (SetupFreeze ());
				globalVar.powerUps = "";
			}
		}	
	}


	IEnumerator SetupFreeze(){
		foreach (GameObject e in enemy) {
			globalVar.isFreeze = true;
			e.GetComponent<Rigidbody> ().isKinematic = true;
			e.GetComponent<Animator> ().enabled = false;
		}
		yield return new WaitForSeconds (3f);
		foreach (GameObject e in enemy) {
			globalVar.isFreeze = false;
			e.GetComponent<Rigidbody> ().isKinematic = false;
			e.GetComponent<Animator> ().enabled = true;
		}
	}


}
