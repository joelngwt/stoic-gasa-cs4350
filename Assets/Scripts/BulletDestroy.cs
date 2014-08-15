using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

	public float delay;

	// Use this for initialization
	void Awake () {
		Destroy (gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
