using UnityEngine;
using System.Collections;

public class JellybeanBomb : MonoBehaviour {

	private GameObject player;
	private bool isStopped = false;

	private float blinkDuration = 0.5f;
	private float bombFuseTime = Constants.BOSS_BOMB_FUSE_TIME;
	[SerializeField] private GameObject explosionParticleEffect;
	private EventManager_ActualBossRoom bossRoomScript;
	private int throwBombAt;
	private bool activated;
	[SerializeField] private AudioClip bombExplosion;
	private GameObject pillar1;
	private GameObject pillar2;
	private GameObject pillar3;
	private GameObject pillar4;
	[SerializeField] private GameObject brokenPillar;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MainCharacter");
		pillar1 = GameObject.FindWithTag ("Pillar1");
		pillar2 = GameObject.FindWithTag ("Pillar2");
		pillar3 = GameObject.FindWithTag ("Pillar3");
		pillar4 = GameObject.FindWithTag ("Pillar4");
		bossRoomScript = GameObject.FindWithTag("MainCamera").GetComponent<EventManager_ActualBossRoom>();
		//bossAIScript = GameObject.FindWithTag("Boss").GetComponent<BossAI>();
		activated = false;

		// if current pillar is 1
		if (bossRoomScript.atPillar == 1) {
			if (bossRoomScript.bossInMiddle == false) {
				Vector3 throwDirection = new Vector3(-38.73f, 1.5f, 49.21f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 15);
			}
			else if (bossRoomScript.bossInMiddle == true) {
				Vector3 throwDirection = new Vector3(-19.97f, 1.5f, 58.1f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 15);
			}
			throwBombAt = 1;
		}
		// if current pillar is 2
		else if (bossRoomScript.atPillar == 2) {
			if (bossRoomScript.bossInMiddle == false) {
				Vector3 throwDirection = new Vector3(-37.64f, 1.5f, -45.56f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 15);
			}
			else if (bossRoomScript.bossInMiddle == true) {
				Vector3 throwDirection = new Vector3(-22.11f, 1.5f, -55.23f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 22);
			}
			throwBombAt = 2;
		}
		// If current pillar is 3
		else if (bossRoomScript.atPillar == 3) {
			if (bossRoomScript.bossInMiddle == false) {
				Vector3 throwDirection = new Vector3(11.46f, 1.5f, -37.95f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 18);
			}
			else if (bossRoomScript.bossInMiddle == true) {
				Vector3 throwDirection = new Vector3(10.71f, 1.5f, -47.75f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 18);
			}
			throwBombAt = 3;
		}
		// If current pillar is 4
		else if (bossRoomScript.atPillar == 4) {
			if (bossRoomScript.bossInMiddle == false) {
				Vector3 throwDirection = new Vector3(11.6f, 1.5f, 39.8f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 18.5f);
			}
			else if (bossRoomScript.bossInMiddle == true) {
				Vector3 throwDirection = new Vector3(10.7f, 1.5f, 51f) - this.transform.position;
				this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 24f);
			}
			throwBombAt = 4;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Stop the bomb if it gets too close (so it won't fly past the player)
		float rangeBetweenBombAndPlayer = Vector3.Distance(this.transform.position, player.transform.position );
		if (rangeBetweenBombAndPlayer < 13.0f && isStopped == false) {
			this.rigidbody.velocity = new Vector3(0, 0, 0);
			isStopped = true;
		}

		// Blinking bomb
		if (bombFuseTime > 3.0f) {
			if (blinkDuration > 0.0f && blinkDuration <= 0.5f) {
				renderer.material.color = Color.red;
				blinkDuration -= Time.deltaTime;
			}
			else if (blinkDuration < 0.0f) {
				blinkDuration = 1.0f;
			}
			if (blinkDuration > 0.5f) {
				renderer.material.color = Color.yellow;
				blinkDuration -= Time.deltaTime;
			}
		}
		// Blink faster if it's going to explode
		else if (bombFuseTime <= 3.0f) {
			if (blinkDuration > 0.0f && blinkDuration <= 0.25f) {
				renderer.material.color = Color.red;
				blinkDuration -= Time.deltaTime;
			}
			else if (blinkDuration < 0.0f) {
				blinkDuration = 0.5f;
			}
			if (blinkDuration > 0.25f) {
				renderer.material.color = Color.yellow;
				blinkDuration -= Time.deltaTime;
			}
		}

		// Fuse countdown
		bombFuseTime -= Time.deltaTime;
		if (bombFuseTime <= 0.0f) {
			
			if (activated == false) {
				audio.PlayOneShot(bombExplosion);
				Instantiate(explosionParticleEffect, this.transform.position, this.transform.rotation);
				//Vector3 breakPillarAt = pillar1.transform.position;
				if (throwBombAt == 1) {
					pillar1.SetActive(false);
					Instantiate(brokenPillar, pillar1.transform.position, pillar1.transform.rotation);
				}
				else if (throwBombAt == 2) {
					pillar2.SetActive(false);
					Instantiate(brokenPillar, pillar2.transform.position, pillar2.transform.rotation);
				}
				else if (throwBombAt == 3) {
					pillar3.SetActive(false);
					Instantiate(brokenPillar, pillar3.transform.position, pillar3.transform.rotation);
				}
				else if (throwBombAt == 4) {
					pillar4.SetActive(false);
					Instantiate(brokenPillar, pillar4.transform.position, pillar4.transform.rotation);
				}

				Collider[] colliders = Physics.OverlapSphere(this.transform.position, 20.0f);
				foreach (Collider hit in colliders) {
					if (hit && hit.rigidbody) {
						hit.rigidbody.AddExplosionForce(3000.0f, this.transform.position, 20.0f, 3.0F);
					}
				}
				activated = true;
			}
			if (bossRoomScript.atPillar == throwBombAt) {
				PlayerPrefs.SetInt("playerHealth", 0);
			}
			Destroy(this.gameObject, 0.5f);
		}
	}
}
