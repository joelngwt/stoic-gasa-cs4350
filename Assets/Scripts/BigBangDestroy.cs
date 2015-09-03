using UnityEngine;
using System.Collections;

public class BigBangDestroy : MonoBehaviour {

	public float delay = 5f;
	// Use this for initialization
	void Awake () {
		Destroy (gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
