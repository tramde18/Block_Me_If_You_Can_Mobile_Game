using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

	public float time,defaultQuadrantTime = 8,bestTime,currentTime;
	public string playerPos;
	public bool isFlip,isPaused,isPlayerDead,isFreeze,isSoundsOn;
	public string playerColor, powerUps,powerUpsPos;
	public int SaveLives;

	void Start(){
		this.gameObject.GetComponent<DataPersistence> ().Load ();
	}

}
