using UnityEngine;
using System.Collections;

public class TimerScript: MonoBehaviour {

	// Update these depending on which style is used
	public float minutes;
	public float seconds;
	public float miliseconds;
	
	void Start(){
		if(Application.loadedLevelName == "MainHall" ||
		   Application.loadedLevelName == "MainHall_"){
			minutes = 0;
			seconds = 1000;
			miliseconds = 0;
		}
		else{
			seconds = PlayerPrefs.GetInt("timeLeft");
		}
	}
	
	void Update(){
		// Seconds only style
		seconds -= Time.deltaTime;
		
		if (seconds > 0)
		{
			guiText.text = seconds.ToString("F0");
		}
		else{
			Application.LoadLevel ("GameOver");
		}
	}
}