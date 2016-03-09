using UnityEngine;
using System.Collections;

// This ensures that when we change scene, the music continues playing from the previous scene and does not restart.

public class KeepMusicOverScenes : MonoBehaviour {

	private static KeepMusicOverScenes instance = null;
	public static KeepMusicOverScenes Instance{
		get { return instance; }
	}
	public AudioClip gameMusic;
	
	void Start(){
		GetComponent<AudioSource>().clip = gameMusic;
		if(Application.loadedLevelName == "mainHall"){
			GetComponent<AudioSource>().Play ();
		}
		
		if(instance != null && instance != this){
			Destroy (this.gameObject);
			return;
		}
		else{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}