using UnityEngine;
using System.Collections;

// Handles shield and reloading behaviour

public class Shield : MonoBehaviour {
	public GameObject shield;
	public GunDisplay gunDisplayScript;
	public GUITexture useShieldButton;
	private bool shieldIsUp = false;
	private Vector3 shieldMoveVector = new Vector3(0,0.45F,0); // shield move distance is here
	private float range = 0f;
	
	public bool isReloading = false;

	// Sound variables
	// -------------
	public AudioClip pistolReload;
	public AudioClip hmgReload;
	public AudioClip shotgunReload;
	// -------------
	
	public GameObject mainCamera;
	public EventManager_ActualBossRoom bossRoomScript;
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
							if(bossRoomScript.atPillar == 1) {
								if (bossRoomScript.bossInMiddle == false) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(-43.08f, 3.35f, 61.67f));
									directionVector = (mainCamera.transform.position - new Vector3(-43.08f, 3.35f, 61.67f)).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(-30.22f, 3.35f, 67.8f));
									directionVector = (mainCamera.transform.position - new Vector3(-30.22f, 3.35f, 67.8f)).normalized;
								}
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							}
							else if(bossRoomScript.atPillar == 2) {
								if (bossRoomScript.bossInMiddle == false) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(-41.45f, 3.35f, -62.69f));
									directionVector = (mainCamera.transform.position - new Vector3(-41.45f, 3.35f, -62.69f)).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(-32f, 3.35f, -61.97f));
									directionVector = (mainCamera.transform.position - new Vector3(-32f, 3.35f, -61.97f)).normalized;
								}
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							}
							else if(bossRoomScript.atPillar == 3) {
								if (bossRoomScript.bossInMiddle == false) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(8.33f, 3.35f, -52.95f));
									directionVector = (mainCamera.transform.position - new Vector3(8.33f, 3.35f, -52.95f)).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(22.8f, 3.35f, -54.66f));
									directionVector = (mainCamera.transform.position - new Vector3(22.8f, 3.35f, -54.66f)).normalized;
								}
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							}
							else if(bossRoomScript.atPillar == 4) {
								if (bossRoomScript.bossInMiddle == false) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(12.14f, 3.35f, 51.47f));
									directionVector = (mainCamera.transform.position - new Vector3(12.14f, 3.35f, 51.47f)).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, new Vector3(23.43f, 3.35f, 57.17f));
									directionVector = (mainCamera.transform.position - new Vector3(23.43f, 3.35f, 57.17f)).normalized;
								}
								
								if (range < 1.0f) {
									shieldIsUp = true;
									PlayerPrefs.SetInt ("shieldUp", 1);
								}
								else {
									mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
								}
							} 
						}
						// Use the shield in any other level
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
						if(bossRoomScript.atPillar == 1) {
							if (bossRoomScript.bossInMiddle == false) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.5f, 3.35f, 57.94f));
								directionVector = (mainCamera.transform.position - new Vector3(-46.5f, 3.35f, 57.94f)).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(-22.59f, 3.35f, 67.16f));
								directionVector = (mainCamera.transform.position - new Vector3(-22.59f, 3.35f, 67.16f)).normalized;
							}
							
							if (range < 1.0f) {
								shieldIsUp = false;
								PlayerPrefs.SetInt ("shieldUp", 0);
							}
							else {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(bossRoomScript.atPillar == 2) {
							if (bossRoomScript.bossInMiddle == false) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.91f, 3.35f, -53.21f));
								directionVector = (mainCamera.transform.position - new Vector3(-46.91f, 3.35f, -53.21f)).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(-25.3f, 3.35f, -63.28f));
								directionVector = (mainCamera.transform.position - new Vector3(-25.3f, 3.35f, -63.28f)).normalized;
							}
							
							if (range < 1.0f) {
								shieldIsUp = false;
								PlayerPrefs.SetInt ("shieldUp", 0);
							}
							else {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(bossRoomScript.atPillar == 3) {
							if (bossRoomScript.bossInMiddle == false) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(3.78f, 3.35f, -48.79f));
								directionVector = (mainCamera.transform.position - new Vector3(3.78f, 3.35f, -48.79f)).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(13.51f, 3.35f, -55.73f));
								directionVector = (mainCamera.transform.position - new Vector3(13.51f, 3.35f, -55.73f)).normalized;
							}
								
							if (range < 1.0f) {
								shieldIsUp = false;
								PlayerPrefs.SetInt ("shieldUp", 0);
							}
							else {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(bossRoomScript.atPillar == 4) {
							if (bossRoomScript.bossInMiddle == false) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(8.53f, 3.35f, 47.89f));
								directionVector = (mainCamera.transform.position - new Vector3(8.53f, 3.35f, 47.89f)).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, new Vector3(14.4f, 3.35f, 57.2f));
								directionVector = (mainCamera.transform.position - new Vector3(14.4f, 3.35f, 57.2f)).normalized;
							}
							
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
					if(bossRoomScript.atPillar == 1) {
						if (bossRoomScript.bossInMiddle == false) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.5f, 3.35f, 57.94f));
							directionVector = (mainCamera.transform.position - new Vector3(-46.5f, 3.35f, 57.94f)).normalized;
						}
						else if (bossRoomScript.bossInMiddle == true) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(-22.59f, 3.35f, 67.16f));
							directionVector = (mainCamera.transform.position - new Vector3(-22.59f, 3.35f, 67.16f)).normalized;
						}
						
						if (range > 0.5f) {
							mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
						}
					}
					else if(bossRoomScript.atPillar == 2) {
						if (bossRoomScript.bossInMiddle == false) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(-46.91f, 3.35f, -53.21f));
							directionVector = (mainCamera.transform.position - new Vector3(-46.91f, 3.35f, -53.21f)).normalized;
						}
						else if (bossRoomScript.bossInMiddle == true) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(-25.3f, 3.35f, -63.28f));
							directionVector = (mainCamera.transform.position - new Vector3(-25.3f, 3.35f, -63.28f)).normalized;
						}
						if (range > 0.5f) {
							mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
						}
					}
					else if(bossRoomScript.atPillar == 3) {
						if (bossRoomScript.bossInMiddle == false) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(3.78f, 3.35f, -48.79f));
							directionVector = (mainCamera.transform.position - new Vector3(3.78f, 3.35f, -48.79f)).normalized;
						}
						else if (bossRoomScript.bossInMiddle == true) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(13.51f, 3.35f, -55.73f));
							directionVector = (mainCamera.transform.position - new Vector3(13.51f, 3.35f, -55.73f)).normalized;
						}
						if (range > 0.5f) {
							mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
						}
					}
					else if(bossRoomScript.atPillar == 4) {
						if (bossRoomScript.bossInMiddle == false) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(8.53f, 3.35f, 47.89f));
							directionVector = (mainCamera.transform.position - new Vector3(8.53f, 3.35f, 47.89f)).normalized;
						}
						else if (bossRoomScript.bossInMiddle == true) {
							// Range and direction to move
							range = Vector3.Distance(mainCamera.transform.position, new Vector3(14.4f, 3.35f, 57.2f));
							directionVector = (mainCamera.transform.position - new Vector3(14.4f, 3.35f, 57.2f)).normalized;
						}
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
