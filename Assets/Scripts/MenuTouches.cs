using UnityEngine;
using System.Collections;

public class MenuTouches : MonoBehaviour {
	
	public Texture2D button1;
	public Texture2D button2;
	public GUIText loading;
	public AudioClip menuButton;
	private float effectsVolume = 5.0F;
	
	void Start(){
		if(Application.loadedLevelName == "mainMenu"){
			loading.enabled = false;
		}
		
		if (PlayerPrefs.HasKey ("effectsVolume")) {
			effectsVolume = PlayerPrefs.GetInt ("effectsVolume");
		}
		AudioListener.volume = effectsVolume / 10;
	}
	
	void OnMouseUp(){
		if (this.name == "text_START"){
			StartCoroutine(LoadLevelSelect());
			//StartCoroutine(Loading());
		}
		else if (this.name == "text_Settings"){
			StartCoroutine(LoadSettings ());
		}
		else if (this.name == "text_Back"){
			StartCoroutine(LoadMainMenu ());
		}
		else if (this.name == "text_ClearHighscore"){
			PlayerPrefs.SetInt ("highScore", 0);
			PlayerPrefs.SetInt ("currentScore", 0);
			GetComponent<GUITexture>().texture = button1;
			GetComponent<AudioSource>().PlayOneShot(menuButton);
		}
		else if (this.name == "text_Backtomenu"){
			StartCoroutine(LoadMainMenu ());
		}
		else if (this.name == "text_Help"){
			StartCoroutine(LoadHelp1 ());
		}
		else if (this.name == "Left_Button"){ // back (help screen)
			if (Application.loadedLevelName == "HelpScreen2") {
				StartCoroutine(LoadHelp1 ());
			}
			else if (Application.loadedLevelName == "HelpScreen3") {
				StartCoroutine(LoadHelp2 ());
			}
		}
		else if (this.name == "Right_Button") { // forward (help screen)
			if (Application.loadedLevelName == "HelpScreen1") {
				StartCoroutine(LoadHelp2 ());
			}
			else if (Application.loadedLevelName == "HelpScreen2") {
				StartCoroutine(LoadHelp3 ());
			}
		}
		else if (this.name == "text_One") {
			if (GameObject.Find ("Music") != null) {
				Destroy(GameObject.Find ("Music")); // Stop menu music
			}
			StartCoroutine(LoadMainHall());
		}
		else if (this.name == "text_Two") {
			if (GameObject.Find ("Music") != null) {
				Destroy(GameObject.Find ("Music")); // Stop menu music
			}
			StartCoroutine(LoadDiningHall());
		}
		else if (this.name == "text_Three") {
			if (GameObject.Find ("Music") != null) {
				Destroy(GameObject.Find ("Music")); // Stop menu music
			}
			StartCoroutine(LoadBossRoom());
		}
		else if (this.name == "text_Boss") {
			if (GameObject.Find ("Music") != null) {
				Destroy(GameObject.Find ("Music")); // Stop menu music
			}
			StartCoroutine(LoadActualBossRoom());
		}
		else if (this.name == "text_Moving") {
			if (GameObject.Find ("Music") != null) {
				Destroy(GameObject.Find ("Music")); // Stop menu music
			}
			StartCoroutine(LoadMainHallMoving());
		}
		else if (this.name == "text_Puzzle") {
			if (GameObject.Find ("Music") != null) {
				Destroy(GameObject.Find ("Music")); // Stop menu music
			}
			StartCoroutine(LoadMainHallPuzzle());
		}
		else if (this.name == "CloseButton") {
			Application.Quit();
		}
	}
	
	void OnMouseDown() {
		if (GetComponent<GUITexture>().name == "text_START" || GetComponent<GUITexture>().name == "text_Settings" ||
		    GetComponent<GUITexture>().name == "text_Back" || GetComponent<GUITexture>().name == "text_ClearHighscore" ||
		    GetComponent<GUITexture>().name == "text_Backtomenu" || GetComponent<GUITexture>().name == "text_Help" ||
		    GetComponent<GUITexture>().name == "Left_Button" || GetComponent<GUITexture>().name == "Right_Button" ||
		    GetComponent<GUITexture>().name == "text_One" || GetComponent<GUITexture>().name == "text_Two" ||
		    GetComponent<GUITexture>().name == "text_Three" || GetComponent<GUITexture>().name == "text_Boss") {
		    
			GetComponent<GUITexture>().texture = button2;
		}
	}
	
	IEnumerator Loading() {
		loading.enabled = true;
		yield break;
	}
	IEnumerator LoadSettings() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("settings");
		yield break;
	}
	
	IEnumerator LoadLevelSelect() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("LevelSelect");
		yield break;
	}
	
	IEnumerator LoadMainMenu() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("mainMenu");
		yield break;
	}
	
	IEnumerator LoadMainHall() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("MainHall_");
		yield break;
	}
	
	IEnumerator LoadDiningHall() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		PlayerPrefs.SetInt ("currentScore", 0);
		Application.LoadLevel ("DiningHall");
		yield break;
	}
	
	IEnumerator LoadBossRoom() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		PlayerPrefs.SetInt ("currentScore", 0);
		Application.LoadLevel ("BossRoom");
		yield break;
	}
	
	IEnumerator LoadActualBossRoom() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		PlayerPrefs.SetInt ("currentScore", 0);
		Application.LoadLevel ("ActualBossRoom");
		yield break;
	}
	
	IEnumerator LoadHelp1() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen1");
		yield break;
	}
	
	IEnumerator LoadHelp2() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen2");
		yield break;
	} 
	IEnumerator LoadHelp3() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen3");
		yield break;
	}

	IEnumerator LoadMainHallMoving() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("MainHall_DemoMove");
		yield break;
	}

	IEnumerator LoadMainHallPuzzle() {
		GetComponent<AudioSource>().PlayOneShot(menuButton);
		GetComponent<GUITexture>().texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("MainHall_DemoPuzzle");
		yield break;
	}
}