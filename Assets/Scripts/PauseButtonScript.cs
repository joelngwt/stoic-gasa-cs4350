using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {

	public Texture2D greenButton; 
	public Texture2D yellowButton;
	public Texture2D resume;
	public Texture2D exit;
	
	public AudioClip music;

	// Use this for initialization
	void Start () {
		// initialize button to white
		GetComponent<GUITexture>().texture = greenButton; 
		
		if(Application.loadedLevelName == "MainHall" || Application.loadedLevelName == "DiningHall" || Application.loadedLevelName == "BossRoom" || Application.loadedLevelName == "ActualBossRoom"){
			GetComponent<AudioSource>().clip = music;
			GetComponent<AudioSource>().Play ();
		}
	}
	
	void OnMouseDown() {
		if (GetComponent<GUITexture>().name == "Pause Button"){
			GetComponent<GUITexture>().texture = yellowButton;
		}
	}

	void OnMouseUp() {
		GetComponent<GUITexture>().texture = greenButton;
		
		if(GetComponent<GUITexture>().name == "Pause Button" && Time.timeScale == 1) { // game is running
			Time.timeScale = 0; // pause the game
		}
		else if(GetComponent<GUITexture>().name == "Pause Button" && Time.timeScale == 0) { // game is paused
			Time.timeScale = 1; // unpause the game
		}
	}
}
