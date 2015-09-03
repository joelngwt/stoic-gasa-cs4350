﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float delay;
	//made public for EnemyShoot.cs to access
	public enum States {
		NullStateID = 0, // Use this ID to represent a non-existing State in your system	
		Attack,
		ChangeCover,
		TakeCover
	}
	public States current;
	public float positionOriginal;
	public float attackTimer;	//countdown till attack from TakeCoverState
	public float coverTimer;	//countdown till take cover from AttackState
	private bool first = false;	//test if first time entering a statement
	public int coverType;

	// Audio
	public AudioClip getDamaged;
	public AudioClip getDamaged2;
	
	// Pickups
	[SerializeField] private GameObject healthPickup;
	[SerializeField] private GameObject ammoPickup;
	[SerializeField] private GameObject boostPickup;

	//explosion
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		attackTimer = 100000.0f;
		coverTimer = 3.0f;
		current = States.Attack;

		//positionOriginal = transform.position.y;
		
		if(coverType == 0)
			positionOriginal = transform.position.y;
		else
			positionOriginal = transform.position.x;
		if(gameObject.tag == "Enemy"){
			animation.Play ("Cover_Down_out");
		}
	}

	// Update is called once per frame
	void Update () {
		attackTimer -= Time.deltaTime;
		coverTimer -= Time.deltaTime;

		//transition from AttackState to TakeCoverState
		if(coverTimer <= 0)
		{
			current = States.TakeCover;
			attackTimer = 2.0f; //reset other timer
			coverTimer = 100000.0f; //big number so that it won't drop zero
			first = true;
		}

		//action in TakeCoverState
		if(current == States.TakeCover && first == true)
		{

			Vector3 temp = transform.position;
			if(coverType == 0 && first == true) // move down to take cover
			{
				if(gameObject.tag == "Enemy"){
					animation.Stop ("Run"); // stop running animation
					animation.Play ("Cover_Down_in"); // take cover down
				}
				//if(temp.y >= positionOriginal - 2.0f)
				//	temp.y -= 0.1f;
				//else 
					first = false; 
			}
			else if(coverType == 1) //move right to take cover
			{
				if(gameObject.tag == "Enemy"){
					animation.Stop ("Run"); // stop running animation
					animation.Play ("Cover_SideL_in"); // take cover
				}
				//if(temp.x <= positionOriginal + 1.0f)
				//	temp.x += 0.1f;
				//else
					first = false;
			}
			else if(coverType == 2) //move left to take cover
			{
				if(gameObject.tag == "Enemy"){
					animation.Stop ("Run"); // stop running animation
					animation.Play ("Cover_SideR_in"); // take cover
				}
				//if(temp.x >= positionOriginal - 1.0f)
				//	temp.x -= 0.1f;
				//else
					first = false;
			}

			transform.position = temp;
			//disable firing in EnemyShoot.cs 
		}

		//transition from TakeCoverState to AttackState
		if(attackTimer <= 0)
		{
			current = States.Attack;
			coverTimer = 3.0f;
			attackTimer = 100000.0f; //big number so that it won't drop zero
			first = true;
		}

		//action in AttackState
		if(current == States.Attack && first == true)
		{
			Vector3 temp = transform.position;
			if(coverType == 0 && first == true)
			{
				if(gameObject.tag == "Enemy"){
					animation.Play ("Cover_Down_out");
				}
				//if(temp.y <= positionOriginal)
				//	temp.y += 0.2f;
				//else 
					first = false; 
			}			
			else if(coverType == 1)
			{
				if(gameObject.tag == "Enemy"){
					animation.Play ("Cover_SideL_out");
				}
				//if(temp.x >= positionOriginal)
				//	temp.x -= 0.2f;
				//else 
					first = false; 
			}
			else if(coverType == 2)
			{
				if(gameObject.tag == "Enemy"){
					animation.Play ("Cover_SideR_out");
				}
				//if(temp.x <= positionOriginal)
				//	temp.x += 0.2f;
				//else
					first = false; 
			}
			transform.position = temp;

			//enable firing in EnemyShoot.cs
		}

		//transition to ChangeCoverState
		//if detect shield button
		//GameObject[] covers = GameObject.FindObjectsWithTag("cover");
		//check if cover.position.x and cover.position.z already has a gummy bear there
		//move there

		/*
		if(transform.position.y <= -0.8f)
		{
			print("attackTimer = " + attackTimer);
			print("coverTimer = " + coverTimer);
			print("current = " + current);
		}
		*/
	}
	
	// Occurs when the bear dies
	public void StartAnim()
	{
		#if UNITY_EDITOR
		//Debug.Log ("startAnim in bear");
		#endif
		
		// Audio randomization
		float audioToPlay = Random.Range(0.0F, 1.0F);
		if(audioToPlay < 0.5){
			audio.PlayOneShot(getDamaged);
		}
		else{
			audio.PlayOneShot(getDamaged2);
		}
		Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
		// Will the bear drop a pickup?
		float dropPickup = Random.Range(0, 100);
		if (dropPickup < Constants.DROP_CHANCE_BOOST) {
			Instantiate(boostPickup, gameObject.transform.position, gameObject.transform.rotation);
		}
		else if (dropPickup < Constants.DROP_CHANCE_AMMO) {
			Instantiate(ammoPickup, gameObject.transform.position, gameObject.transform.rotation);
		}
		else if (dropPickup < Constants.DROP_CHANCE_HEALTH) {
			Instantiate(healthPickup, gameObject.transform.position, gameObject.transform.rotation);
		}		


		animation.Play ("Die_Bear");
		gameObject.collider.enabled = false;
		//renderer.material.SetColor("_Color", Color.red);
		DestroyObject(gameObject, 0.7F); // 0.625 seconds needed for die animation to complete
	}
}