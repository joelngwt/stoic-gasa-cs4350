using UnityEngine;
using System.Collections;

public class EventManager_ActualBossRoom : MonoBehaviour {

	private float RotationSpeed = 15;
	private float movementSpeed = 25;
	
	// Values for internal use
	private Quaternion _lookRotation;	// Rotational angle
	private Vector3 _direction;			// Directional vector
	private GameObject theCamera;		// Game camera
	private int num;					// Keep track of movement sequence
	private int speechSequence;			// Keep track of boss speech
	public GameObject theCharacter;		// Character object
	public int atPillar; 				// Keep track of what pillar the player is behind
	private bool canMove;				// Whether or not the player can move from pillar to pillar
	
	// Keep track of which pillars are destroyed
	public bool isPillar1Destroyed;
	public bool isPillar2Destroyed;
	public bool isPillar3Destroyed;
	public bool isPillar4Destroyed;
	
	// Target Reticles for roof and pillars
	[SerializeField] private GameObject crackedRoof;
	[SerializeField] private GameObject targetReticleRoof;
	public GameObject targetReticle1to2Edge;
	public GameObject targetReticle1to3Edge;
	public GameObject targetReticle2to1Edge;
	public GameObject targetReticle2to4Edge;
	public GameObject targetReticle3to4Edge;
	public GameObject targetReticle4to3Edge;
	public GameObject targetReticle1to2Middle;
	public GameObject targetReticle1to3Middle;
	public GameObject targetReticle1to4Middle;
	public GameObject targetReticle2to1Middle;
	public GameObject targetReticle2to4Middle;
	public GameObject targetReticle3to1Middle;
	public GameObject targetReticle3to4Middle;
	public GameObject targetReticle4to2Middle;
	public GameObject targetReticle4to3Middle;
	
	// Boss speech boxes
	[SerializeField] private GUITexture bossSpeech1;
	[SerializeField] private GUITexture bossSpeech2;
	[SerializeField] private GUITexture bossSpeech3;
	
	// Movement and look at rotation
	public bool bossInMiddle;
	private Vector3 movementCenterPoint = new Vector3(-41.13f, 3.35f, -0.77f);
	private Vector3 movementPillar1BossEdge = new Vector3(-50.7f, 3.35f, 61.88f);
	private Vector3 movementPillar2BossEdge = new Vector3(-50f, 3.35f, -54.8f);
	private Vector3 movementPillar3BossEdge = new Vector3(2.11f, 3.35f, -49.65f);
	private Vector3 movementPillar4BossEdge = new Vector3(-0.27f, 3.35f, 55.3f);
	private Vector3 movementPillar1BossMiddle = new Vector3(-23.66f, 3.35f, 72.18f);
	private Vector3 movementPillar2BossMiddle = new Vector3(-25.06f, 3.35f, -71.34f);
	private Vector3 movementPillar3BossMiddle = new Vector3(13.33f, 3.35f, -62.4f);
	private Vector3 movementPillar4BossMiddle = new Vector3(13.29f, 3.35f, 65.06f);
	private Vector3 lookAtPillar1BossEdge = new Vector3(17.86f, 3.35f, 2.1f);
	private Vector3 lookAtPillar2BossEdge = new Vector3(-17.17f, 3.35f, -23.97f);
	private Vector3 lookAtPillar3BossEdge = new Vector3(47.58f, 3.35f, -0.77f);
	private Vector3 lookAtPillar4BossEdge = new Vector3(47.58f, 3.35f, -0.77f);
	private Vector3 lookAtPillar1BossMiddle = new Vector3(-7.23f, 8f, 0.76f);
	private Vector3 lookAtPillar2BossMiddle = new Vector3(-7.23f, 8f, 0.76f);
	private Vector3 lookAtPillar3BossMiddle = new Vector3(-7.23f, 8f, 0.76f);
	private Vector3 lookAtPillar4BossMiddle = new Vector3(-7.23f, 8f, 0.76f);
	
	// Audio
	public AudioClip footsteps;
	
	// Disable these scripts while moving
	public Shooting shootScript;
	public Shield shieldScript;
	
	// Need to access these scripts to save/load info
	public InGameScoreScript scoreScript;
	public GunDisplay gunScript;
	public LifeCounter lifeScript;
	public TimerScript timeScript;
	
	// Spawning
	public GameObject bearPrefab;
	private int count;
	public GameObject lollipopPrefab;
	//public GameObject EggPrefab;
	private bool reached;
	public bool minionsKilled;
	
	// Boss AI
	public BossAI bossAIScript;
	
	void Start(){
		theCamera = Camera.main.gameObject;
		theCharacter = GameObject.FindWithTag("MainCharacter");
		audio.clip = footsteps;
		num = 0;				// Origin = 0
		speechSequence = 0;		// Origin = 0
		theCharacter.transform.rotation = theCamera.transform.rotation;
		count = 1;				// Origin = 1
		atPillar = 0;			// Origin = 0
		reached = false;
		minionsKilled = false;
		bossInMiddle = false;
		crackedRoof.SetActive(false);
		targetReticleRoof.SetActive(false);
		bossSpeech1.enabled = false;
		bossSpeech2.enabled = false;
		bossSpeech3.enabled = false;
		
		targetReticle1to2Edge.SetActive(false);;
		targetReticle1to3Edge.SetActive(false);
		targetReticle2to1Edge.SetActive(false);
		targetReticle2to4Edge.SetActive(false);
		targetReticle3to4Edge.SetActive(false);
		targetReticle4to3Edge.SetActive(false);
		targetReticle1to2Middle.SetActive(false);
		targetReticle1to3Middle.SetActive(false);
		targetReticle1to4Middle.SetActive(false);
		targetReticle2to1Middle.SetActive(false);
		targetReticle2to4Middle.SetActive(false);
		targetReticle3to1Middle.SetActive(false);
		targetReticle3to4Middle.SetActive(false);
		targetReticle4to2Middle.SetActive(false);
		targetReticle4to3Middle.SetActive(false);
		
		isPillar1Destroyed = false;
		isPillar2Destroyed = false;
		isPillar3Destroyed = false;
		isPillar4Destroyed = false;
	
		bossInMiddle = false;
		shootScript.haveLooked = false;
	}
	
	// Update is called once per frame
	void Update () {
		// character hitbox follows the camera around
		theCharacter.transform.position = theCamera.transform.position;
		Debug.Log ("num = " + num);
		Debug.Log ("Count = " + count);
		// Debug.Log ("reached = " + shootScript.haveReached + ", At pillar = " + atPillar);
		
		if (bossAIScript.canShootRoof == true) {
			crackedRoof.SetActive(true);
			targetReticleRoof.SetActive(true);
		}
		
		if (shootScript.shotPillar1 == true && shootScript.haveLooked == false) {
			if (shootScript.haveReached == false) {
				moveToPillar (1);
			}
			if (shootScript.haveReached == true && shootScript.haveLooked == false) {
				if (bossInMiddle == false) {
					LookAt_Pillar(lookAtPillar1BossEdge);
				}
				else if (bossInMiddle == true) {
					LookAt_Pillar(lookAtPillar1BossMiddle);
				}
			}
		}
		else if (shootScript.shotPillar2 == true && shootScript.haveLooked == false) {
			if (shootScript.haveReached == false) {
				moveToPillar (2);
			}
			if (shootScript.haveReached == true && shootScript.haveLooked == false) {
				if (bossInMiddle == false) {
					LookAt_Pillar(lookAtPillar2BossEdge);
				}
				else if (bossInMiddle == true) {
					LookAt_Pillar(lookAtPillar2BossMiddle);
				}
			}
		}
		else if (shootScript.shotPillar3 == true && shootScript.haveLooked == false) {
			if (shootScript.haveReached == false) {
				moveToPillar (3);
			}
			if (shootScript.haveReached == true && shootScript.haveLooked == false) {
				if (bossInMiddle == false) {
					LookAt_Pillar(lookAtPillar3BossEdge);
				}
				else if (bossInMiddle == true) {
					LookAt_Pillar(lookAtPillar3BossMiddle);
				}				
			}
		}
		else if (shootScript.shotPillar4 == true && shootScript.haveLooked == false) {
			if (shootScript.haveReached == false) {
				moveToPillar (4);
			}
			if (shootScript.haveReached == true && shootScript.haveLooked == false) {
				if (bossInMiddle == false) {
					LookAt_Pillar(lookAtPillar4BossEdge);
				}
				else if (bossInMiddle == true) {
					LookAt_Pillar(lookAtPillar4BossMiddle);
				}			
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
		else if (num == 6 && bossAIScript.spawnMinions == true) {
			if(count > 0 && count <= 5){
				spawnBear (new Vector3(86.24f, 30.52f, 6.68f), 0); // target1
				spawnBear (new Vector3(86.24f, 30.52f, 0.63f), 0); // target2
				spawnBear (new Vector3(86.24f, 30.52f, -6.2f), 0); // target3
				spawnBear (new Vector3 (72.43f, 3.32f, -9.94f), 0); // target4
				spawnBear (new Vector3 (72.43f, 3.32f, 8.44f), 0); // target5
			}
			else if(count > 5 && count <= 12 && !(GameObject.Find ("Target1") || GameObject.Find ("Target2") || GameObject.Find ("Target3") || GameObject.Find ("Target4") || GameObject.Find ("Target5"))){
				spawnBear (new Vector3(86.24f, 30.52f, 6.68f), 0); // target6
				spawnBear (new Vector3(86.24f, 30.52f, 0.63f), 0); // target7
				spawnBear (new Vector3(86.24f, 30.52f, -6.2f), 0); // target8
				spawnBear (new Vector3 (72.43f, 3.32f, -9.94f), 0); // target9
				spawnBear (new Vector3 (72.43f, 3.32f, 8.44f), 0); // target10
				spawnLollipop (new Vector3 (66f, 6.29f, 63.887f)); // target11
				spawnLollipop (new Vector3 (66f, 6.29f, -68.92f)); // target12
			}
			else if (count > 12 && count <= 13 && !(GameObject.Find ("Target6") || GameObject.Find ("Target7") || GameObject.Find ("Target8") || GameObject.Find ("Target9") || GameObject.Find ("Target10") || GameObject.Find ("Target11") || GameObject.Find ("Target12"))){
				minionsKilled = true;
				count = 14;
				num = 7;
			}
		}
		else if (num == 7) {
			num = LookAt(lookAtPillar1BossMiddle, num);
		}
	}
	
	private void startStorySequence() {
		if(speechSequence == 0) {
			shootScript.enabled = false;
			bossSpeech1.enabled = true;
			speechSequence = 1;
		}
		else if(Input.GetMouseButtonUp(0) && speechSequence == 1) {
			bossSpeech1.enabled = false;
			bossSpeech2.enabled = true;
			speechSequence = 2;
		}
		else if (Input.GetMouseButtonUp(0) && speechSequence == 2) {
			bossSpeech2.enabled = false;
			bossSpeech3.enabled = true;
			speechSequence = 3;
		}
		else if (Input.GetMouseButtonUp(0) && speechSequence == 3) {
			bossSpeech3.enabled = false;
			bossSpeech1.enabled = false;
			bossSpeech2.enabled = false;
			shootScript.enabled = true;
			num += 1;
		}
	}
	
	private void moveToCenter() {
		TranslateTo(movementCenterPoint, 50f, 0);
	}
	
	private void moveToPillar(int pillarNumber) {
		targetReticle1to2Edge.SetActive(false);;
		targetReticle1to3Edge.SetActive(false);
		targetReticle2to1Edge.SetActive(false);
		targetReticle2to4Edge.SetActive(false);
		targetReticle3to4Edge.SetActive(false);
		targetReticle4to3Edge.SetActive(false);
		targetReticle1to2Middle.SetActive(false);
		targetReticle1to3Middle.SetActive(false);
		targetReticle1to4Middle.SetActive(false);
		targetReticle2to1Middle.SetActive(false);
		targetReticle2to4Middle.SetActive(false);
		targetReticle3to1Middle.SetActive(false);
		targetReticle3to4Middle.SetActive(false);
		targetReticle4to2Middle.SetActive(false);
		targetReticle4to3Middle.SetActive(false);
		
		if (pillarNumber == 1) {
			if (bossInMiddle == false) {
				TranslateToPillar(movementPillar1BossEdge, 50f);
			}
			else if (bossInMiddle == true) {
				TranslateToPillar(movementPillar1BossMiddle, 50f);
			}
			atPillar = 1;
		}
		else if (pillarNumber == 2) {
			if (bossInMiddle == false) {
				TranslateToPillar(movementPillar2BossEdge, 50f);
			}
			else if (bossInMiddle == true) {
				TranslateToPillar(movementPillar2BossMiddle, 50f);
			}
			atPillar = 2;
		}
		else if (pillarNumber == 3) {
			if (bossInMiddle == false) {
				TranslateToPillar(movementPillar3BossEdge, 50f);
			}
			else if (bossInMiddle == true) {
				TranslateToPillar(movementPillar3BossMiddle, 50f);
			}
			atPillar = 3;
		}
		else if (pillarNumber == 4) {
			if (bossInMiddle == false) {
				TranslateToPillar(movementPillar4BossEdge, 50f);
			}
			else if (bossInMiddle == true) {
				TranslateToPillar(movementPillar4BossMiddle, 50f);
			}
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
		GameObject lollipop = Instantiate(lollipopPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject;
		lollipop.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = camera.transform.position - lollipop.transform.position; // make the instantiated bear face the camera
		lollipop.transform.rotation = Quaternion.LookRotation(direction);
		count++;
	}
	
	/*
	private void spawnEgg(Vector3 position){
		GameObject egg = Instantiate(EggPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject;
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
