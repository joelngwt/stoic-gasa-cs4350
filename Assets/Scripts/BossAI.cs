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
	private float fireRate = Constants.BOSS_FIRE_RATE; // Rate of fire for the enemy
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
	
	public bool fightStart;
	public int bossMovementNumber;
	public bool spawnMinions;
	[SerializeField] private EventManager_ActualBossRoom bossRoomScript;

	// Use this for initialization
	void Start () {
		totalHealth = Constants.BOSS_TOTAL_HEALTH;
		currentHealth = totalHealth;
		percentage = currentHealth / totalHealth;
		fightStart = false;
		bossMovementNumber = 0;
		currentPhase = 0;
		spawnMinions = false;
		
		player = GameObject.FindWithTag ("MainCharacter");
	}
	
	// Update is called once per frame
	void Update () {
		percentage = currentHealth / totalHealth;
		//Debug.Log ("current health = " + currentHealth + " percentage = " + percentage + " total health = " + totalHealth);
		Debug.Log ("current phase = " + currentPhase + ", boss movement number = " + bossMovementNumber);
		
		if (bossRoomScript.bossInMiddle == true && percentage < 0.6f) {
			sprayBullets();
			LookAt(player.transform.position, 0); // slow this down by A LOT, so as to let player hit the back 
		}
		else if (bossRoomScript.minionsKilled == true) {
			// boss move to middle of room
			moveToMiddle();
		}	
		else if (percentage < 0.6f) {
			//Debug.Log ("60% phase");
			hideBehindChair();
			if (currentPhase == 3) {
				spawnMinions = true;
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
				sprayBullets();
			}
		}
		else if (percentage <= 1.0f && fightStart == true) {	// phase 1
			currentPhase = 1;
			sprayBullets();
		}
	}
	
	public void getHit() {
		currentHealth -= 1;
	}
	
	void shootBullets() {
		if(Time.time - nextFire > fireRate){
			nextFire = Time.time + fireRate;
			GameObject clone;
			// Create a clone of the 'Bullet' prefab.
			clone = Instantiate(bullet, this.transform.position+new Vector3(5F, 7F, -4F), this.transform.rotation) as GameObject;

			audio.PlayOneShot(shootSound);
			
			float hitOrNot = Random.Range(0.0F, 1.0F);
			float offsetValueX = Random.Range (-4.0F, 4.0F);
			float offsetValueY = Random.Range (-4.0F, 4.0F);
			
			// Exclude values where offset is between -1 and 1.
			while(offsetValueX < -2.0F && offsetValueX > 2.0F){
				offsetValueX = Random.Range (-4.0F, 4.0F);
			}
			while(offsetValueY < -2.0F && offsetValueY > 2.0F){
				offsetValueY = Random.Range (-4.0F, 4.0F);
			}
			
			Vector3 randomOffset;
			if(hitOrNot < 0.08F){ // hit
				randomOffset = new Vector3(-5F,-7F,4F);
			}
			else{ // no hit
				randomOffset = new Vector3(offsetValueX-5F, offsetValueY-7F, offsetValueY+4F);
			}
			
			// Adds a force to the bullet so it can move
			clone.rigidbody.velocity = ((player.transform.position + randomOffset - transform.position));
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
		Vector3 bossPosition = this.transform.position;
		if (hasThrown == false) {
			Instantiate (jellybeanBomb, new Vector3(bossPosition.x-10, bossPosition.y+2, bossPosition.z), this.transform.rotation);
			hasThrown = true;
			bombHasExploded = false;
		}
	}

	void moveToMiddle ()
	{
		Vector3 waypoint1 = new Vector3(88.19f, 2.4f, 18.4f);
		Vector3 waypoint2 = new Vector3(70.68f, 2.4f, 17.97f);
		Vector3 middlePoint = new Vector3(-7.23f, 0.24f, 0.76f);
		
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
			bossMovementNumber = LookAt(faceForward, bossMovementNumber);
			currentPhase = 3;
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
			
			//theCharacter.transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
			this.transform.Translate (dir * spd * Time.deltaTime, Space.World);
			this.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * 15);
		}	
		else if (range1 <= 1.0) {
			num += 1;
		}
			
		return num;
	}

	private int LookAt( Vector3 position , int num) {
		
		//find the vector pointing from our position to the target
		Vector3 _direction = (position - this.transform.position);//.normalized;
		
		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(this.transform.rotation, _lookRotation, Time.deltaTime * 15);
		
		if (Mathf.Abs(Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f) {
			num += 1;
			//reached = true;
		}
		
		return num;
	}
}
