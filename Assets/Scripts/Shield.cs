using UnityEngine;
using System.Collections;

// Handles shield and reloading behaviour

public class Shield : MonoBehaviour {
	public GameObject shield;
	public GunDisplay gunDisplayScript;
	public GUITexture useShieldButton;
	private bool shieldIsUp = false;
	private Vector3 shieldMoveVector = new Vector3(0,0.45F,0); // shield move distance is here
	
	public bool isReloading = false;

	// Sound variables
	// -------------
	public AudioClip pistolReload;
	public AudioClip hmgReload;
	public AudioClip shotgunReload;
	// -------------
	
	public GameObject mainCamera;
	public EventManager_ActualBossRoom eventManagerScript;

	void Start(){
		PlayerPrefs.SetInt ("shieldUp", 0);
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
					//if(useShieldButton.name == "UseShield"){
						if (Application.loadedLevelName == "ActualBossRoom") {
							Debug.Log ("Continously running");
						}
						else {
							shieldIsUp = true;
							PlayerPrefs.SetInt ("shieldUp", 1);
							shield.transform.Translate(shieldMoveVector, Camera.main.transform);
						}
						
						if(gunDisplayScript.currentSelection.Equals ("Pistol")){
							if (isReloading == false) {
								StartCoroutine(PistolReload()); // call this method
							}
						}
						else if(gunDisplayScript.currentSelection.Equals ("HMG")){
							if (isReloading == false) {
								StartCoroutine(HMGReload()); // call this method
							}
						}
						else if(gunDisplayScript.currentSelection.Equals ("Shotgun")){
							if (isReloading == false) {
								StartCoroutine(ShotgunReload()); // call this method
							}
						}
					//}
				}
			}
			else {
				if(shieldIsUp == true){
					shieldIsUp = false;
					PlayerPrefs.SetInt ("shieldUp", 0);
					shield.transform.Translate(-shieldMoveVector, Camera.main.transform);
				}
			}
		#if UNITY_ANDROID
		}
		#endif
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		}
		#endif
	}

	// Reloading takes time
	IEnumerator PistolReload(){
		isReloading = true;
		while(gunDisplayScript.ammoCountPistol < Constants.PISTOL_MAGAZINE_SIZE){
			gunDisplayScript.ammoCountPistol++;
			audio.PlayOneShot(pistolReload);
			yield return new WaitForSeconds(Constants.PISTOL_RELOAD_SPEED);
		}
		isReloading = false;
		yield break;
	}
	// Reloading takes time
	IEnumerator ShotgunReload(){
		isReloading = true;
		while(gunDisplayScript.ammoCountShotgun < Constants.SHOTGUN_MAGAZINE_SIZE){
			if(gunDisplayScript.ammoCountTotalShotgun > 0){
				gunDisplayScript.ammoCountTotalShotgun--;
				gunDisplayScript.ammoCountShotgun++;
				audio.PlayOneShot(shotgunReload);
				yield return new WaitForSeconds(Constants.SHOTGUN_RELOAD_SPEED);
			}
			else{
				break;
			}
		}
		isReloading = false;
		yield break;
	}
	
	// Reloading takes time
	IEnumerator HMGReload(){
		isReloading = true;
		while(gunDisplayScript.ammoCountHMG != Constants.HMG_MAGAZINE_SIZE){
			if(gunDisplayScript.ammoCountTotalHMG > 0){
				gunDisplayScript.ammoCountTotalHMG--;
				gunDisplayScript.ammoCountHMG++;
				audio.PlayOneShot(hmgReload);
				yield return new WaitForSeconds(Constants.HMG_RELOAD_SPEED);
			}
			else{
				break;
			}
		}
		isReloading = false;
		yield break;
	}
}
