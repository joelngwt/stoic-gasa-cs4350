﻿using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

	private float currentHealth;
	private float totalHealth;
	public float percentage;
	
	[SerializeField] private GameObject bullet;				// bullet prefab
	private GameObject player;								// "mainCharacter"

	// Phase 100 values
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
	public int currentPhase = 0;
	
	public bool fightStart;

	// Use this for initialization
	void Start () {
		totalHealth = Constants.BOSS_TOTAL_HEALTH;
		currentHealth = totalHealth;
		percentage = currentHealth / totalHealth;
		fightStart = false;
		
		player = GameObject.FindWithTag ("MainCharacter");
	}
	
	// Update is called once per frame
	void Update () {
		percentage = currentHealth / totalHealth;
		Debug.Log ("current health = " + currentHealth + " percentage = " + percentage + " total health = " + totalHealth);
		Debug.Log ("current phase = " + currentPhase);
			
		if (percentage < 0.1f) {
			phase10();
		}
		else if (percentage < 0.2f) {
			phase20();
		}
		else if (percentage < 0.4f) {
			phase40();
		}
		else if (percentage < 0.5f) {
			phase50();
		}
		else if (percentage < 0.6f) {
			phase60();
		}
		else if (percentage <= 0.8f && currentPhase >= 1 && currentPhase <= 2) {		// phase 2
			if (currentPhase == 1) {
				throwBomb();
				sprayBullets();
				// currentPhase += 1 when the bean is destroyed (done in JellybeanBomb.cs)
			}
			else if (currentPhase == 2) {
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
		Vector3 bossPosition = this.transform.position;
		if (hasThrown == false) {
			Instantiate (jellybeanBomb, new Vector3(bossPosition.x-10, bossPosition.y+2, bossPosition.z), this.transform.rotation);
			hasThrown = true;
			bombHasExploded = false;
		}
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
