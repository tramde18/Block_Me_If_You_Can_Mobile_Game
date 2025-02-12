using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSavePlayer : MonoBehaviour {

	GameObject savePlayer;
	GlobalVariables globalVar;
	Vector3 savePlayerPos;

	void Start(){
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables> ();
	}

	public void AddPlayer(){
		savePlayer = GameObject.FindGameObjectWithTag("Player");
		savePlayerPos = new Vector3 (this.transform.position.x,
			this.transform.position.y,
			this.transform.position.z + 1f);
		GameObject GO = Instantiate (savePlayer,savePlayerPos,this.transform.rotation);
		GO.transform.parent = GameObject.Find ("Worldspace Object").transform;
		//Destroy (GO.GetComponent<AddSavePlayer> ());

	}

	

}
