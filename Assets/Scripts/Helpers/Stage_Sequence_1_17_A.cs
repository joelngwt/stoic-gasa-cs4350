using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage_Sequence_1_17_A : Stage_Sequence_Helper {
	
	////////////////////////////////////////////////////
	//
	//		Constants
	//
	////////////////////////////////////////////////////

	public readonly string TEXT_MESH_CUBE_RESOURCE_PATH = "Prefabs/Text_Mesh_Cube/Text_Mesh_Cube";
	
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

	////////////////////////////////////////////////////
	//
	//		Script variables
	//
	////////////////////////////////////////////////////
	
	public GameObject whiteboard;
	public GameObject die_1;
	public Dice_Script die_1_script;
	public GameObject die_2;
	public Dice_Script die_2_script;
	public GameObject die_3;
	public Dice_Script die_3_script;
	public GameObject die_game_text_mesh_gameObject;
	public TextMesh die_game_text_mesh;

	public GameObject thumbs_up_down_panel_gameObject;
	public Thumbs_Up_Down_Panel_Script thumbs_script;

	public GameObject bridge_gameObject;

	public Texture2D[] dice_face_textures;

	public int number_of_answers_needed = 10;
	public int number_of_answers_scored = 0;

	public int numbers_size = 6;
	public List<int> available_numbers = new List<int>();
	public int correct_answer = 0;
	
	////////////////////////////////////////////////////
	//
	//		MonoBehaviour functions
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Custom functions for other scripts
	//
	////////////////////////////////////////////////////
	
	/*
	 * Constructor
	 * 
	 * Note: 
	 * Always need an empty constructor for 
	 * derived classes
	 * */
	public Stage_Sequence_1_17_A (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;

		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 17;
	}
	
	/*
	 * Process an update
	 * */
	public override void process_update () 	{
		
		/*
		 * Check if the sequence has been 
		 * initialized
		 * */
		if(sequence_phase_index == 0)
		{
			/*
			 * Sequence has not been initizlied
			 * */
			
			/*
			 * Initialize 
			 * */
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3( 0.0F, 140.0F, 11.0F), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_POSSESSION, 
			                                                                                      new Vector3( 0.0F, 137.0F, -120.0F), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			sequence_phase_index = 1;
		}
		else if(sequence_phase_index == 1)
		{
			if(Vector3.Distance(Camera.main.transform.position, new Vector3(0.0F, 140.0F, 11.0F)) <= 1.00F)
			{
				sequence_phase_index = 2;
			}
		}
		else if(sequence_phase_index == 2)
		{
			
			whiteboard = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
			whiteboard.name = "Whiteboard";
			whiteboard.transform.position = new Vector3(0F, 160F, -100F);
			whiteboard.transform.localScale = new Vector3(175F, 50F, 1F);
			whiteboard.transform.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Dice/dice_background") as Texture2D;

			die_1 = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
			die_1.name = "Die_1";
			die_1.transform.position = new Vector3(30F, 140F, -90F);
			die_1.transform.localScale = new Vector3(20F, 20F, 20F);
			die_1_script = die_1.AddComponent<Dice_Script>();
			
			die_2 = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
			die_2.name = "Die_2";
			die_2.transform.position = new Vector3(00F, 140F, -90F);
			die_2.transform.localScale = new Vector3(20F, 20F, 20F);
			die_2_script = die_2.AddComponent<Dice_Script>();
			
			die_3 = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
			die_3.name = "Die_3";
			die_3.transform.position = new Vector3(-30F, 140F, -90F);
			die_3.transform.localScale = new Vector3(20F, 20F, 20F);
			die_3_script = die_3.AddComponent<Dice_Script>();

			die_game_text_mesh_gameObject = GameObject.Instantiate(Resources.Load(TEXT_MESH_CUBE_RESOURCE_PATH) as GameObject, 
			                                                       new Vector3(60F, 170F, -90F), 
			                                                       Quaternion.Euler(new Vector3(0F, -180F, 0F))) as GameObject;
			die_game_text_mesh_gameObject.name = "Die game instruction";
			die_game_text_mesh = die_game_text_mesh_gameObject.GetComponent<TextMesh>();
			die_game_text_mesh.text = "Shoot the largest die! [ " + number_of_answers_scored.ToString() + " / " + number_of_answers_needed.ToString() + " ]";
			die_game_text_mesh.characterSize = 1;
			die_game_text_mesh.fontSize = 90;
			die_game_text_mesh.color = Color.black;

			thumbs_up_down_panel_gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
			thumbs_up_down_panel_gameObject.layer = 1 << 1;
			thumbs_up_down_panel_gameObject.name = "Thumbs_panel";
			thumbs_up_down_panel_gameObject.transform.localScale = new Vector3(24F, 24F, 0.1F);
			thumbs_up_down_panel_gameObject.GetComponent<MeshRenderer>().enabled = false;
			thumbs_script = thumbs_up_down_panel_gameObject.AddComponent<Thumbs_Up_Down_Panel_Script>();
			
			sequence_phase_index = 3;
		}
		else if(sequence_phase_index == 3)
		{
			/*
			 * Must wait 1 frame for the monobehaviour in the 
			 * dice to initialize so we do it here
			 * */
			generate_dice_numbers();

			sequence_phase_index = 4;
		}
		else if(sequence_phase_index == 4)
		{
			if(die_1_script.is_shot 
			   || die_2_script.is_shot 
			   || die_3_script.is_shot )
			{
				if(die_1_script.is_shot)
				{
					if(die_1_script.stored_int == correct_answer)
					{
						number_of_answers_scored++;

						thumbs_script.thumbs_up();
					}
					else
					{
						
						thumbs_script.thumbs_down();
					}
					
					thumbs_up_down_panel_gameObject.transform.position = new Vector3(die_1.transform.position.x, 
					                                                                 die_1.transform.position.y, 
					                                                                 die_1.transform.position.z + 20.0F);
				}
				if(die_2_script.is_shot)
				{
					if(die_2_script.stored_int == correct_answer)
					{
						number_of_answers_scored++;
						
						thumbs_script.thumbs_up();
					}
					else
					{
						
						thumbs_script.thumbs_down();
					}
					
					thumbs_up_down_panel_gameObject.transform.position = new Vector3(die_2.transform.position.x, 
					                                                                 die_2.transform.position.y, 
					                                                                 die_2.transform.position.z + 20.0F);
				}
				if(die_3_script.is_shot)
				{
					if(die_3_script.stored_int == correct_answer)
					{
						number_of_answers_scored++;
						
						thumbs_script.thumbs_up();
					}
					else
					{
						
						thumbs_script.thumbs_down();
					}
					
					thumbs_up_down_panel_gameObject.transform.position = new Vector3(die_3.transform.position.x, 
					                                                                 die_3.transform.position.y, 
					                                                                 die_3.transform.position.z + 20.0F);
				}

				die_game_text_mesh.text = "Shoot the largest die! [ " + number_of_answers_scored.ToString() + " / " + number_of_answers_needed.ToString() + " ]";

				die_1_script.is_shot = false;
				die_2_script.is_shot = false;
				die_3_script.is_shot = false;

				if(number_of_answers_scored < number_of_answers_needed)
				{
					sequence_phase_index = 3;
				}
				else if(number_of_answers_scored >= number_of_answers_needed)
				{
					sequence_phase_index = 5;
				}
			}
		}
		else if(sequence_phase_index == 5)
		{
			GameObject.Destroy(die_1);
			GameObject.Destroy(die_2);
			GameObject.Destroy(die_3);
			GameObject.Destroy(whiteboard);
			GameObject.Destroy(die_game_text_mesh_gameObject);

			bridge_gameObject = GameObject.Find("Bridge");

			sequence_phase_index = 6;
		}
		else if(sequence_phase_index == 6)
		{
			if(Vector3.Distance(bridge_gameObject.transform.position, new Vector3(-1F, 133.6419F, -15F)) > 1.00F)
			{
				Vector3 position = bridge_gameObject.transform.position;

				bridge_gameObject.transform.position = new Vector3(position.x, 
				                                                   position.y, 
				                                                   Mathf.Max(-15.0F, position.z - 100F * Time.deltaTime));
			}
			else
			{
				sequence_phase_index = 7;
			}
		}
		else if(sequence_phase_index == 7)
		{
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3( 0.0F, 135.0F, -135.0F), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT, 
			                                                                                      new Vector3( 0.0F, 135.0F, -165.0F), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			sequence_phase_index = 8;
		}
		else if(sequence_phase_index == 8)
		{
			if(Camera.main.GetComponent<World_Object_Movement_Helper>().task_complete)
			{
				sequence_phase_index = 9;
			}
		}
		else if(sequence_phase_index == 9)
		{
			PlayerPrefs.SetInt ("currentScore", (int)Camera.main.GetComponent<EventManager_MainHall>().scoreScript.currentScore);
			PlayerPrefs.SetInt ("HMGTotalAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountTotalHMG);
			PlayerPrefs.SetInt ("ShotgunTotalAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountTotalShotgun);
			PlayerPrefs.SetInt ("HMGAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountHMG);
			PlayerPrefs.SetInt ("ShotgunAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountShotgun);
			PlayerPrefs.SetInt("playedTakeDamage", (int)Camera.main.GetComponent<EventManager_MainHall>().lifeScript.playedTakeDamage);
			PlayerPrefs.SetInt("timeLeft", (int)Camera.main.GetComponent<EventManager_MainHall>().timeScript.seconds);
			PlayerPrefs.SetInt("playerLoadedHealth", (int)Camera.main.GetComponent<EventManager_MainHall>().lifeScript.playerHealth);
			
			Application.LoadLevel("DiningHall");
		}
		
		
	}
	
	////////////////////////////////////////////////////
	//
	//		Functions available via inheritance
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Private functions for this script
	//
	////////////////////////////////////////////////////

	private void generate_dice_numbers () 	{

		int number;
		int index;

		correct_answer = 0;

		available_numbers.Clear();

		for(int i = 1; i <= numbers_size; i++)
		{
			number = i + number_of_answers_scored;

			available_numbers.Add((number));
		}

		index = Random.Range(0, available_numbers.Count);
		number = available_numbers[index];
		available_numbers.RemoveAt(index);
		die_1_script.change_value(number);
		correct_answer = Mathf.Max(correct_answer, number);
		
		index = Random.Range(0, available_numbers.Count);
		number = available_numbers[index];
		available_numbers.RemoveAt(index);
		die_2_script.change_value(number);
		correct_answer = Mathf.Max(correct_answer, number);
		
		index = Random.Range(0, available_numbers.Count);
		number = available_numbers[index];
		available_numbers.RemoveAt(index);
		die_3_script.change_value(number);
		correct_answer = Mathf.Max(correct_answer, number);
	}
}
