﻿using UnityEngine;

public class VerticeBehavior : MonoBehaviour {

	GameObject gameManager;
	public playIngressGlyph gameManagerScript;
	public int objName;

	// Use this for initialization
	void Start () {
		renderer.material.SetColor("_Color", Color.gray);

	}
	
	// Update is called once per frame
	void Update () {
		if(!gameManagerScript.getGameIsEnding()){
			if(!gameManagerScript.getIsMouseDown()){
				renderer.material.SetColor("_Color", Color.gray);	
			}
		}
	}

	void OnMouseDown(){
//		Debug.Log("Mouse Down.");
		if(gameManagerScript.getIsMainGameInitialised())
			gameManagerScript.beginAttempt();
	}

	void OnMouseOver(){
//		Debug.Log("Mouse Enter.");

		if(gameManagerScript.getIsMouseDown() && 
		   gameManagerScript.getLastVertice() != objName && 
		   gameManagerScript.getIsMainGameInitialised() && 
		   !gameManagerScript.getGameIsEnding()){
//			Debug.Log("Mouse Activated.");
			gameManagerScript.touchedVertice(objName);
//			Handheld.Vibrate();
			renderer.material.SetColor("_Color", Color.red);
		}
	}

	void OnMouseUp(){
		if(gameManagerScript.getIsMouseDown() && gameManagerScript.getIsMainGameInitialised()){
			gameManagerScript.finishedAttempt();
		}
		renderer.material.SetColor("_Color", Color.gray);
	}
}