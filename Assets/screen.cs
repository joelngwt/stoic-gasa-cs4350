using UnityEngine;
using System.Collections;

public class screen : MonoBehaviour {

	public playIngressGlyph gameManagerScript;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.SetColor("_Color", Color.black);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
//		Debug.Log("Mouse Down.");
		if(gameManagerScript.getIsMainGameInitialised() && !gameManagerScript.getGameIsEnding())
			gameManagerScript.beginAttempt();
	}

}
