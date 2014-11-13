using UnityEngine;
using System.Collections;

public class RocketMissile : MonoBehaviour {

	[SerializeField] private Shooting shootingScript;
	private Vector3 flyTo;
	private bool haveExploded;
	[SerializeField] private GameObject explosionEffectLarge;
	[SerializeField] private GameObject explosionEffectSmall;
	[SerializeField] private AudioClip explosionSound;
	private Collider[] colliders;

	// Use this for initialization
	void Start () {
		shootingScript = GameObject.Find ("RayCaster").GetComponent<Shooting>();
		flyTo = shootingScript.positionShot;
		haveExploded = false;
	}
	
	// Update is called once per frame
	void Update () {
		float range = Vector3.Distance(this.transform.position, flyTo );
		if (range > 1.0f) {			// still flying
			if (Application.loadedLevelName == "MainHall" ) {
				// The main hall has a bigger level scale than the others
				this.rigidbody.velocity = ((flyTo - this.transform.position)).normalized * Constants.ROCKET_FLYING_SPEED;
			}
			else {
				this.rigidbody.velocity = ((flyTo - this.transform.position)).normalized * Constants.ROCKET_FLYING_SPEED * 0.75f;
			}
			
			//find the vector pointing from our position to the target
			Vector3 _direction = (this.transform.position - flyTo);
			
			//create the rotation we need to be in to look at the target
			Quaternion _lookRotation = Quaternion.LookRotation(_direction);
			
			//rotate us over time according to speed until we are in the required rotation
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _lookRotation, Time.deltaTime * 10.0f);
		}
		else if (range <= 1.0f) {	// hit the target (close enough)
			if (haveExploded == false) {
				haveExploded = true;
				if (Application.loadedLevelName == "MainHall" || Application.loadedLevelName == "ActualBossRoom") {
					Instantiate(explosionEffectLarge, this.transform.position, this.transform.rotation);
				}
				else {
					Instantiate(explosionEffectSmall, this.transform.position, this.transform.rotation);
				}
				Debug.Log ("Explode sound");
				audio.PlayOneShot(explosionSound);
			}
			
			// The levels have different scales
			if (Application.loadedLevelName == "MainHall_") {
				colliders = Physics.OverlapSphere(this.transform.position, 
				                                  Constants.ROCKET_EXPLOSION_RADIUS * 
				                                  Constants.ROCKET_EXPLOSION_RADIUS_MAINHALL_MULTIPLIER);
			}
			else if (Application.loadedLevelName == "BossRoom") {
				colliders = Physics.OverlapSphere(this.transform.position, 
				                                  Constants.ROCKET_EXPLOSION_RADIUS * 
				                                  Constants.ROCKET_EXPLOSION_RADIUS_BOSSROOM_MULTIPLIER);
			}
			else if (Application.loadedLevelName == "ActualBossRoom") {
				colliders = Physics.OverlapSphere(this.transform.position, 
				                                  Constants.ROCKET_EXPLOSION_RADIUS * 
				                                  Constants.ROCKET_EXPLOSION_RADIUS_ACTUALBOSSROOM_MULTIPLIER);
			}
			else {
				colliders = Physics.OverlapSphere(this.transform.position, Constants.ROCKET_EXPLOSION_RADIUS);
			}
			
			foreach (Collider hit in colliders) {
				if (hit) {
					shootingScript.hitDetection(hit.gameObject);
				}
			}
			Destroy (this.transform.gameObject, 0.5f);
		}
		
	}
}
