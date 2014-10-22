using UnityEngine;
using System.Collections;

public class Thumbs_Up_Down_Panel_Script : MonoBehaviour {

	public readonly string THUMBS_UP_RESOURCE_PATH = "Dice/Thumbs_up";
	public readonly string THUMBS_DOWN_RESOURCE_PATH = "Dice/Thumbs_down";

	public Texture2D thumbs_up_texture;
	public Texture2D thumbs_down_texture;

	public float timer_default = 0.25F;
	public float timer = 0.00F;

	// Use this for initialization
	void Start () {
	
		/*
		 * Read the resources
		 * */
		thumbs_up_texture = Resources.Load(THUMBS_UP_RESOURCE_PATH) as Texture2D;
		thumbs_down_texture = Resources.Load(THUMBS_DOWN_RESOURCE_PATH) as Texture2D;

		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(timer > 0.00F)
		{
			timer = timer - Time.deltaTime;
		}
		else
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	public void thumbs_up () 	{

		timer = timer_default;

		gameObject.GetComponent<MeshRenderer>().enabled = true;
		gameObject.GetComponent<MeshRenderer>().material.mainTexture = thumbs_up_texture;
	}

	public void thumbs_down () 	{
		
		timer = timer_default;
		
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		gameObject.GetComponent<MeshRenderer>().material.mainTexture = thumbs_down_texture;
	}
}
