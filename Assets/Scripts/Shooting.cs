using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	// Gun variables
	// -------------
	public GameObject bullethole;			// particle effect
	private int ammoCount;
	public GunDisplay gunDisplayScript;
	protected float fireRateHMG = Constants.HMG_SHOOT_SPEED;	// Rate of fire for the HMG
	protected float nextFireHMG = Constants.HMG_SHOOT_SPEED;	// Rate of fire for the HMG
	protected float fireRateRocketLauncher = Constants.ROCKET_SHOOT_SPEED;	// Rate of fire for the rocket launcher
	protected float nextFireRocketLauncher = Constants.ROCKET_SHOOT_SPEED;	// Rate of fire for the rocket launcher
	private bool shotgunShooting = false;
	public Vector3 positionShot;			// used for the rocket launcher's missile
	public GameObject rocketMissile;		// missile prefab that the rocket launcher shoots out
	// -------------

	// Shield variables
	// -------------
	public Shield shieldScript;
	// -------------

	// Score variables
	// -------------
	public InGameScoreScript scoreScript;
	public GameObject plus10;
	public GameObject plus20;
	public GameObject plus30;
	// -------------
	
	// Sound variables
	// -------------
	public AudioClip pistolShoot;
	public AudioClip HMGShoot;
	public AudioClip shotgunShoot;
	public AudioClip rocketLauncherShoot1;
	public AudioClip rocketLauncherShoot2;
	// -------------
	
	// Boost activation
	// -------------
	private bool isBoosted = false;
	// -------------
	
	// Pickups
	// -------------
	public PickupBehaviour pickupScript;
	// -------------
	
	// Boss Room variables
	// -------------
	public bool shotPillar1 = false;
	public bool shotPillar2 = false;
	public bool shotPillar3 = false;
	public bool shotPillar4 = false;
	public bool haveReached = false;
	public bool haveLooked = false;
	private EventManager_ActualBossRoom bossRoomScript;
	[SerializeField] private CrackedRoof crackedRoofScript;
	private BossAI bossAIScript;
	// -------------
	
	// Other
	// -------------
	[SerializeField] private GUITexture pauseButton;
	// -------------
	
	void Start() {
		if (Application.loadedLevelName == "ActualBossRoom") {
			bossRoomScript = GameObject.FindWithTag ("MainCamera").GetComponent<EventManager_ActualBossRoom>();
			bossAIScript = GameObject.FindWithTag ("Boss").GetComponent<BossAI>();
		}
	}		
	
	// Update is called once per frame
	void Update () {
	
		Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// Multiple raycasts for shotgun (fixed, constant bullet spread)
		// ----------
		Ray myRay2 = Camera.main.ScreenPointToRay(Input.mousePosition - new Vector3(40,40,40));
		RaycastHit hit2;

		Ray myRay3 = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(-40,40,-40));
		RaycastHit hit3;

		Ray myRay4 = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(40,0,40));
		RaycastHit hit4;

		Ray myRay5 = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0,40,40));
		RaycastHit hit5;
		// ----------

		if(Time.timeScale > 0){ // can only shoot if not paused
			// if gun is pistol
			if(gunDisplayScript.currentSelection == "Pistol")
			{
				if(!pauseButton.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
				{
					if(Physics.Raycast(myRay,out hit) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountPistol > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit.point, Quaternion.identity);		
							Debug.DrawRay(myRay.origin, myRay.direction*hit.distance, Color.red);
							if (isBoosted == false ) {
								gunDisplayScript.ammoCountPistol--; // decrease ammo count
							}
							audio.PlayOneShot(pistolShoot);
			
							hitDetection(hit.transform.gameObject);
						}
					}
				}
			}
			
			// if gun is HMG
			if(gunDisplayScript.currentSelection == "HMG")
			{
				if(!pauseButton.HitTest(Input.mousePosition) && Input.GetMouseButton(0) && Time.time - nextFireHMG > fireRateHMG && GUIUtility.hotControl == 0)
				{

					if(Physics.Raycast(myRay,out hit) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountHMG > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit.point, Quaternion.identity);		
							Debug.DrawRay(myRay.origin, myRay.direction*hit.distance, Color.red);
							audio.PlayOneShot(HMGShoot);
			
							hitDetection(hit.transform.gameObject);

							if (isBoosted == false) {
								gunDisplayScript.ammoCountHMG--; // decrease ammo count
							}
							nextFireHMG = Time.time + fireRateHMG; // shooting delay
						}
					}
				}
			}
			// If gun is shotgun
			if(gunDisplayScript.currentSelection == "Shotgun")
			{
				if(!pauseButton.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0) && shotgunShooting == false && shieldScript.isReloading == false && GUIUtility.hotControl == 0)
				{
									
					shotgunShooting = true; // let the script know that we are shooting with the shotgun
					StartCoroutine(ShotgunShooting()); // call this method

					// Bullet/raycast 1
					if(Physics.Raycast(myRay,out hit) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							if (isBoosted == false) {
								gunDisplayScript.ammoCountShotgun--; // decrease ammo count 
							}
							Instantiate(bullethole, hit.point, Quaternion.identity);		
							Debug.DrawRay(myRay.origin, myRay.direction*hit.distance, Color.red);
							audio.PlayOneShot(shotgunShoot);
			
							hitDetection(hit.transform.gameObject);
						}
					}

					// Bullet/raycast 2
					if(Physics.Raycast(myRay2,out hit2) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit2.point, Quaternion.identity);		
							Debug.DrawRay(myRay2.origin, myRay2.direction*hit2.distance, Color.red);
							audio.PlayOneShot(shotgunShoot);
							
							hitDetection(hit2.transform.gameObject);
						}
					}

					// Bullet/raycast 3
					if(Physics.Raycast(myRay3, out hit3) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit3.point, Quaternion.identity);		
							Debug.DrawRay(myRay3.origin, myRay3.direction*hit3.distance, Color.red);
							audio.PlayOneShot(shotgunShoot);
							
							hitDetection(hit3.transform.gameObject);
						}
					}

					// Bullet/raycast 4
					if(Physics.Raycast(myRay4, out hit4) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit4.point, Quaternion.identity);		
							Debug.DrawRay(myRay4.origin, myRay4.direction*hit4.distance, Color.red);
							
							hitDetection(hit4.transform.gameObject);
						}
					}

					// Bullet/raycast 5
					if(Physics.Raycast(myRay5,out hit5) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit5.point, Quaternion.identity);		
							Debug.DrawRay(myRay5.origin, myRay5.direction*hit5.distance, Color.red);
							
							hitDetection(hit5.transform.gameObject);
						}
					}
				}
			}
			else if (gunDisplayScript.currentSelection == "RocketLauncher") {
				if(!pauseButton.HitTest(Input.mousePosition) && Input.GetMouseButton(0) && Time.time - nextFireRocketLauncher > fireRateRocketLauncher && GUIUtility.hotControl == 0)
				{
					
					if(Physics.Raycast(myRay,out hit) && shieldScript.isReloading == false) {
						if(gunDisplayScript.ammoCountRocketLauncher > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							positionShot = hit.point;	
							Instantiate(rocketMissile, this.transform.position, Quaternion.Euler(0f, 0f, 0f));
							Debug.DrawRay(myRay.origin, myRay.direction*hit.distance, Color.red);
							if (Random.Range (0, 100) < 50) {
								audio.PlayOneShot(rocketLauncherShoot1);
							}
							else {
								audio.PlayOneShot(rocketLauncherShoot2);
							}
							if (isBoosted == false) {
								gunDisplayScript.ammoCountRocketLauncher--; // decrease ammo count
							}
							nextFireRocketLauncher = Time.time + fireRateRocketLauncher; // shooting delay
						}
					}
				}
			}
		}
	}

	// Delay for shooting with a shotgun
	IEnumerator ShotgunShooting(){
		yield return new WaitForSeconds(Constants.SHOTGUN_SHOOT_SPEED);
		shotgunShooting = false;
		yield break;
	}
	
	IEnumerator Plus10(GameObject thingHit){
		if(thingHit.tag == "Enemy" && Application.loadedLevelName == "MainHall"){
			Instantiate(plus10, new Vector3(thingHit.transform.position.x, thingHit.transform.position.y+12f, thingHit.transform.position.z), thingHit.transform.rotation); 
		}
		else{
			Instantiate(plus10, new Vector3(thingHit.transform.position.x, thingHit.transform.position.y+5f, thingHit.transform.position.z), thingHit.transform.rotation); 
		}
		scoreScript.currentScore += Constants.SCORE_BEAR;
		yield break;
	}
	
	IEnumerator Plus20(GameObject thingHit){
		Instantiate(plus20, thingHit.transform.position, thingHit.transform.rotation); 
		scoreScript.currentScore += Constants.SCORE_LOLLIPOP;
		yield break;
	}
	
	IEnumerator Plus30(GameObject thingHit){
		Instantiate(plus30, thingHit.transform.position, thingHit.transform.rotation); 
		scoreScript.currentScore += Constants.SCORE_EGG;
		yield break;
	}
	
	IEnumerator BoostTimer(){
		isBoosted = true;
		yield return new WaitForSeconds(Constants.BOOST_TIME);
		isBoosted = false;
		yield break;
	}
	
	public void hitDetection(GameObject hitGameObject){
		if(hitGameObject.tag == "Enemy") {
			GameObject target = hitGameObject;
			StartCoroutine(Plus10(target));
			Enemy script = target.GetComponent<Enemy>();
			script.StartAnim();
		}
		else if(hitGameObject.tag == "EnemyLollipop") {
			GameObject target = hitGameObject;
			StartCoroutine(Plus20(target));
			EnemyLollipop script = target.GetComponent<EnemyLollipop>();
			script.StartAnim();
		}
		else if(hitGameObject.tag == "EnemyEgg") {
			GameObject target = hitGameObject;
			StartCoroutine(Plus30(target));
			EnemyEgg script = target.GetComponent<EnemyEgg>();
			script.StartAnim();
		}
		else if (hitGameObject.tag == "HealthPickup" || hitGameObject.tag == "AmmoPickup") {
			GameObject pickup = hitGameObject;
			PickupBehaviour script = pickup.GetComponent<PickupBehaviour>();
			//hitGameObject.rigidbody.AddForce(Vector3.up * 5000.0f);
			script.canMove = true;
		}
		else if (hitGameObject.tag == "BoostPickup") {
			GameObject pickup = hitGameObject;
			PickupBehaviour script = pickup.GetComponent<PickupBehaviour>();
			//hitGameObject.rigidbody.AddForce(Vector3.up * 5000.0f);
			script.canMove = true;
			
			StartCoroutine(BoostTimer());
		}
		else if (hitGameObject.tag == "Boss" && bossAIScript.behindChair == false && bossAIScript.movingToMiddle == false) {
			GameObject target = hitGameObject;
			BossAI script = target.GetComponent<BossAI>();
			StartCoroutine(Plus30(target));
			
			script.getHit();
		}
		else if (hitGameObject.tag == "BossBack" && bossAIScript.behindChair == false && bossAIScript.movingToMiddle == false) {
			GameObject target = hitGameObject;
			BossAI script = target.GetComponentInParent<BossAI>();
			StartCoroutine(Plus30(target));
			
			script.getHitBack();
		}
		else if (hitGameObject.tag == "Pillar1" && bossRoomScript.atPillar != 1) {
			shotPillar1 = true;
			shotPillar2 = false;
			shotPillar3 = false;
			shotPillar4 = false;
			haveReached = false;
			haveLooked = false;
		}
		else if (hitGameObject.tag == "Pillar2" && bossRoomScript.atPillar != 2) {
			shotPillar1 = false;
			shotPillar2 = true;
			shotPillar3 = false;
			shotPillar4 = false;
			haveReached = false;
			haveLooked = false;
		}
		else if (hitGameObject.tag == "Pillar3" && bossRoomScript.atPillar != 3) {
			shotPillar1 = false;
			shotPillar2 = false;
			shotPillar3 = true;
			shotPillar4 = false;
			haveReached = false;
			haveLooked = false;
		}
		else if (hitGameObject.tag == "Pillar4" && bossRoomScript.atPillar != 4) {
			shotPillar1 = false;
			shotPillar2 = false;
			shotPillar3 = false;
			shotPillar4 = true;
			haveReached = false;
			haveLooked = false;
		}
		else if (hitGameObject.tag == "CrackedRoof") {
			crackedRoofScript.health -= 1;
		}
		else if(hitGameObject.tag == "Possession_Cube")
		{
			hitGameObject.GetComponent<Possession_Cube>().trigger_possession();
		}
		else if(hitGameObject.name.Equals("Equation_Plus"))
		{
			hitGameObject.GetComponent<Button_Trigger>().activated = true;
		}
		else if(hitGameObject.name.Equals("Equation_Subtract"))
		{
			hitGameObject.GetComponent<Button_Trigger>().activated = true;
		}
		else if(hitGameObject.name.Equals("Die_1") 
		        || hitGameObject.name.Equals("Die_2") 
		        || hitGameObject.name.Equals("Die_3") )
		{
			hitGameObject.GetComponent<Dice_Script>().is_shot = true;
		}
	}
}
