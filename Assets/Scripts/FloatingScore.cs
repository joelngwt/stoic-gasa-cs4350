using UnityEngine;
using System.Collections;

public class FloatingScore : MonoBehaviour {

	private float floatDuration;
	private Vector3 direction;
	private GameObject camera;

	// Use this for initialization
	void Start () {
		camera = Camera.main.gameObject;
		floatDuration = 0.5f;
		direction = camera.transform.position - this.transform.position;
		this.transform.rotation = Quaternion.LookRotation(-direction);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.up * 0.5f);
		
		floatDuration -= Time.deltaTime;
		if(floatDuration < 0){
			Destroy(this.gameObject);
		}
	}
}
