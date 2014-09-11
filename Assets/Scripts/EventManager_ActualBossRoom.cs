using UnityEngine;
using System.Collections;

public class EventManager_ActualBossRoom : MonoBehaviour {

	public float RotationSpeed = 15;
	public float movementSpeed = 25;
	
	//values for internal use
	private Quaternion _lookRotation;	// Rotational angle
	private Vector3 _direction;			// Directional vector
	private GameObject theCamera;		// Game camera
	private int num;					// Keep track of movement sequence
	public GameObject theCharacter;		// Character object
	public int atPillar; 				// Keep track of what pillar the player is behind
	//public GameObject pillar2Sparkle;
	//public GameObject pillar3Sparkle;
	//public GameObject pillar4Sparkle;
	public int bossThrowBombAt;
	
	private bool bossInMiddle;
	private Vector3 movementCenterPoint = new Vector3(-41.13f, 3.35f, -0.77f);
	private Vector3 movementPillar1BossEdge = new Vector3(-46.5f, 3.35f, 57.94f);
	private Vector3 movementPillar2BossEdge = new Vector3(-46.91f, 3.35f, -53.21f);
	private Vector3 movementPillar3BossEdge = new Vector3(8.74f, 3.35f, -52.83f);
	private Vector3 movementPillar4BossEdge = new Vector3(11.17f, 3.35f, 50.51f);
	private Vector3 movementPillar1BossMiddle;
	private Vector3 movementPillar2BossMiddle;
	private Vector3 movementPillar3BossMiddle;
	private Vector3 movementPillar4BossMiddle;
	private Vector3 lookAtPillar1BossEdge = new Vector3(17.86f, 3.35f, 2.1f);
	private Vector3 lookAtPillar2BossEdge = new Vector3(1.69f, 3.35f, -41.99f);
	private Vector3 lookAtPillar3BossEdge = new Vector3(25.32f, 3.35f, -38.41f);
	private Vector3 lookAtPillar4BossEdge = new Vector3(47.58f, 3.35f, -0.77f);
	private Vector3 lookAtPillar1BossMiddle;
	private Vector3 lookAtPillar2BossMiddle;
	private Vector3 lookAtPillar3BossMiddle;
	private Vector3 lookAtPillar4BossMiddle;
	
	// Audio
	public AudioClip footsteps;
	
	// Disable these scripts while moving
	public Shooting shootScript;
	public Shield shieldScript;
	
	// Need to access these scripts to save info
	public InGameScoreScript scoreScript;
	public GunDisplay gunScript;
	public LifeCounter lifeScript;
	public TimerScript timeScript;
	
	// Spawning
	public GameObject bearPrefab;
	private int count;
	public GameObject lollipopPrefab;
	//public GameObject EggPrefab;
	private bool reached = false;
	
	// Boss AI
	public BossAI bossAIScript;
	
	void Start(){
		theCamera = Camera.main.gameObject;
		theCharacter = GameObject.FindWithTag("MainCharacter");
		audio.clip = footsteps;
		num = 4;
		theCharacter.transform.rotation = theCamera.transform.rotation;
		count = 1;
		atPillar = 0;
		bossThrowBombAt = 0;

		//pillar2Sparkle.particleEmitter.enabled = false;
		//pillar3Sparkle.particleEmitter.enabled = false;
		//pillar4Sparkle.particleEmitter.enabled = false;
		
		bossInMiddle = false;
		shootScript.haveLooked = false;
	}
	
	// Update is called once per frame
	void Update () {
		// character hitbox follows the camera around
		theCharacter.transform.position = theCamera.transform.position;
		
		Debug.Log ("reached = " + shootScript.haveReached + ", At pillar = " + atPillar);
		
		if (shootScript.shotPillar2 == true && shootScript.haveLooked == false) {
			//moveToCenter();
			if (shootScript.haveReached == false) {
				moveToPillar (2);
			}
			if (shootScript.haveReached == true && shootScript.haveLooked == false) {
				LookAt_Pillar(lookAtPillar2BossEdge);
			}
		}
		else if (shootScript.shotPillar4 == true && shootScript.haveLooked == false) {
			//moveToCenter();
			if (shootScript.haveReached == false) {
				moveToPillar (4);
			}
			if (shootScript.haveReached == true && shootScript.haveLooked == false) {
				LookAt_Pillar(lookAtPillar4BossEdge);
			}
		}
					
		if (num == 0) { // Walk into room
			num = TranslateTo( new Vector3(-65.9f, 3.35f, 69.3f), 50f, num);
		}
		else if (num == 1) { // Walk to intersection of carpet
			num = TranslateTo( new Vector3(-65.43f, 3.35f, 0.712f), 50f, num);
		}
		else if (num == 2) { // Look at boss
			num = LookAt( new Vector3(-35.1f, 3.35f, 2.1f), num);
		}
		else if (num == 3) {
			startStorySequence();
		}
		else if (num == 4) { // Walk behind pillar
			num = TranslateTo(movementPillar1BossEdge, 50f, num);
		}
		else if (num == 5) { // Look in the boss direction (prepare for battle!)
			num = LookAt( new Vector3(17.86f, 3.35f, 2.1f), num);
			atPillar = 1;
			// Fight begins
			bossAIScript.fightStart = true;
		}
		// Boss reaches 80% health
		else if (num == 6 && bossAIScript.percentage < 0.8f) {
			bossThrowBombAt = 1;
			//pillar2Sparkle.particleEmitter.enabled = true;
			//pillar3Sparkle.particleEmitter.enabled = true;
		}
	}
	
	// TODO
	private void startStorySequence() {
		if(Input.GetMouseButtonDown(0)) {
			num += 1;
		}
	}
	
	private void moveToCenter() {
		TranslateTo(movementCenterPoint, 50f, 0);
	}
	
	private void moveToPillar(int pillarNumber) {
		if (pillarNumber == 1 && bossInMiddle == false) {
			TranslateToPillar(movementPillar1BossEdge, 50f);
			atPillar = 1;
		}
		else if (pillarNumber == 2 && bossInMiddle == false) {
			TranslateToPillar(movementPillar2BossEdge, 50f);
			atPillar = 2;
		}
		else if (pillarNumber == 3 && bossInMiddle == false) {
			TranslateTo(movementPillar3BossEdge, 50f, 0);
			atPillar = 3;
		}
		else if (pillarNumber == 4 && bossInMiddle == false) {
			TranslateTo(movementPillar4BossEdge, 50f, 0);
			atPillar = 4;
		}
		else if (pillarNumber == 1 && bossInMiddle == true) {
			atPillar = 1;
		}
		else if (pillarNumber == 2 && bossInMiddle == true) {
			atPillar = 2;
		}
		else if (pillarNumber == 3 && bossInMiddle == true) {
			atPillar = 3;
		}
		else if (pillarNumber == 4 && bossInMiddle == true) {
			atPillar = 4;
		}
	}
	
	private int LookAt( Vector3 position , int num) {
		
		//find the vector pointing from our position to the target
		_direction = (position - transform.position);//.normalized;
		
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		theCharacter.transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		
		if (Mathf.Abs(Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f) {
			num += 1;
			reached = true;
		}
		
		return num;
	}
	
	private int TranslateTo( Vector3 position , float spd, int num) {
		
		// Calculate the distance between the follower and the leader.
		float range1 = Vector3.Distance(theCamera.transform.position, position );
		//Debug.Log ("Range = " + range1);
		
		if (range1 > 1.0) {
			// prevent shooting and using the shield while moving
			shootScript.enabled = false;
			shieldScript.enabled = false;
			
			//find the vector pointing from our position to the target
			_direction = (position - transform.position).normalized;
			
			//create the rotation we need to be in to look at the target
			_lookRotation = Quaternion.LookRotation (_direction);
			
			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
			// calculate direction and move towards the target
			Vector3 dir = position - theCamera.transform.position;
			dir = dir.normalized;
			
			if(!audio.isPlaying){
				audio.Play ();
			}
			
			//theCharacter.transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
			theCamera.transform.Translate (dir * spd * Time.deltaTime, Space.World);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
		} else if (range1 <= 1.0) {
			// Reached position, enable shielding and shooting
			shootScript.enabled = true;
			shieldScript.enabled = true;
			
			audio.Stop ();
			num += 1;
			Debug.Log (num);
			
		}
		return num;
	}
	
	private void LookAt_Pillar(Vector3 position) {
		//find the vector pointing from our position to the target
		_direction = (position - transform.position);//.normalized;
		
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		theCharacter.transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		
		if (Mathf.Abs(Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f) {
			reached = true;
			shootScript.haveLooked = true;
		}
	}
	
	private void TranslateToPillar( Vector3 position , float spd) {
		// Calculate the distance between the follower and the leader.
		float range1 = Vector3.Distance(theCamera.transform.position, position );
		//Debug.Log ("Range = " + range1);
		
		if (range1 > 1.0) {
			// prevent shooting and using the shield while moving
			shootScript.enabled = false;
			shieldScript.enabled = false;
			
			//find the vector pointing from our position to the target
			_direction = (position - transform.position).normalized;
			
			//create the rotation we need to be in to look at the target
			_lookRotation = Quaternion.LookRotation (_direction);
			
			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
			// calculate direction and move towards the target
			Vector3 dir = position - theCamera.transform.position;
			dir = dir.normalized;
			
			if(!audio.isPlaying){
				audio.Play ();
			}
			
			//theCharacter.transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
			theCamera.transform.Translate (dir * spd * Time.deltaTime, Space.World);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
		} else if (range1 <= 1.0) {
			// Reached position, enable shielding and shooting
			shootScript.enabled = true;
			shieldScript.enabled = true;
			shootScript.haveReached = true;
			
			audio.Stop ();			
		}
	}
	
	private void spawnBear(Vector3 position, int cover){
		GameObject bear = Instantiate(bearPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		bear.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = camera.transform.position - bear.transform.position; // make the instantiated bear face the camera
		bear.transform.rotation = Quaternion.LookRotation(direction);
		
		Enemy e = bear.GetComponent<Enemy>();
		e.coverType = cover;
		
		count++;
	}
	
	private void spawnLollipop(Vector3 position){
		GameObject lollipop = Instantiate(lollipopPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		lollipop.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = camera.transform.position - lollipop.transform.position; // make the instantiated bear face the camera
		lollipop.transform.rotation = Quaternion.LookRotation(direction);
		count++;
	}
	
	/*
	private void spawnEgg(Vector3 position){
		GameObject egg = Instantiate(EggPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		egg.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = camera.transform.position - egg.transform.position; // make the instantiated bear face the camera
		egg.transform.rotation = Quaternion.LookRotation(direction);
		count++;
	}
	*/
	
	private void saveGame(){
		PlayerPrefs.SetInt ("currentScore", (int)scoreScript.currentScore);
		PlayerPrefs.SetInt ("HMGTotalAmmo", (int)gunScript.ammoCountTotalHMG);
		PlayerPrefs.SetInt ("ShotgunTotalAmmo", (int)gunScript.ammoCountTotalShotgun);
		PlayerPrefs.SetInt ("HMGAmmo", (int)gunScript.ammoCountHMG);
		PlayerPrefs.SetInt ("ShotgunAmmo", (int)gunScript.ammoCountShotgun);
		PlayerPrefs.SetInt("playedTakeDamage", (int)lifeScript.playedTakeDamage);
		PlayerPrefs.SetInt("timeLeft", (int)timeScript.seconds);
		PlayerPrefs.SetInt("playerLoadedHealth", (int)lifeScript.playerHealth);
		Debug.Log ("Game saved");
	}
}
