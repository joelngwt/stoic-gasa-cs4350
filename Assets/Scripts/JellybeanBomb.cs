using UnityEngine;
using System.Collections;

public class JellybeanBomb : MonoBehaviour {

	private GameObject player;
	private bool isStopped = false;

	private float blinkDuration = 0.5f;
	private float bombFuseTime = Constants.BOSS_BOMB_FUSE_TIME;
	[SerializeField] private GameObject explosionParticleEffect;
	private EventManager_ActualBossRoom eventManagerActualBossRoom;
	private int throwBombAt;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MainCharacter");
		eventManagerActualBossRoom = GameObject.FindWithTag ("MainCamera").GetComponent<EventManager_ActualBossRoom>();

		// if current pillar is 1
		if (eventManagerActualBossRoom.atPillar == 1) {
			Vector3 throwDirection = new Vector3(-38.73f, 1.5f, 49.21f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 15);
		}
		// if current pillar is 2
		else if (eventManagerActualBossRoom.atPillar == 2) {
			Vector3 throwDirection = new Vector3(-26.63f, 1.5f, -62.53f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 15);
		}
		// If current pillar is 3
		else if (eventManagerActualBossRoom.atPillar == 3) {
			Vector3 throwDirection = new Vector3(11.46f, 1.5f, -37.95f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 18);
		}
		// If current pillar is 4
		else if (eventManagerActualBossRoom.atPillar == 4) {
			Vector3 throwDirection = new Vector3(11.6f, 1.5f, 39.8f) - this.transform.position;
			this.rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y+10, throwDirection.z) * 18.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Stop the bomb if it gets too close (so it won't fly past the player)
		float rangeBetweenBombAndPlayer = Vector3.Distance(this.transform.position, player.transform.position );
		Debug.Log ("range = " + rangeBetweenBombAndPlayer);
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
			Instantiate(explosionParticleEffect, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);

			if (eventManagerActualBossRoom.atPillar == eventManagerActualBossRoom.bossThrowBombAt) {
				// TODO player dies
			}
		}
	}
}
