using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

	private int currentHealth;
	private int totalHealth;
	public float percentage;
	
	[SerializeField] private GameObject bullet;				// bullet prefab
	private GameObject player;								// "mainCharacter"
	
	[SerializeField] private float bulletSpeed = 10.0f; 	// bullet speed
	[SerializeField] private float fireRate = 0.1F; 		// Rate of fire for the enemy
	[SerializeField] private float nextFire = 0.1F;
	
	[SerializeField] private AudioClip shootSound;

	// Use this for initialization
	void Start () {
		totalHealth = Constants.BOSS_TOTAL_HEALTH;
		currentHealth = totalHealth;
		percentage = currentHealth / totalHealth;
		
		player = GameObject.FindWithTag ("MainCharacter");
	}
	
	// Update is called once per frame
	void Update () {
		percentage = currentHealth / totalHealth;
		if (percentage < 1.0f) {
			phase100();
		}
		else if (percentage < 0.8f) {
			phase80();
		}
		else if (percentage < 0.6f) {
			phase60();
		}
		else if (percentage < 0.5f) {
			phase50();
		}
		else if (percentage < 0.4f) {
			phase40();
		}
		else if (percentage < 0.2f) {
			phase20();
		}
		else if (percentage < 0.1f) {
			phase10();
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
			//Debug.Log ("Bullet position = " + clone.transform.position.x + " " + clone.transform.position.y + " "+ clone.transform.position.z);
			//Debug.Log ("Target position = " + (player.transform.position - transform.position).x + " " + (player.transform.position - transform.position).y + " "+ (player.transform.position - transform.position).z);
			
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
				#if UNITY_EDITOR
				Debug.Log ("Hit");
				#endif
				randomOffset = new Vector3(-5F,-7F,4F);
			}
			else{ // no hit
				randomOffset = new Vector3(offsetValueX-5F, offsetValueY-7F, offsetValueY+4F);
			}
			
			// Adds a force to the bullet so it can move
			clone.rigidbody.velocity = ((player.transform.position + randomOffset - transform.position));
		}
	}
	
	void phase100() {
		shootBullets();
	}
	
	void phase80() {
	
	}
	
	void phase60() {
	
	}
	
	void phase50() {
	
	}
	
	void phase40() {
	
	}
	
	void phase20() {
	
	}
	
	void phase10() {
	
	}
}
