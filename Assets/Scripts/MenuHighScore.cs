using UnityEngine;
using System.Collections;

// Handles saving of highscore, settings, etc.

public class MenuHighScore : MonoBehaviour {

	private int highScore = 0;
	private int currentScoreNum = 0;
	public TextMesh textMesh;
	public TextMesh currentScore;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("highScore")) {
			highScore = PlayerPrefs.GetInt ("highScore");
		}
		if (PlayerPrefs.HasKey ("currentScore")) {
			currentScoreNum = PlayerPrefs.GetInt ("currentScore");
		}
		textMesh.text = "Highscore: " + highScore.ToString ();
		currentScore.text = "Your score: " + currentScoreNum.ToString();
	}
}
