using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

	private float currentHealth;
	private float totalHealth;
	public float percentage;
	
	[SerializeField] private GameObject bullet;				// bullet prefab
	private GameObject player;								// "mainCharacter"

	// Boss shooting values
	private float bulletSpeed = 10.0f; 						// bullet speed
	private float fireRate = Constants.BOSS_FIRE_RATE; 		// Rate of fire for the enemy
	private float nextFire = Constants.BOSS_FIRE_RATE;
	private float sprayFor = Constants.BOSS_SPRAY_TIME;		// Boss will spray a barrage of bullets 
	private float idleFor = Constants.BOSS_IDLE_TIME;		// Boss will not do anything
	[SerializeField] private AudioClip shootSound;

	// Jellybean bomb
	[SerializeField] private GameObject jellybeanBomb;
	private bool hasThrown = false;
	private bool bombHasExploded = false;
	private float bombExistenceTimer = Constants.BOSS_BOMB_FUSE_TIME;
	public int currentPhase;
	
	// Kinder Surprise Bomb
	[SerializeField] private GameObject kinderSurprise;
	private float throwKinderFor = Constants.BOSS_KINDER_THROW_TIME;
	private float idleKinderFor = Constants.BOSS_KINDER_IDLE_TIME;
	
	public bool fightStart;								// Has the boss fight started (starts after the story sequence)
	public int bossMovementNumber;						// Tracker for boss movement
	public bool spawnMinions;							// Has boss spawned minions?
	[SerializeField] private EventManager_ActualBossRoom bossRoomScript;
	public bool canShootRoof;							// Can the player shoot the roof (yes if boss < 10% hp)
	public bool roofHitBoss;							// Has the roof hit the boss? (win game if yes)
	private CrackedRoof crackedRoofScript;				// Script access from the cracked roof
	private bool dieAnimationPlayed;					// Has the boss dying animation been played (prevent playing again)
	public bool behindChair;							// Is the boss behind the chair?
	public bool movingToMiddle;							// Is the boss moving to the middle of the room?

	// Use this for initialization
	void Start () {
		totalHealth = Constants.BOSS_TOTAL_HEALTH;
		currentHealth = totalHealth;
		percentage = currentHealth / totalHealth;
		fightStart = false;
		bossMovementNumber = 0;
		currentPhase = 0;
		spawnMinions = false;
		canShootRoof = false;
		roofHitBoss = false;
		dieAnimationPlayed = false;
		behindChair = false;
		movingToMiddle = false;
		
		player = GameObject.FindWithTag ("MainCharacter");
		crackedRoofScript = GameObject.FindWithTag ("CrackedRoof").GetComponent<CrackedRoof>();
	}
	
	// Update is called once per frame
	void Update () {
		percentage = currentHealth / totalHealth;
		Debug.Log ("current health = " + currentHealth + " percentage = " + percentage + " total health = " + totalHealth);
		//Debug.Log ("current phase = " + currentPhase + ", boss movement number = " + bossMovementNumber);
		
		if (bossRoomScript.bossInMiddle == true && percentage < 0.6f) {
			LookAt(player.transform.position, 0, 0.1f);
		}
		
		if (crackedRoofScript.spawnedBrokenRoof == true) {
			if (roofHitBoss == true) {
				if (dieAnimationPlayed == false) {
					animation.Play ("Death1");
					dieAnimationPlayed = true;
				}
				Invoke ("winGame", 3f);			// calls winGame() after 3 seconds
			}
		}
		else if (percentage < 0.1f) {
			sprayBullets();
			canShootRoof = true;
		}
		else if (percentage < 0.2f) {
			if (currentPhase == 5) {
				//Debug.Log ("Phase 20-1");
				throwBomb();
				currentPhase = 6;
			}
			else if (currentPhase == 6) {
				//Debug.Log ("Phase 20-2");
				hasThrown = false;
				//sprayBullets();
				throwKinderSurprise();
			}
		}
		else if (percentage < 0.4f) {
			if (currentPhase == 4) {
				//Debug.Log ("Phase 40-1");
				throwBomb();
				currentPhase = 5;
			}
			else if (currentPhase == 5) {
				//Debug.Log ("Phase 40-2");
				hasThrown = false;
				sprayBullets();
			}
		}
		else if (bossRoomScript.bossInMiddle == true && percentage < 0.6f) {
			movingToMiddle = false;
			//sprayBullets();
			throwKinderSurprise();
		}
		else if (bossRoomScript.minionsKilled == true) {
			// boss move to middle of room
			behindChair = false;
			moveToMiddle();
		}	
		else if (percentage < 0.6f) {
			//Debug.Log ("60% phase");
			hideBehindChair();
			if (currentPhase == 3) {
				if (!animation.IsPlaying("Idle1")) {
					animation.Play ("Idle1");
				}
				spawnMinions = true;
				currentPhase = 4;
			}
		}
		else if (percentage <= 0.8f) {
			//Debug.Log ("80% phase");
			if (currentPhase == 1) {
				//Debug.Log ("Phase 80-1");
				throwBomb();
				currentPhase = 2;
			}
			else if (currentPhase == 2) {
				//Debug.Log ("Phase 80-2");
				hasThrown = false;
				//sprayBullets();
				throwKinderSurprise();
			}
		}
		else if (percentage <= 1.0f && fightStart == true) {	// phase 1
			currentPhase = 1;
			sprayBullets();
		}
		else if (percentage == 1.0f) {
			if (!animation.IsPlaying("Idle1")) {
				animation.Play ("Idle1");
			}
		}
	}
	
	void winGame() {
		Application.LoadLevel("Win");
	}
	
	public void getHit() {
		currentHealth -= 1;
	}
	
	public void getHitBack() {
		currentHealth -= 5;
	}
	
	void shootBullets() {
		if (Time.time - nextFire > fireRate) {
			nextFire = Time.time + fireRate;
			GameObject clone;
			// Create a clone of the 'Bullet' prefab.
			clone = Instantiate (bullet, this.transform.position + new Vector3 (5F, 7F, -4F), this.transform.rotation) as GameObject;

			//animation.Stop ("Magic Attack3");
			audio.PlayOneShot (shootSound);
			
			float hitOrNot = Random.Range (0.0F, 1.0F);
			float offsetValueX = Random.Range (-4.0F, 4.0F);
			float offsetValueY = Random.Range (-4.0F, 4.0F);
			
			// Exclude values where offset is between -1 and 1.
			while (offsetValueX < -2.0F && offsetValueX > 2.0F) {
				offsetValueX = Random.Range (-4.0F, 4.0F);
			}
			while (offsetValueY < -2.0F && offsetValueY > 2.0F) {
				offsetValueY = Random.Range (-4.0F, 4.0F);
			}
			
			Vector3 randomOffset;
			if (hitOrNot < 0.08F) { // hit
				randomOffset = new Vector3 (-5F, -7F, 4F);
			} else { // no hit
				randomOffset = new Vector3 (offsetValueX - 5F, offsetValueY - 7F, offsetValueY + 4F);
			}
			
			// Adds a force to the bullet so it can move
			clone.rigidbody.velocity = ((player.transform.position + randomOffset - transform.position));
		} 
		else {
			if (!animation.IsPlaying("Magic Attack3")) {
				animation.Play ("Magic Attack3");
			}
		}
	}

	// Shoot bullets
	void sprayBullets() {
		if (sprayFor > 0) {
			shootBullets();
			sprayFor -= Time.deltaTime;
		}
		else if (idleFor > 0) {
			idleFor -= Time.deltaTime;
		}
		if (idleFor < 0) {
			sprayFor = Constants.BOSS_SPRAY_TIME;
			idleFor = Constants.BOSS_IDLE_TIME;
		}
	}

	// Throw jellybean bomb
	void throwBomb() {
		Debug.Log ("Bomb thrown");
		activateTargetReticles();
		Vector3 bossPosition = this.transform.position;
		if (hasThrown == false) {
			if (!animation.IsPlaying("Attack")) {
				animation.Play ("Attack");
			}
			Instantiate (jellybeanBomb, new Vector3(bossPosition.x-10, bossPosition.y+2, bossPosition.z), this.transform.rotation);
			hasThrown = true;
			bombHasExploded = false;
		}
	}

	void activateTargetReticles() {
		if (bossRoomScript.atPillar == 1 && bossRoomScript.bossInMiddle == false) {
			if (bossRoomScript.isPillar2Destroyed == false) {
				bossRoomScript.targetReticle1to2Edge.SetActive(true);
			}
			if (bossRoomScript.isPillar3Destroyed == false) {
				bossRoomScript.targetReticle1to3Edge.SetActive(true);
			}
		}
		else if (bossRoomScript.atPillar == 2 && bossRoomScript.bossInMiddle == false) {
			if (bossRoomScript.isPillar1Destroyed == false) {
				bossRoomScript.targetReticle2to1Edge.SetActive(true);
			}
			if (bossRoomScript.isPillar4Destroyed == false) {
				bossRoomScript.targetReticle2to4Edge.SetActive(true);	
			}
		}
		else if (bossRoomScript.atPillar == 3 && bossRoomScript.bossInMiddle == false) {
			if (bossRoomScript.isPillar4Destroyed == false) {
				bossRoomScript.targetReticle3to4Edge.SetActive(true);
			}
		}
		else if (bossRoomScript.atPillar == 4 && bossRoomScript.bossInMiddle == false) {
			if (bossRoomScript.isPillar3Destroyed == false) {
				bossRoomScript.targetReticle4to3Edge.SetActive(true);
			}
		}
		else if (bossRoomScript.atPillar == 1 && bossRoomScript.bossInMiddle == true) {
			if (bossRoomScript.isPillar2Destroyed == false) {
				bossRoomScript.targetReticle1to2Middle.SetActive(true);
			}
			if (bossRoomScript.isPillar3Destroyed == false) {
				bossRoomScript.targetReticle1to3Middle.SetActive(true);
			}
			if (bossRoomScript.isPillar4Destroyed == false) {
				bossRoomScript.targetReticle1to4Middle.SetActive(true);
			}
		}
		else if (bossRoomScript.atPillar == 2 && bossRoomScript.bossInMiddle == true) {
			if (bossRoomScript.isPillar1Destroyed == false) {
				bossRoomScript.targetReticle2to1Middle.SetActive(true);
			}
			if (bossRoomScript.isPillar4Destroyed == false) {
				bossRoomScript.targetReticle2to4Middle.SetActive(true);
			}
		}
		else if (bossRoomScript.atPillar == 3 && bossRoomScript.bossInMiddle == true) {
			if (bossRoomScript.isPillar1Destroyed == false) {
				bossRoomScript.targetReticle3to1Middle.SetActive(true);
			}
			if (bossRoomScript.isPillar4Destroyed == false) {
				bossRoomScript.targetReticle3to4Middle.SetActive(true);
			}
		}
		else if (bossRoomScript.atPillar == 4 && bossRoomScript.bossInMiddle == true) {
			if (bossRoomScript.isPillar2Destroyed == false) {
				bossRoomScript.targetReticle4to2Middle.SetActive(true);
			}
			if (bossRoomScript.isPillar3Destroyed == false) {
				bossRoomScript.targetReticle4to3Middle.SetActive(true);
			}
		}
	}
	
	void moveToMiddle ()
	{
		Vector3 waypoint1 = new Vector3(88.19f, 2.4f, 18.4f);
		Vector3 waypoint2 = new Vector3(70.68f, 2.4f, 17.97f);
		Vector3 middlePoint = new Vector3(-7.23f, 0.0f, 0.76f);
		
		movingToMiddle = true;
		
		if (bossMovementNumber == 4) {
			bossMovementNumber = TranslateTo(waypoint1, 20.0f, bossMovementNumber);
		}
		else if (bossMovementNumber == 5) {
			bossMovementNumber = TranslateTo(waypoint2, 20.0f, bossMovementNumber);
		}
		else if (bossMovementNumber == 6) {
			bossMovementNumber = TranslateTo(middlePoint, 20.0f, bossMovementNumber);
		}
		else if (bossMovementNumber == 7) {
			bossRoomScript.bossInMiddle = true;
			bossMovementNumber = 8;
		}
	}
	
	void hideBehindChair() {
		Vector3 waypoint1 = new Vector3(70.68f, 2.4f, 17.97f);
		Vector3 waypoint2 = new Vector3(88.19f, 2.4f, 18.4f);
		Vector3 waypointBehindChair = new Vector3(88.57f, 2.4f, 3.12f);
		Vector3 faceForward = new Vector3(-28.48f, 2.4f, -0.51f);
		
		behindChair = true;
	
		if (bossMovementNumber == 0) {
			bossMovementNumber = TranslateTo(waypoint1, 15.0f, bossMovementNumber);
		}
		else if (bossMovementNumber == 1) {
			bossMovementNumber = TranslateTo(waypoint2, 15.0f, bossMovementNumber);
		}
		else if (bossMovementNumber == 2) {
			bossMovementNumber = TranslateTo(waypointBehindChair, 15.0f, bossMovementNumber);
		}
		else if (bossMovementNumber == 3) {
			bossMovementNumber = LookAt(faceForward, bossMovementNumber, 15f);
			currentPhase = 3;
			if (!animation.IsPlaying("Idle1")) {
				animation.Play ("Idle1");
			}
		}
	}
	
	private void throwKinderSurprise() {
		Vector3 bossPosition = this.transform.position;
		
		if (throwKinderFor > 0) {
			if (!animation.IsPlaying("Attack")) {
				animation.Play ("Attack");
			}
			Instantiate(kinderSurprise, new Vector3(bossPosition.x-10, bossPosition.y+2, bossPosition.z), this.transform.rotation);
			throwKinderFor -= Time.deltaTime;
		}
		else if (idleKinderFor > 0) {
			idleKinderFor -= Time.deltaTime;
		}
		if (idleKinderFor < 0) {
			throwKinderFor = Constants.BOSS_KINDER_THROW_TIME;
			idleKinderFor = Constants.BOSS_KINDER_IDLE_TIME;
		}
	}
	
	private int TranslateTo(Vector3 position , float spd, int num) {
		float range1 = Vector3.Distance(this.transform.position, position );
	
		if (range1 > 1.0) {
			//find the vector pointing from our position to the target
			Vector3 _direction = (position - this.transform.position).normalized;
			
			//create the rotation we need to be in to look at the target
			Quaternion _lookRotation = Quaternion.LookRotation (_direction);
			
			//rotate us over time according to speed until we are in the required rotation
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, _lookRotation, Time.deltaTime * 15);
			
			// calculate direction and move towards the target
			Vector3 dir = position - this.transform.position;
			dir = dir.normalized;

			animation.Play("Run");
			
			//theCharacter.transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
			this.transform.Translate (dir * spd * Time.deltaTime, Space.World);
			this.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * 15);
		}	
		else if (range1 <= 1.0) {
			animation.Stop("Run");
			num += 1;
		}
			
		return num;
	}

	private int LookAt(Vector3 position, int num, float rotationSpeed) {
		
		//find the vector pointing from our position to the target
		Vector3 _direction = (position - this.transform.position);//.normalized;
		
		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		animation.Play ("Idle1");

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(this.transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
		
		if (Mathf.Abs(Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f) {
			animation.Stop("Idle1");
			num += 1;
			//reached = true;
		}
		
		return num;
	}
}
