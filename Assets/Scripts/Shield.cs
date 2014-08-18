using UnityEngine;
using System.Collections;

// Handles shield and reloading behaviour

public class Shield : MonoBehaviour {
	public GameObject shield;
	private bool shieldIsUp = false;
	private Vector3 shieldMoveVector = new Vector3(0,0.45F,0); // shield move distance is here
	public GunDisplay gunDisplayScript;
	public bool reloading = false;
	public GUITexture useShieldButton;

	// Sound variables
	// -------------
	public AudioClip pistolReload;
	public AudioClip hmgReload;
	public AudioClip shotgunReload;
	// -------------

	void Start(){
		PlayerPrefs.SetInt ( "shieldUp", 0);
	}

	// Update is called once per frame
	void Update () {
		if(Time.timeScale > 0){ // can only shoot if not paused
			
			// Shield usage
			#if UNITY_ANDROID
			if(Input.GetMouseButton(0) && useShieldButton.HitTest(Input.mousePosition)){
			#endif
			#if UNITY_STANDALONE || UNITY_WEBPLAYER
			if(Input.GetKey("space")){
			#endif
				if(shieldIsUp == false){
					if(useShieldButton.name == "UseShield"){
						shieldIsUp = true;
						PlayerPrefs.SetInt ( "shieldUp", 1);
						shield.transform.Translate(shieldMoveVector, Camera.main.transform);
						
						if(gunDisplayScript.currentSelection.Equals ("Pistol")){
							reloading = true; // let the script know that we are reloading
							StartCoroutine(PistolReload()); // call this method
						}
						else if(gunDisplayScript.currentSelection.Equals ("HMG")){
							reloading = true; // let the script know that we are reloading
							StartCoroutine(HMGReload()); // call this method
						}
						else if(gunDisplayScript.currentSelection.Equals ("Shotgun")){
							reloading = true; // let the script know that we are reloading
							StartCoroutine(ShotgunReload()); // call this method
						}
					}
				}
			}
			else {
				if(shieldIsUp == true){
					shieldIsUp = false;
					PlayerPrefs.SetInt ( "shieldUp", 0);
					shield.transform.Translate(-shieldMoveVector, Camera.main.transform);
				}
			}
		}
	}

	// Reloading takes time
	IEnumerator PistolReload(){
		while(gunDisplayScript.ammoCountPistol < 6){
			gunDisplayScript.ammoCountPistol++;
			audio.PlayOneShot(pistolReload);
			yield return new WaitForSeconds(0.1F);
		}
		reloading = false;
		yield break;
	}
	// Reloading takes time
	IEnumerator ShotgunReload(){
		while(gunDisplayScript.ammoCountShotgun < 5){
			if(gunDisplayScript.ammoCountTotalShotgun > 0){
				gunDisplayScript.ammoCountTotalShotgun--;
				gunDisplayScript.ammoCountShotgun++;
				audio.PlayOneShot(shotgunReload);
				yield return new WaitForSeconds(0.5F);
			}
			else{
				break;
			}
		}
		reloading = false;
		yield break;
	}
	
	// Reloading takes time
	IEnumerator HMGReload(){
		while(gunDisplayScript.ammoCountHMG != 40){
			if(gunDisplayScript.ammoCountTotalHMG > 0){
				gunDisplayScript.ammoCountTotalHMG--;
				gunDisplayScript.ammoCountHMG++;
				audio.PlayOneShot(hmgReload);
				yield return new WaitForSeconds(0.05F);
			}
			else{
				break;
			}
		}
		reloading = false;
		yield break;
	}
}
