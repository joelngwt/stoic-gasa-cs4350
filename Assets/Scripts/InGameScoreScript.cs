using UnityEngine;
using System.Collections;

public class InGameScoreScript : MonoBehaviour {

	public int currentScore;
	public int highScore;
	//public LifeCounter lifecounterScript;

	void Start(){
		if(Application.loadedLevelName == "MainHall"){
			currentScore = 0;
		}
		else if (PlayerPrefs.HasKey ("currentScore")) {
			currentScore = PlayerPrefs.GetInt ("currentScore");
		}
		if (PlayerPrefs.HasKey ("highScore")) {
			highScore = PlayerPrefs.GetInt ("highScore");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Score: " + currentScore.ToString("F0");
		
		if(highScore < currentScore){
			PlayerPrefs.SetInt ("highScore", (int)currentScore);
		}
	}
}
