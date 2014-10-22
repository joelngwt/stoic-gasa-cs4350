using UnityEngine;
using System.Collections;

public class Dice_Script : MonoBehaviour {
	
	public readonly string DIE_NUMBER_0_RESOURCE_PATH = "Dice/Dice_0";
	public readonly string DIE_NUMBER_1_RESOURCE_PATH = "Dice/Dice_1";
	public readonly string DIE_NUMBER_2_RESOURCE_PATH = "Dice/Dice_2";
	public readonly string DIE_NUMBER_3_RESOURCE_PATH = "Dice/Dice_3";
	public readonly string DIE_NUMBER_4_RESOURCE_PATH = "Dice/Dice_4";
	public readonly string DIE_NUMBER_5_RESOURCE_PATH = "Dice/Dice_5";
	public readonly string DIE_NUMBER_6_RESOURCE_PATH = "Dice/Dice_6";
	public readonly string DIE_NUMBER_7_RESOURCE_PATH = "Dice/Dice_7";
	public readonly string DIE_NUMBER_8_RESOURCE_PATH = "Dice/Dice_8";
	public readonly string DIE_NUMBER_9_RESOURCE_PATH = "Dice/Dice_9";
	public readonly string DIE_NUMBER_10_RESOURCE_PATH = "Dice/Dice_10";
	public readonly string DIE_NUMBER_11_RESOURCE_PATH = "Dice/Dice_11";
	public readonly string DIE_NUMBER_12_RESOURCE_PATH = "Dice/Dice_12";
	public readonly string DIE_NUMBER_13_RESOURCE_PATH = "Dice/Dice_13";
	public readonly string DIE_NUMBER_14_RESOURCE_PATH = "Dice/Dice_14";
	public readonly string DIE_NUMBER_15_RESOURCE_PATH = "Dice/Dice_15";
	public readonly string DIE_NUMBER_16_RESOURCE_PATH = "Dice/Dice_16";


	public Texture2D[] dice_face_textures;

	public float spin_timer_default = 0.25F;
	public float spin_speed = 1800.00F;
	public float spin_timer = 0.00F;

	public int stored_int = 0;
	public bool is_shot = false;

	// Use this for initialization
	void Start () {
		
		/*
		 * Initialize the data structures
		 * */
		dice_face_textures = new Texture2D[17];
		dice_face_textures[0] = Resources.Load(DIE_NUMBER_0_RESOURCE_PATH) as Texture2D;
		dice_face_textures[1] = Resources.Load(DIE_NUMBER_1_RESOURCE_PATH) as Texture2D;
		dice_face_textures[2] = Resources.Load(DIE_NUMBER_2_RESOURCE_PATH) as Texture2D;
		dice_face_textures[3] = Resources.Load(DIE_NUMBER_3_RESOURCE_PATH) as Texture2D;
		dice_face_textures[4] = Resources.Load(DIE_NUMBER_4_RESOURCE_PATH) as Texture2D;
		dice_face_textures[5] = Resources.Load(DIE_NUMBER_5_RESOURCE_PATH) as Texture2D;
		dice_face_textures[6] = Resources.Load(DIE_NUMBER_6_RESOURCE_PATH) as Texture2D;
		dice_face_textures[7] = Resources.Load(DIE_NUMBER_7_RESOURCE_PATH) as Texture2D;
		dice_face_textures[8] = Resources.Load(DIE_NUMBER_8_RESOURCE_PATH) as Texture2D;
		dice_face_textures[9] = Resources.Load(DIE_NUMBER_9_RESOURCE_PATH) as Texture2D;
		dice_face_textures[10] = Resources.Load(DIE_NUMBER_10_RESOURCE_PATH) as Texture2D;
		dice_face_textures[11] = Resources.Load(DIE_NUMBER_11_RESOURCE_PATH) as Texture2D;
		dice_face_textures[12] = Resources.Load(DIE_NUMBER_12_RESOURCE_PATH) as Texture2D;
		dice_face_textures[13] = Resources.Load(DIE_NUMBER_13_RESOURCE_PATH) as Texture2D;
		dice_face_textures[14] = Resources.Load(DIE_NUMBER_14_RESOURCE_PATH) as Texture2D;
		dice_face_textures[15] = Resources.Load(DIE_NUMBER_15_RESOURCE_PATH) as Texture2D;
		dice_face_textures[16] = Resources.Load(DIE_NUMBER_16_RESOURCE_PATH) as Texture2D;

		gameObject.transform.GetComponent<MeshRenderer>().material.mainTexture = dice_face_textures[stored_int];
	}
	
	// Update is called once per frame
	void Update () {
	
		if(spin_timer > 0.00F)
		{
			spin_timer = Mathf.Max((spin_timer - Time.deltaTime), 0.00F);

			gameObject.transform.Rotate(new Vector3( 0.00F, spin_speed * Time.deltaTime, 0.00F));
		}
		else if(spin_timer == 0.00F)
		{
			gameObject.transform.rotation = Quaternion.Euler(new Vector3(0F, -180F, 180F));

			spin_timer = -1.00F;
		}
	}

	public void change_value (int given_int) 	{

		if(given_int >= 0 
		   && given_int <= 16)
		{
			stored_int = given_int;
			
			spin_timer = spin_timer_default;
			
			gameObject.GetComponent<MeshRenderer>().material.mainTexture = dice_face_textures[given_int];
		}
	}

	public void get_shot () 	{

		if(spin_timer <= 0.00F)
		{
			is_shot = true;
		}
	}
}
