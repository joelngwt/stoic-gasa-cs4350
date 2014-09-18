using UnityEngine;
using System.Collections;

public class KinderSurpriseBomb : MonoBehaviour {

	private GameObject pillar1;
	private GameObject pillar2;
	private GameObject pillar3;
	private GameObject pillar4;
	private EventManager_ActualBossRoom bossRoomScript;
	private GameObject player;
	[SerializeField] private GameObject lollipopPrefab;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MainCharacter");
		pillar1 = GameObject.FindWithTag ("Pillar1");
		pillar2 = GameObject.FindWithTag ("Pillar2");
		pillar3 = GameObject.FindWithTag ("Pillar3");
		pillar4 = GameObject.FindWithTag ("Pillar4");
		bossRoomScript = GameObject.FindWithTag("MainCamera").GetComponent<EventManager_ActualBossRoom>();
		
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
		}
	}
	
	void OnCollisionEnter(Collision col) {
		Debug.Log ("collided with = " + col.gameObject.tag);
		if (col.gameObject.tag == "Ground") {
			spawnLollipop(this.transform.position);
			Destroy(this.gameObject, 0.1f);
		}
	}
	
	private void spawnLollipop(Vector3 position) {
		GameObject lollipop = Instantiate(lollipopPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject;
		Vector3 direction = player.transform.position - lollipop.transform.position; // make the instantiated bear face the camera
		lollipop.transform.rotation = Quaternion.LookRotation(direction);
	}
}
