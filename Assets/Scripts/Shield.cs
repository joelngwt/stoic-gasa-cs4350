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
	private Vector3 directionVector; 

	void Start(){
		PlayerPrefs.SetInt ("shieldUp", 0);
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log(shieldIsUp);
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
							if(eventManagerScript.atPillar == 1) {
								// Range and direction to move
								float range = Vector3.Distance(mainCamera.transform.position, new Vector3(-43.08f, 3.35f, 61.67f));
								directionVector = (mainCamera.transform.position - new Vector3(-43.08f, 3.35f, 61.67f)).normalized;
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							}
							else if(eventManagerScript.atPillar == 2) {
								// Range and direction to move
								float range = Vector3.Distance(mainCamera.transform.position, new Vector3(-41.45f, 3.35f, -62.69f));
								directionVector = (mainCamera.transform.position - new Vector3(-41.45f, 3.35f, -62.69f)).normalized;
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							}
							else if(eventManagerScript.atPillar == 3) {
								// Range and direction to move
								float range = Vector3.Distance(mainCamera.transform.position, new Vector3(8.33f, 3.35f, -52.95f));
								directionVector = (mainCamera.transform.position - new Vector3(8.33f, 3.35f, -52.95f)).normalized;
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							}
							else if(eventManagerScript.atPillar == 4) {
								// Range and direction to move
								float range = Vector3.Distance(mainCamera.transform.position, new Vector3(12.14f, 3.35f, 51.47f));
								directionVector = (mainCamera.transform.position - new Vector3(12.14f, 3.35f, 51.47f)).normalized;
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							} 
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
			// "Space" not pressed
			else {
				if(shieldIsUp == true){
					if (Application.loadedLevelName == "ActualBossRoom") {
						if(eventManagerScript.atPillar == 1) {
							// Range and direction to move
							float range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.5f, 3.35f, 57.94f));
							directionVector = (mainCamera.transform.position - new Vector3(-46.5f, 3.35f, 57.94f)).normalized;
							
							if (range < 1.0f) {
								shieldIsUp = false;
								PlayerPrefs.SetInt ("shieldUp", 0);
							}
							else {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(eventManagerScript.atPillar == 2) {
							// Range and direction to move
							float range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.91f, 3.35f, -53.21f));
							directionVector = (mainCamera.transform.position - new Vector3(-46.91f, 3.35f, -53.21f)).normalized;
							
							if (range < 1.0f) {
								shieldIsUp = false;
								PlayerPrefs.SetInt ("shieldUp", 0);
							}
							else {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(eventManagerScript.atPillar == 3) {
							// Range and direction to move
							float range = Vector3.Distance(mainCamera.transform.position, new Vector3(3.78f, 3.35f, -48.79f));
							directionVector = (mainCamera.transform.position - new Vector3(3.78f, 3.35f, -48.79f)).normalized;
							
							if (range < 1.0f) {
								shieldIsUp = false;
								PlayerPrefs.SetInt ("shieldUp", 0);
							}
							else {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(eventManagerScript.atPillar == 4) {
							// Range and direction to move
							float range = Vector3.Distance(mainCamera.transform.position, new Vector3(8.53f, 3.35f, 47.89f));
							directionVector = (mainCamera.transform.position - new Vector3(8.53f, 3.35f, 47.89f)).normalized;
							
							if (range < 1.0f) {
								shieldIsUp = false;
								PlayerPrefs.SetInt ("shieldUp", 0);
							}
							else {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						} 
					}
					else {
						shieldIsUp = false;
						PlayerPrefs.SetInt ("shieldUp", 0);
						shield.transform.Translate(-shieldMoveVector, Camera.main.transform);
					}
				}
				// Player tapped spacebar and did not hide fully,
				// so move the player back out
				else if (shieldIsUp == false && Application.loadedLevelName == "ActualBossRoom") {
					if(eventManagerScript.atPillar == 1) {
						// Range and direction to move
						float range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.5f, 3.35f, 57.94f));
						directionVector = (mainCamera.transform.position - new Vector3(-46.5f, 3.35f, 57.94f)).normalized;
						
						if (range > 0.5f) {
							mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
						}
					}
					else if(eventManagerScript.atPillar == 2) {
						// Range and direction to move
						float range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.91f, 3.35f, -53.21f));
						directionVector = (mainCamera.transform.position - new Vector3(-46.91f, 3.35f, -53.21f)).normalized;
						
						if (range > 0.5f) {
							mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
						}
					}
					else if(eventManagerScript.atPillar == 3) {
						// Range and direction to move
						float range = Vector3.Distance(mainCamera.transform.position, new Vector3(3.78f, 3.35f, -48.79f));
						directionVector = (mainCamera.transform.position - new Vector3(3.78f, 3.35f, -48.79f)).normalized;
						
						if (range > 0.5f) {
							mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
						}
					}
					else if(eventManagerScript.atPillar == 4) {
						// Range and direction to move
						float range = Vector3.Distance(mainCamera.transform.position, new Vector3(8.53f, 3.35f, 47.89f));
						directionVector = (mainCamera.transform.position - new Vector3(8.53f, 3.35f, 47.89f)).normalized;
						
						if (range > 0.5f) {
							mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
						}
					} 
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
