using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class LaneSensor : MonoBehaviour {
	Animator firstLaneEnemy,secondLaneEnemy,thirdLaneEnemy,midLaneEnemy,camAnim;
	GlobalVariables globalVar;
	DataPersistence saveLoad;
	AddSavePlayer addSavePlayer;
	PowerUps powerUps;
	GameObject player;
	GameObject QuadrantOne,QuadrantTwo,QuadrantThree,QuadrantFour;
	Text QoneTimer,QtwoTimer,QthreeTimer,QfourTimer,timer;
	AudioSource audioManager;
	public AudioClip taloKanaSFX;
	float minutes,seconds;

	void Start(){
		audioManager = GameObject.Find ("InGame_SFX").GetComponent<AudioSource> ();
		powerUps = GameObject.FindGameObjectWithTag ("PowerupsQ1").GetComponent<PowerUps> ();
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables> ();
		saveLoad = GameObject.Find ("GlobalVariables").GetComponent<DataPersistence> ();
		firstLaneEnemy = GameObject.FindGameObjectWithTag ("1stLaneEnemy").GetComponent<Animator> ();
		secondLaneEnemy = GameObject.FindGameObjectWithTag ("2ndLaneEnemy").GetComponent<Animator> ();
    	thirdLaneEnemy = GameObject.FindGameObjectWithTag ("3rdLaneEnemy").GetComponent<Animator> ();
		midLaneEnemy = GameObject.FindGameObjectWithTag ("MidLaneEnemy").GetComponent<Animator> ();
		camAnim = GameObject.FindGameObjectWithTag("worldSpaceObj").GetComponent<Animator>();

		QuadrantOne = GameObject.FindGameObjectWithTag ("Quadrant1");
		QuadrantTwo = GameObject.FindGameObjectWithTag ("Quadrant2");
		QuadrantThree = GameObject.FindGameObjectWithTag ("Quadrant3");
		QuadrantFour = GameObject.FindGameObjectWithTag ("Quadrant4");

		QoneTimer = GameObject.FindGameObjectWithTag ("Quadrant1Timer").GetComponent<Text>();
		QtwoTimer = GameObject.FindGameObjectWithTag ("Quadrant2Timer").GetComponent<Text>();
		QthreeTimer = GameObject.FindGameObjectWithTag ("Quadrant3Timer").GetComponent<Text>();
		QfourTimer = GameObject.FindGameObjectWithTag ("Quadrant4Timer").GetComponent<Text>();

	}

	void Update(){	
		player = GameObject.FindGameObjectWithTag ("Player");
		addSavePlayer = player.GetComponent<AddSavePlayer>();

		if (!globalVar.isFreeze) {
			if (CrossPlatformInputManager.GetAxis ("Horizontal") > 0) {
				if (globalVar.playerPos == "1stLaneSensor") {
					StrafeEnemy ("Right", firstLaneEnemy);
					FirstLaneEnemyFollow ();
				} else if (globalVar.playerPos == "Quadrant3" || globalVar.playerPos == "Quadrant4") {
					StopEnemyFollows (firstLaneEnemy);
					StrafeEnemy ("Right", secondLaneEnemy);
					SecondLaneEnemyFollow ();
				}else if (globalVar.playerPos == "Quadrant2" || globalVar.playerPos == "Quadrant1") {
					//StopEnemyFollows (secondLaneEnemy);
					if (!globalVar.isFlip) {
						StopEnemyFollows (firstLaneEnemy);
						StrafeEnemy ("Right", secondLaneEnemy);
						StrafeEnemy ("Right", thirdLaneEnemy);
						ThirdLaneEnemyFollow ();
					} else if (globalVar.isFlip) {
						StopEnemyFollows (thirdLaneEnemy);
						StrafeEnemy ("Right", secondLaneEnemy );
						StrafeEnemy ("Right", firstLaneEnemy );
						FirstLaneEnemyFollow ();
					}

				}
			} else if (CrossPlatformInputManager.GetAxis ("Horizontal") < 0) {
				if(globalVar.playerPos == "1stLaneSensor"){
					StrafeEnemy ("Left", firstLaneEnemy);
					FirstLaneEnemyFollow ();
				}else if (globalVar.playerPos == "Quadrant3" || globalVar.playerPos == "Quadrant4") {
					StopEnemyFollows (firstLaneEnemy);
					StrafeEnemy ("Left", secondLaneEnemy);
					SecondLaneEnemyFollow ();
				}else if (globalVar.playerPos == "Quadrant2" || globalVar.playerPos == "Quadrant1") {
					StopEnemyFollows (firstLaneEnemy);
					//StopEnemyFollows (secondLaneEnemy);
					//StrafeEnemy ("Left", secondLaneEnemy );
					//StrafeEnemy ("Left", thirdLaneEnemy );
					//SecondLaneEnemyFollow ();
					//ThirdLaneEnemyFollow ();
					if (!globalVar.isFlip) {
						StopEnemyFollows (firstLaneEnemy);
						StrafeEnemy ("Left", secondLaneEnemy);
						StrafeEnemy ("Left", thirdLaneEnemy);
						ThirdLaneEnemyFollow ();
					} else if (globalVar.isFlip) {
						StopEnemyFollows (thirdLaneEnemy);
						StrafeEnemy ("Left", secondLaneEnemy );
						StrafeEnemy ("Left", firstLaneEnemy );
						FirstLaneEnemyFollow ();
					}
				}
			} else if (CrossPlatformInputManager.GetAxis ("Horizontal") == 0) {
				if(globalVar.playerPos == "1stLaneSensor"){
					StopEnemyFollows (firstLaneEnemy);
				}else if (globalVar.playerPos == "Quadrant3" || globalVar.playerPos == "Quadrant4") {
					StopEnemyFollows (secondLaneEnemy);
					StopEnemyFollows (midLaneEnemy);
				}else if (globalVar.playerPos == "Quadrant2" || globalVar.playerPos == "Quadrant1") {
					StopEnemyFollows (thirdLaneEnemy);
					StopEnemyFollows (midLaneEnemy);
					StopEnemyFollows (secondLaneEnemy);
				}
			}

			if (CrossPlatformInputManager.GetAxis ("Vertical") > 0) {
				if (globalVar.playerPos == "Quadrant3") {
					if (globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 90, midLaneEnemy.transform.rotation.z);
					} else if (!globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 270, midLaneEnemy.transform.rotation.z);
					}
					StrafeEnemy ("Left", midLaneEnemy);
					MidLaneEnemyFollow ();
				} else if (globalVar.playerPos == "Quadrant4") {
					if (globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 270, midLaneEnemy.transform.rotation.z);
					} else if (!globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 90, midLaneEnemy.transform.rotation.z);
					}
					StrafeEnemy ("Right", midLaneEnemy);
					MidLaneEnemyFollow ();
				}

			} else if (CrossPlatformInputManager.GetAxis ("Vertical") < 0) {
				if (globalVar.playerPos == "Quadrant3" || globalVar.playerPos == "Quadrant2") {
					if (globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 90, midLaneEnemy.transform.rotation.z);
					} else if (!globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 270, midLaneEnemy.transform.rotation.z);
					}
					StrafeEnemy ("Right", midLaneEnemy);
					MidLaneEnemyFollow ();
				} else if (globalVar.playerPos == "Quadrant4" || globalVar.playerPos == "Quadrant1") {
					if (globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 270, midLaneEnemy.transform.rotation.z);
					} else if (!globalVar.isFlip) {
						midLaneEnemy.transform.localEulerAngles = 
							new Vector3 (midLaneEnemy.transform.rotation.x, 90, midLaneEnemy.transform.rotation.z);
					}
					StrafeEnemy ("Left", midLaneEnemy);
					MidLaneEnemyFollow ();
				}
			}
		}


	}

	void OnTriggerExit(Collider col){
		if (this.tag == "3rdLaneSensor" && col.tag == "Player") {
			StopEnemyFollows (midLaneEnemy);
			StopEnemyFollows (firstLaneEnemy);
			StopEnemyFollows (secondLaneEnemy);
			StopEnemyFollows (thirdLaneEnemy);
			if (!globalVar.isFlip) {
				//RotateEnemy (-180);
				camAnim.SetBool ("isFlip", true);
				//StartCoroutine(SetupFlipAnimation(true,-180));
				ChangeTagOnFlip (true);
				globalVar.isFlip = true;
				globalVar.playerPos = "";
				//powerUps.GeneratePowerups ();
				if (globalVar.SaveLives <= 3) {
					addSavePlayer.AddPlayer ();
					globalVar.SaveLives++;
				}

			} 
		} else if (this.tag == "1stLaneSensor" && col.tag == "Player") {
			StopEnemyFollows (midLaneEnemy);
			StopEnemyFollows (firstLaneEnemy);
			StopEnemyFollows (secondLaneEnemy);
			StopEnemyFollows (thirdLaneEnemy);
			if (globalVar.isFlip) {
				//StartCoroutine(SetupFlipAnimation(false,180));
				camAnim.SetBool ("isFlip", false);
				ChangeTagOnFlip (false);
				globalVar.isFlip = false;
				globalVar.playerPos = "";
				//powerUps.GeneratePowerups ();
				if (globalVar.SaveLives <= 3) {
					addSavePlayer.AddPlayer ();
					globalVar.SaveLives++;
				}

			} 
		}
	}

	IEnumerator SetupFlipAnimation(bool condition,int rotation){
		camAnim.SetBool ("isFlip", condition);
		yield return new WaitForSeconds (2.5f);
		RotateEnemy (rotation);
	}

	void OnTriggerEnter(Collider col){
		if ((this.tag == "Quadrant1" && col.tag == "Player") ||
			(this.tag == "Quadrant2" && col.tag == "Player") || 
			(this.tag == "Quadrant3" && col.tag == "Player") ||
			(this.tag == "Quadrant4" && col.tag == "Player")){
			ResetQuadrantTimer ();
		}
	}

	void OnTriggerStay(Collider col){
		if (this.tag == "1stLaneSensor" && col.tag == "Player") {
			globalVar.playerPos = "1stLaneSensor";
		} else if (this.tag == "Quadrant3" && col.tag == "Player") {
			globalVar.playerPos = "Quadrant3";
		} else if (this.tag == "Quadrant4" && col.tag == "Player") {
			globalVar.playerPos = "Quadrant4";
		} else if (this.tag == "Quadrant2" && col.tag == "Player") {
			globalVar.playerPos = "Quadrant2";
		} else if (this.tag == "Quadrant1" && col.tag == "Player") {
			globalVar.playerPos = "Quadrant1";
		} else if (this.tag == "3rdLaneSensor" && col.tag == "Player") {
			globalVar.playerPos = "3rdLaneSensor";
		}

		if (!globalVar.isFlip && !globalVar.isPlayerDead) {
			if ((this.tag == "Quadrant1" && col.tag == "Player" && QoneTimer.text == "0") ||
				(this.tag == "Quadrant2" && col.tag == "Player" && QtwoTimer.text == "0") ||
				(this.tag == "Quadrant3" && col.tag == "Player" && QthreeTimer.text == "0") ||
				(this.tag == "Quadrant4" && col.tag == "Player" && QfourTimer.text == "0")) {
				//TimePenalty (1f);
				audioManager.loop = false;
				audioManager.PlayOneShot(taloKanaSFX);
				globalVar.isPlayerDead = true;
				saveLoad.Save ();
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Canvas>().enabled = false;
				firstLaneEnemy.GetComponent<EnemyController>().gameoverUI.SetActive(true);
				showScore ();
			}
		} else if (globalVar.isFlip && !globalVar.isPlayerDead) {
			if ((this.tag == "Quadrant3" && col.tag == "Player" && QoneTimer.text == "0") ||
				(this.tag == "Quadrant4" && col.tag == "Player" && QtwoTimer.text == "0") ||
				(this.tag == "Quadrant1" && col.tag == "Player" && QthreeTimer.text == "0") ||
				(this.tag == "Quadrant2" && col.tag == "Player" && QfourTimer.text == "0")) {
				//TimePenalty (1f);
				audioManager.loop = false;
				audioManager.PlayOneShot(taloKanaSFX);
				globalVar.isPlayerDead = true;
				saveLoad.Save ();
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Canvas>().enabled = false;
				firstLaneEnemy.GetComponent<EnemyController>().gameoverUI.SetActive(true);
				showScore ();
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

	void TimePenalty(float timePenalty){
		globalVar.time += timePenalty;
	}

	void FirstLaneEnemyFollow(){
		if (player != null) {
			Vector3 newPos = new Vector3 (player.transform.position.x,
				                 firstLaneEnemy.transform.position.y,
				                 firstLaneEnemy.transform.position.z);
			
			firstLaneEnemy.transform.position = Vector3.MoveTowards (firstLaneEnemy.transform.position, newPos, Time.deltaTime);
		}
	}

	void SecondLaneEnemyFollow(){
		if (player != null) {
			Vector3 newPos = new Vector3 (player.transform.position.x,
				secondLaneEnemy.transform.position.y,
				secondLaneEnemy.transform.position.z);

			secondLaneEnemy.transform.position = Vector3.MoveTowards (secondLaneEnemy.transform.position,newPos,Time.deltaTime);
		}
	}

	void ThirdLaneEnemyFollow(){
		if (player != null) {
			Vector3 newPos = new Vector3 (player.transform.position.x,
				thirdLaneEnemy.transform.position.y,
				thirdLaneEnemy.transform.position.z);

			thirdLaneEnemy.transform.position = Vector3.MoveTowards (thirdLaneEnemy.transform.position,newPos ,Time.deltaTime);
		}
	}

	void MidLaneEnemyFollow(){
		if (player != null) {
			Vector3 newPos = new Vector3 (midLaneEnemy.transform.position.x,
				midLaneEnemy.transform.position.y,
				player.transform.position.z);

			midLaneEnemy.transform.position = Vector3.MoveTowards (midLaneEnemy.transform.position,newPos, Time.deltaTime);
		}
	}

	void StrafeEnemy(string direction,Animator enemy){
		if (direction == "Left") {
			enemy.SetBool ("isStrafingLeft", true);
			enemy.SetBool ("isStrafingRight", false);
		} else if (direction == "Right") {
			enemy.SetBool ("isStrafingLeft", false);
			enemy.SetBool ("isStrafingRight", true);
		}
	}

	void StopEnemyFollows(Animator enemy){
		enemy.SetBool ("isStrafingLeft", false);
		enemy.SetBool ("isStrafingRight", false);
	}


	public void RotateEnemy(int Yrotation){
		firstLaneEnemy.GetComponent<Rigidbody> ().isKinematic = false;
		firstLaneEnemy.GetComponent<Animator> ().enabled = true;
		secondLaneEnemy.GetComponent<Rigidbody> ().isKinematic = false;
		secondLaneEnemy.GetComponent<Animator> ().enabled = true;
		thirdLaneEnemy.GetComponent<Rigidbody> ().isKinematic = false;
		thirdLaneEnemy.GetComponent<Animator> ().enabled = true;
		midLaneEnemy.GetComponent<Rigidbody> ().isKinematic = false;
		midLaneEnemy.GetComponent<Animator> ().enabled = true;

		firstLaneEnemy.transform.localEulerAngles = new Vector3(firstLaneEnemy.transform.localEulerAngles.x,
				Yrotation,
			firstLaneEnemy.transform.localEulerAngles.z);

		secondLaneEnemy.transform.localEulerAngles = new Vector3(secondLaneEnemy.transform.localEulerAngles.x,
				Yrotation,
			secondLaneEnemy.transform.localEulerAngles.z);

		thirdLaneEnemy.transform.localEulerAngles = new Vector3(thirdLaneEnemy.transform.localEulerAngles.x,
				Yrotation,
			thirdLaneEnemy.transform.localEulerAngles.z);

		midLaneEnemy.transform.localEulerAngles = new Vector3(thirdLaneEnemy.transform.localEulerAngles.x,
				Yrotation,
			thirdLaneEnemy.transform.localEulerAngles.z);
	}

	void ResetQuadrantTimer(){
		globalVar.defaultQuadrantTime = 8f;
	}

	void ChangeTagOnFlip(bool isReverse){
		if (isReverse) {
			thirdLaneEnemy.tag = "1stLaneEnemy";
			firstLaneEnemy.tag = "3rdLaneEnemy";
			QuadrantOne.tag = "Quadrant3"; 
			QuadrantTwo.tag = "Quadrant4";
			QuadrantThree.tag = "Quadrant1";
			QuadrantFour.tag = "Quadrant2";

		} else if (!isReverse) {
			firstLaneEnemy.tag = "1stLaneEnemy";
			thirdLaneEnemy.tag = "3rdLaneEnemy";
			QuadrantOne.tag = "Quadrant1"; 
			QuadrantTwo.tag = "Quadrant2";
			QuadrantThree.tag = "Quadrant3";
			QuadrantFour.tag = "Quadrant4";
		}
	}
}
