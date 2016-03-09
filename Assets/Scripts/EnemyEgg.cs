using UnityEngine;
using System.Collections;

public class EnemyEgg : MonoBehaviour
{
	public GameObject player;
	public float delay;
	public int lifeEgg;
	public enum States {
		NullStateID = 0, // Use this ID to represent a non-existing State in your system	
		Attack,
		Wait,
		Charge
	}
	public States current;
	private float attackTimer;	//countdown till attack from one of the Fly states
	private float colourTimer; //for maintaining colour red after getting hit
	private Vector3 ePos, pPos;

	private int playerHealth = 0;
	
	public AudioClip takeDamage; // audio
	public AudioClip shieldBlock;
	public AudioClip attack;

	// Use this for initialization
	void Start ()
	{
		lifeEgg = 10;
		current = States.Charge;
		attackTimer = -1.0f; //starts with negative number so that Egg will never enter Attack State until it reaches Player
		colourTimer = 0.5f;
	}

	// Update is called once per frame
	void Update ()
	{
		player = GameObject.FindWithTag ("MainCharacter");
		attackTimer -= Time.deltaTime;
		colourTimer -= Time.deltaTime;

		//Charge State
		if(current == States.Charge)
		{
			GetComponent<Animation>().Play ("Run");
			ePos = transform.position;
			pPos = player.transform.position;
			//transition from Attack state to Retreat state

			//change according to model
			if (Mathf.Abs(ePos.x - pPos.x) < 5.0f && Mathf.Abs(ePos.y - pPos.y) < 10.0f
			    && Mathf.Abs(ePos.z - pPos.z) < 7.0f)
			{
				
				current = States.Attack;
				attackTimer = 0.0f;
			}
			else //charge towards player
			{
				GetComponent<Rigidbody>().velocity = (pPos - ePos);
			}
		}

		//Attack State
		if(current == States.Attack)
		{
			//print ("implement player minus one in health in Egg Attack State");
			current = States.Wait;
			attackTimer = 5.0f;
			
			GetComponent<Animation>().Stop (); // stop the running animation
			GetComponent<AudioSource>().PlayOneShot(attack); // attack sound
			// Get and update the health of the player
			if (PlayerPrefs.HasKey ("playerHealth")) {
				playerHealth = PlayerPrefs.GetInt ("playerHealth");
			}
			if(PlayerPrefs.GetInt ("shieldUp") == 0){
				playerHealth -= 1;
			}
			else{
				GetComponent<AudioSource>().PlayOneShot(shieldBlock); // shield block sound
			}
			Debug.Log ("Health1 = " + playerHealth);
			PlayerPrefs.SetInt ("playerHealth", (int)playerHealth);

		}

		if(current == States.Wait)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
			if(attackTimer <= 0.0f)
				current = States.Attack;
		}
	}

	//also responsible for injuring and killing the egg enemy
	public void StartAnim()
	{
		lifeEgg--;
		GetComponent<AudioSource>().PlayOneShot(takeDamage);
		//renderer.material.SetColor("_Color", Color.red);
		if(lifeEgg == 0)
		{
			GetComponent<Animation>().Play ("Die_Egg");
			DestroyObject(gameObject, 0.7F);
			colourTimer = 0.5f;
		}
		else 
		{
			colourTimer = 0.5f;
		}
	}
}

