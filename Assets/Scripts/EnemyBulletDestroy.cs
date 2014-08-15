using UnityEngine;
using System.Collections;

public class EnemyBulletDestroy : MonoBehaviour {

	private int playerHealth = 0;
	
	// Audio
	public AudioClip shieldBlock;
	public AudioClip takeDamage;
	
	// Function is triggered when the object collides with another object
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Enemy" || collider.tag == "EnemyHead"){
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
				StartCoroutine(PlayOuch());
			}
			else{ // shield is up and the player gets hit by a bullet
				AudioSource.PlayClipAtPoint(shieldBlock, transform.position);
			}
			Debug.Log ("Health1 = " + playerHealth);
			PlayerPrefs.SetInt ("playerHealth", (int)playerHealth);

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
				Debug.Log ("Health2 = " + playerHealth);
				PlayerPrefs.SetInt ("playerHealth", (int)playerHealth);
				
				// Destroy the bullet
				Destroy (gameObject);
			}
			// Do not put else statement here, will break the detection
		}
		else{
			// Destroy the bullet object if it hits anything else
			Destroy (gameObject);
		}	
	}
	
	IEnumerator PlayOuch(){
		yield return new WaitForSeconds(0.5F);
		audio.PlayOneShot(takeDamage);
		yield return new WaitForSeconds(0.5F);
		yield break;
	}
}

