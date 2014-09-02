using UnityEngine;
using System.Collections;

// Assign this script to all 3 pickups

public class PickupBehaviour : MonoBehaviour {

	private float maxUpAndDown = 5.0f;			// Maximum up and down value
	private float speed = 150.0f;				// Speed of moving
	private float angle = -90.0f;				
	private float toDegrees = Mathf.PI/180.0f;  // Radians to degree
	private float startHeight;					// Starting height
	public bool canMove;
	private GameObject player;
	private GunDisplay gunDisplayScript;
	
	Transform myTransform;
	
	void Awake()
	{
		player = GameObject.FindWithTag ("MainCharacter");
		gunDisplayScript = GameObject.Find ("GunDisplay").GetComponent<GunDisplay>();
		myTransform = transform;
		startHeight = myTransform.localPosition.y;
		canMove = false;
	}
	
	void Update () 
	{
		if (canMove) {
			this.rigidbody.velocity = Constants.FLY_TO_PLAYER_SPEED * (player.transform.position - this.transform.position);
		}
		else {
			angle += speed * Time.deltaTime;
			
			if (angle > 270.0f)
			{ 
				angle -= 360.0f;
			}
			
			Vector3 pos = myTransform.localPosition;
			// Calculates the next height according to sin wave
			pos.y = startHeight + maxUpAndDown * (1 + Mathf.Sin(angle * toDegrees)) * 0.5f;
			myTransform.localPosition = pos;
			
			myTransform.Rotate(0, 6.0f * 20.0f * Time.deltaTime, 0);
		}
	}
	
	void OnTriggerEnter(Collider collidedWith) {
		if(collidedWith.name == "Character") {
			if (this.gameObject.tag == "HealthPickup") {
				int playerHealth = PlayerPrefs.GetInt("playerHealth");
				if (playerHealth < 4 && playerHealth > 0) {
					playerHealth += Constants.HEALTH_PICKUP_GAIN;
				}
				PlayerPrefs.SetInt("playerHealth", playerHealth);
				Destroy (this.transform.gameObject);
			}
			else if (this.gameObject.tag == "AmmoPickup") {
				gunDisplayScript.ammoCountTotalHMG += Constants.AMMO_PICKUP_HMG;
				gunDisplayScript.ammoCountTotalShotgun += Constants.AMMO_PICKUP_SHOTGUN;
				Destroy (this.transform.gameObject);
			}
			else if (this.gameObject.tag == "BoostPickup") {
				Destroy (this.transform.gameObject);
			}
		}
	}
}
