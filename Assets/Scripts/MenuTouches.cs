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
			guiTexture.texture = button1;
			audio.PlayOneShot(menuButton);
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
	}
	
	void OnMouseDown() {
		if (guiTexture.name == "text_START" || guiTexture.name == "text_Settings" ||
		    guiTexture.name == "text_Back" || guiTexture.name == "text_ClearHighscore" ||
		    guiTexture.name == "text_Backtomenu" || guiTexture.name == "text_Help" ||
		    guiTexture.name == "Left_Button" || guiTexture.name == "Right_Button" ||
		    guiTexture.name == "text_One" || guiTexture.name == "text_Two" ||
		    guiTexture.name == "text_Three" || guiTexture.name == "text_Boss") {
		    
			guiTexture.texture = button2;
		}
	}
	
	IEnumerator Loading() {
		loading.enabled = true;
		yield break;
	}
	IEnumerator LoadSettings() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("settings");
		yield break;
	}
	
	IEnumerator LoadLevelSelect() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("LevelSelect");
		yield break;
	}
	
	IEnumerator LoadMainMenu() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("mainMenu");
		yield break;
	}
	
	IEnumerator LoadMainHall() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("MainHall_");
		yield break;
	}
	
	IEnumerator LoadDiningHall() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("DiningHall");
		yield break;
	}
	
	IEnumerator LoadBossRoom() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("BossRoom");
		yield break;
	}
	
	IEnumerator LoadActualBossRoom() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("ActualBossRoom");
		yield break;
	}
	
	IEnumerator LoadHelp1() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen1");
		yield break;
	}
	
	IEnumerator LoadHelp2() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen2");
		yield break;
	} 
	IEnumerator LoadHelp3() {
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen3");
		yield break;
	}
}