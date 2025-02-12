using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorials : MonoBehaviour {
	
	public void PlayVideoTutorials(VideoClip vid){
		this.GetComponent<VideoPlayer> ().clip = vid;
		this.GetComponent<VideoPlayer> ().Play ();
	}

}
