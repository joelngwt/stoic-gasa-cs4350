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
			if (GameObject.Find ("Music") != null) {
				Destroy(GameObject.Find ("Music")); // Stop menu music
			}
			StartCoroutine(LoadMainHall());
			StartCoroutine(Loading());
		}
		else if (this.name == "text_Settings"){
			StartCoroutine(LoadSettings ());
		}
		else if (this.name == "text_Back"){
			StartCoroutine(LoadMainMenu ());
		}
		else if (this.name == "text_ClearHighscore"){
			PlayerPrefs.DeleteKey ("highScore");
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
			if(Application.loadedLevelName == "HelpScreen2"){
				StartCoroutine(LoadHelp1 ());
			}
			else if(Application.loadedLevelName == "HelpScreen3"){
				StartCoroutine(LoadHelp2 ());
			}
		}
		else if (this.name == "Right_Button"){ // forward (help screen)
			if(Application.loadedLevelName == "HelpScreen1"){
				StartCoroutine(LoadHelp2 ());
			}
			else if(Application.loadedLevelName == "HelpScreen2"){
				StartCoroutine(LoadHelp3 ());
			}
		}
	}
	
	void OnMouseDown(){
		if (guiTexture.name == "text_START")
			guiTexture.texture = button2;
		else if (guiTexture.name == "text_Settings")
			guiTexture.texture = button2;
		else if (guiTexture.name == "text_Back")
			guiTexture.texture = button2;
		else if (guiTexture.name == "text_ClearHighscore"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "text_Backtomenu"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "text_Help"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "Left_Button"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "Right_Button"){
			guiTexture.texture = button2;
		}
	}
	
	IEnumerator Loading(){
		loading.enabled = true;
		yield break;
	}
	IEnumerator LoadSettings(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("settings");
		yield break;
	}
	
	IEnumerator LoadMainMenu(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("mainMenu");
		yield break;
	}
	
	IEnumerator LoadMainHall(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("MainHall");
		yield break;
	}
	
	IEnumerator LoadHelp1(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen1");
		yield break;
	}
	
	IEnumerator LoadHelp2(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen2");
		yield break;
	}
	IEnumerator LoadHelp3(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen3");
		yield break;
	}
}