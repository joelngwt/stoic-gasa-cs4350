using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {

	public Texture2D button1; // white (unpaused)
	public Texture2D button2; // red (paused)
	public Texture2D resume;
	public Texture2D exit;
	
	public AudioClip music;

	// Use this for initialization
	void Start () {
		// initialize button to white
		guiTexture.texture = button1; 
		
		if(Application.loadedLevelName == "MainHall" || Application.loadedLevelName == "DiningHall" || Application.loadedLevelName == "BossRoom"){
			audio.clip = music;
			audio.Play ();
		}
		
	}

	void OnMouseUp(){
		if(guiTexture.name == "Pause Button" && Time.timeScale == 1) 
		{ // game is running
			guiTexture.texture = button2; // red button
			Time.timeScale = 0; // pause the game
			// Todo: pop up menu
		}
		else if(guiTexture.name == "Pause Button" && Time.timeScale == 0)
		{ // game is paused
			guiTexture.texture = button1; // white button
			Time.timeScale = 1; // unpause the game
			
		}
	}

	void OnGUI()
	{
		// If game is paused
		if (Time.timeScale == 0)
		{
			guiTexture.texture = button2; // red button
			// Resume button
			if (GUI.Button (new Rect (50, Screen.height*(float)0.42, Screen.width*(float)0.2, Screen.height*(float)0.1), resume)) {
				Time.timeScale = 1;
				guiTexture.texture = button1;
			}
			// Settings
			// Exit
			if (GUI.Button (new Rect (50, Screen.height*(float)0.55, Screen.width*(float)0.2, Screen.height*(float)0.1), exit)) {
				Time.timeScale = 1;
				Application.LoadLevel("mainMenu");
			}
		}

	}
}
