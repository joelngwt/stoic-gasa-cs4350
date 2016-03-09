using UnityEngine;
using System.Collections;

public class CrackedRoof : MonoBehaviour {

	public int health;
	[SerializeField] private GameObject brokenRoof;
	public bool spawnedBrokenRoof;
	private BossAI bossAIScript;
	[SerializeField] private Texture sky;
	[SerializeField] private GameObject skyLight;
	[SerializeField] private GameObject targetReticle;

	// Use this for initialization
	void Start () {
		health = Constants.CRACKED_ROOF_HEALTH;
		spawnedBrokenRoof = false;
		
		bossAIScript = GameObject.FindWithTag("Boss").GetComponent<BossAI>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			// Remove all rigidbody constraints so that the roof can start falling
			//this.gameObject.SetActive(false);
			this.GetComponent<Renderer>().material.mainTexture = sky;
			targetReticle.SetActive(false);
			if (spawnedBrokenRoof == false) {
				Instantiate(brokenRoof, this.transform.position, this.transform.rotation);
				Instantiate(skyLight);
				spawnedBrokenRoof = true;
			}
		}
	}
	
	void OnCollisionEnter(Collision col) {
		// Debug.Log ("collided with = " + col.gameObject.name);
		if (col.gameObject.tag == "Boss") {
			bossAIScript.roofHitBoss = true;
			// Debug.Log ("Set to true");
		}
	}
}
