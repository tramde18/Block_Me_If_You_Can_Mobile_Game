using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class DataPersistence : MonoBehaviour {

	GlobalVariables globalVar;

	void Start(){
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables> ();
	}

	public void Save(){
		globalVar.currentTime = globalVar.time;

		if (globalVar.currentTime > globalVar.bestTime) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/playerData.dat");

			PlayerData data = new PlayerData ();
			globalVar.currentTime = globalVar.time;
			data.bestTime = globalVar.currentTime;

			bf.Serialize (file, data);
			file.Close ();
		}
	}

	public void Load(){
		globalVar = GameObject.Find ("GlobalVariables").GetComponent<GlobalVariables> ();
		if (File.Exists (Application.persistentDataPath + "/playerData.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat",FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			globalVar.bestTime = data.bestTime;
		}
	}

}

[Serializable]
class PlayerData{
	public float bestTime;
}