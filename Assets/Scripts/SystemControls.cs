using UnityEngine;
using System.Collections;

// Used for backend stuff
// Functions in this script:
// 1. Back button
// 2. Prevent sleep (not in use)
// 3. Game pauses when multitasking

// Script is found in:
// pause button
// main menu title
// settings title

public class SystemControls : MonoBehaviour {
	
	private bool paused;
	public GUITexture backAgainMessage;
	private int backPressCounter = 0;
	public PauseButtonScript pauseButtonscript;
	
	// Use this for initialization
	void Start () {
		// prevent the device from sleeping/dimming
		// Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if(Application.loadedLevelName == "mainMenu"){
			backAgainMessage.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKeyDown("escape")) // when the back button is pressed on the device
			{
				// Currently in game
				if((Application.loadedLevelName == "MainHall"  || Application.loadedLevelName == "DiningHall" || Application.loadedLevelName == "BossRoom") && Time.timeScale == 1)
				{
					// pause game
					Time.timeScale = 0;
				}
				// Game is paused and currently in game
				else if(Time.timeScale == 0)
				{	
					// Unpause the game
					Time.timeScale = 1;
					pauseButtonscript.guiTexture.texture = pauseButtonscript.button1;
					//Application.LoadLevel("mainMenu");
				}
				else if(Application.loadedLevelName == "mainMenu")
				{
					if(backPressCounter == 0)
					{
						backPressCounter++;
						backAgainMessage.enabled = true;
					}
					else if(backPressCounter == 1)
					{
						Application.Quit();
					}
				}
				else if(Application.loadedLevelName == "settings")
				{
					Application.LoadLevel("mainMenu");
				}
				else if(Application.loadedLevelName == "GameOver")
				{
					Application.LoadLevel("mainMenu");
				}
				else if(Application.loadedLevelName == "HelpScreen1" || Application.loadedLevelName == "HelpScreen2" || Application.loadedLevelName == "HelpScreen3" || Application.loadedLevelName == "HelpScreen4")
				{
					Application.LoadLevel("mainMenu");
				}
			}
			
			 /*if(Input.GetKey (KeyCode.Menu)) // for the device menu button (leftmost button on the Note)
			 {
				// currently not in use
			 }*/
		}
	}
	
	// Causes the app to pause when you press the home button, or if the app is interrupted with a phone call, etc.
	void OnApplicationPause(bool pauseStatus) {
		paused = pauseStatus;
		
		if(paused == true)
		{
			Time.timeScale = 0;
		}
	}
}
