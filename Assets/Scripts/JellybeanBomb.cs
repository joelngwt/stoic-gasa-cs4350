using UnityEngine;
using System.Collections;

public class JellybeanBomb : MonoBehaviour {

	private GameObject player;
	private bool isStopped = false;

	private float blinkDuration = 0.5f;
	private float bombFuseTime = Constants.BOSS_BOMB_FUSE_TIME;
	[SerializeField] private GameObject explosionParticleEffect;
	private EventManager_ActualBossRoom eventManagerActualBossRoomScript;
	private BossAI bossAIScript;
	private int throwBombAt;
	private bool activated;
	[SerializeField] private AudioClip bombExplosion;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MainCharacter");
		eventManagerActualBossRoomScript = GameObject.FindWithTag("MainCamera").GetComponent<EventManager_ActualBossRoom>();
		bossAIScript = GameObject.FindWithTag("Boss").GetComponent<BossAI>();
		activated = false;

		// if current pillar is 1
		if (eventManagerActualBossRoomScript.atPillar == 1) {
			Vector3 throwDirection = new Vector3(-38.73f, 1.5f, 49.21f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 15);
			throwBombAt = 1;
		}
		// if current pillar is 2
		else if (eventManagerActualBossRoomScript.atPillar == 2) {
			Vector3 throwDirection = new Vector3(-26.63f, 1.5f, -62.53f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 15);
			throwBombAt = 2;
		}
		// If current pillar is 3
		else if (eventManagerActualBossRoomScript.atPillar == 3) {
			Vector3 throwDirection = new Vector3(11.46f, 1.5f, -37.95f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 18);
			throwBombAt = 3;
		}
		// If current pillar is 4
		else if (eventManagerActualBossRoomScript.atPillar == 4) {
			Vector3 throwDirection = new Vector3(11.6f, 1.5f, 39.8f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 18.5f);
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
			bossAIScript.currentPhase += 1;
			if (activated == false) {
				audio.PlayOneShot(bombExplosion);
				Instantiate(explosionParticleEffect, this.transform.position, this.transform.rotation);
				activated = true;
			}
			if (eventManagerActualBossRoomScript.atPillar == throwBombAt) {
				PlayerPrefs.SetInt("playerHealth", 0);
			}
			Destroy(this.gameObject, 0.5f);
		}
	}
}
