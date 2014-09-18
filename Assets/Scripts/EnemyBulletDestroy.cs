using UnityEngine;
using System.Collections;

public class EnemyBulletDestroy : MonoBehaviour {

	private int playerHealth = 0;
	
	// Audio
	public AudioClip shieldBlock;
	public AudioClip takeDamage;

	public GameObject attached_warning_reticle;
	
	// Function is triggered when the object collides with another object
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Enemy" || collider.tag == "EnemyHead" || collider.tag == "Boss" || collider.tag == "BossBack"){
			// Do nothing. The bullet spawns from the middle of the enemy
			// If this detection is not done, the bullet will destroy itself
			// when it exits the enemy.
		}
		
		// If the bullet collides with the character
		// Note: The naming is strict. This means that non-clones and clones are different names
		else if(collider.name == "Character"){
		
			// Get and update the health of the player
			if (PlayerPrefs.HasKey ("playerHealth")) {
				playerHealth = PlayerPrefs.GetInt ("playerHealth");
			}
			if(PlayerPrefs.GetInt ("shieldUp") == 0){
				playerHealth -= 1;
			}
			else{ // shield is up and the player gets hit by a bullet
				AudioSource.PlayClipAtPoint(shieldBlock, transform.position);
			}
			#if UNITY_EDITOR
			//Debug.Log ("Health1 = " + playerHealth);
			#endif
			PlayerPrefs.SetInt ("playerHealth", (int)playerHealth);

			/*
			 * Destroy the warning reticle if 
			 * it exists
			 * */
			if(attached_warning_reticle != null)
			{
				GameObject.Destroy(attached_warning_reticle);
			}

			// Destroy the bullet
			Destroy (gameObject);
		}
		// Prevent collisions between bullets
		else if(collider.name == "EnemyBullet(Clone)"){
			if(collider.name == "Character"){
				// Get and update the health of the player
				if (PlayerPrefs.HasKey ("playerHealth")) {
					playerHealth = PlayerPrefs.GetInt ("playerHealth");
				}
				if(PlayerPrefs.GetInt ("shieldUp") == 0){
					playerHealth -= 1;
				}
				else{ // shield is up and the player gets hit by a bullet
					AudioSource.PlayClipAtPoint(shieldBlock, transform.position);
				}
				#if UNITY_EDITOR
				Debug.Log ("Health2 = " + playerHealth);
				#endif
				PlayerPrefs.SetInt ("playerHealth", (int)playerHealth);
				
				/*
				 * Destroy the warning reticle if 
				 * it exists
				 * */
				if(attached_warning_reticle != null)
				{
					GameObject.Destroy(attached_warning_reticle);
				}
				
				// Destroy the bullet
				Destroy (gameObject);
			}
			// Do not put else statement here, will break the detection
		}
		else{
			
			/*
			 * Destroy the warning reticle if 
			 * it exists
			 * */
			if(attached_warning_reticle != null)
			{
				GameObject.Destroy(attached_warning_reticle);
			}

			// Destroy the bullet object if it hits anything else
			Destroy (gameObject);
		}	
	}
}