using UnityEngine;
using System.Collections;

// Handles shield and reloading behaviour

public class Shield : MonoBehaviour {
	// Values for internal use
	public GameObject shield;
	public GunDisplay gunDisplayScript;
	public GUITexture useShieldButton;
	private bool shieldIsUp = false;
	private Vector3 shieldMoveVector = new Vector3(0,0.45F,0); // shield up/down move distance
	private float range = 0f;
	public GameObject mainCamera;
	public EventManager_ActualBossRoom bossRoomScript;
	private Vector3 directionVector;
	public bool isReloading = false;
	
	// Sound variables
	// -------------
	public AudioClip pistolReload;
	public AudioClip hmgReload;
	public AudioClip shotgunReload;
	public AudioClip rocketLauncherReload;
	// -------------

	// Player position during usage of pillars in actual boss level
	private Vector3 movementPillar1BossEdge = new Vector3(-50.45f, 3.35f, 45.14f);
	private Vector3 movementPillar2BossEdge = new Vector3(-53.33f, 3.35f, -46.21f);
	private Vector3 movementPillar3BossEdge = new Vector3(2.11f, 3.35f, -49.65f);
	private Vector3 movementPillar4BossEdge = new Vector3(-2.29f, 3.35f, 52.61f);
	private Vector3 movementPillar1BossMiddle = new Vector3(-29.48f, 3.35f, 67.06f);
	private Vector3 movementPillar2BossMiddle = new Vector3(-28.91f, 3.35f, -63.35f);
	private Vector3 movementPillar3BossMiddle = new Vector3(13.33f, 3.35f, -62.4f);
	private Vector3 movementPillar4BossMiddle = new Vector3(17.73f, 3.35f, 62.47f);
	private Vector3 hidingPillar1BossEdge = new Vector3(-49.06f, 3.35f, 52.98f);
	private Vector3 hidingPillar2BossEdge = new Vector3(-49.88f, 3.35f, -51.69f);
	private Vector3 hidingPillar3BossEdge = new Vector3(5.25f, 3.35f, -55.66f);
	private Vector3 hidingPillar4BossEdge = new Vector3(3.34f, 3.35f, 60.07f);
	private Vector3 hidingPillar1BossMiddle = new Vector3(-36.1f, 3.35f, 64.13f);
	private Vector3 hidingPillar2BossMiddle = new Vector3(-35.73f, 3.35f, -59.63f);
	private Vector3 hidingPillar3BossMiddle = new Vector3(25.09f, 3.35f, -60.49f);
	private Vector3 hidingPillar4BossMiddle = new Vector3(26.89f, 3.35f, 62.39f);
	
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
				#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
				if(Input.GetKey("space")){
					#endif
					if(shieldIsUp == false){
						//if(useShieldButton.name == "UseShield"){
						if (Application.loadedLevelName == "ActualBossRoom") {
							if(bossRoomScript.atPillar == 1) {
								if (bossRoomScript.bossInMiddle == false) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar1BossEdge);
									directionVector = (mainCamera.transform.position - hidingPillar1BossEdge).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar1BossMiddle);
									directionVector = (mainCamera.transform.position - hidingPillar1BossMiddle).normalized;
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
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar2BossEdge);
									directionVector = (mainCamera.transform.position - hidingPillar2BossEdge).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar2BossMiddle);
									directionVector = (mainCamera.transform.position - hidingPillar2BossMiddle).normalized;
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
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar3BossEdge);
									directionVector = (mainCamera.transform.position - hidingPillar3BossEdge).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar3BossMiddle);
									directionVector = (mainCamera.transform.position - hidingPillar3BossMiddle).normalized;
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
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar4BossEdge);
									directionVector = (mainCamera.transform.position - hidingPillar4BossEdge).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, hidingPillar4BossMiddle);
									directionVector = (mainCamera.transform.position - hidingPillar4BossMiddle).normalized;
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
						else if(gunDisplayScript.currentSelection.Equals ("RocketLauncher")){
							if (isReloading == false) {
								StartCoroutine(RocketLauncherReload()); // call this method
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
									range = Vector3.Distance(mainCamera.transform.position, movementPillar1BossEdge);
									directionVector = (mainCamera.transform.position - movementPillar1BossEdge).normalized; // -46.5f, 3.35f, 57.94f
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, movementPillar1BossMiddle);
									directionVector = (mainCamera.transform.position - movementPillar1BossMiddle).normalized;
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
									range = Vector3.Distance(mainCamera.transform.position, movementPillar2BossEdge);
									directionVector = (mainCamera.transform.position - movementPillar2BossEdge).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, movementPillar2BossMiddle);
									directionVector = (mainCamera.transform.position - movementPillar2BossMiddle).normalized;
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
									range = Vector3.Distance(mainCamera.transform.position, movementPillar3BossEdge);
									directionVector = (mainCamera.transform.position - movementPillar3BossEdge).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, movementPillar3BossMiddle);
									directionVector = (mainCamera.transform.position - movementPillar3BossMiddle).normalized;
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
									range = Vector3.Distance(mainCamera.transform.position, movementPillar4BossEdge);
									directionVector = (mainCamera.transform.position - movementPillar4BossEdge).normalized;
								}
								else if (bossRoomScript.bossInMiddle == true) {
									// Range and direction to move
									range = Vector3.Distance(mainCamera.transform.position, movementPillar4BossMiddle);
									directionVector = (mainCamera.transform.position - movementPillar4BossMiddle).normalized;
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
								range = Vector3.Distance(mainCamera.transform.position, movementPillar1BossEdge);
								directionVector = (mainCamera.transform.position - movementPillar1BossEdge).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, movementPillar1BossMiddle);
								directionVector = (mainCamera.transform.position - movementPillar1BossMiddle).normalized;
							}
							
							if (range > 0.5f) {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(bossRoomScript.atPillar == 2) {
							if (bossRoomScript.bossInMiddle == false) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, movementPillar2BossEdge);
								directionVector = (mainCamera.transform.position - movementPillar2BossEdge).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, movementPillar2BossMiddle);
								directionVector = (mainCamera.transform.position - movementPillar2BossMiddle).normalized;
							}
							if (range > 0.5f) {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(bossRoomScript.atPillar == 3) {
							if (bossRoomScript.bossInMiddle == false) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, movementPillar3BossEdge);
								directionVector = (mainCamera.transform.position - movementPillar3BossEdge).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, movementPillar3BossMiddle);
								directionVector = (mainCamera.transform.position - movementPillar3BossMiddle).normalized;
							}
							if (range > 0.5f) {
								mainCamera.transform.Translate(-directionVector * Time.deltaTime * 20.0f, Space.World);
							}
						}
						else if(bossRoomScript.atPillar == 4) {
							if (bossRoomScript.bossInMiddle == false) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, movementPillar4BossEdge);
								directionVector = (mainCamera.transform.position - movementPillar4BossEdge).normalized;
							}
							else if (bossRoomScript.bossInMiddle == true) {
								// Range and direction to move
								range = Vector3.Distance(mainCamera.transform.position, movementPillar4BossMiddle);
								directionVector = (mainCamera.transform.position - movementPillar4BossMiddle).normalized;
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
			#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
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
	
	// Reloading takes time
	IEnumerator RocketLauncherReload(){
		isReloading = true;
		while(gunDisplayScript.ammoCountRocketLauncher < Constants.ROCKET_MAGAZINE_SIZE){
			if(gunDisplayScript.ammoCountTotalRocketLauncher > 0){
				gunDisplayScript.ammoCountTotalRocketLauncher--;
				gunDisplayScript.ammoCountRocketLauncher++;
				audio.PlayOneShot(rocketLauncherReload);
				yield return new WaitForSeconds(Constants.ROCKET_RELOAD_SPEED);
			}
			else{
				break;
			}
		}
		isReloading = false;
		yield break;
	}
}
