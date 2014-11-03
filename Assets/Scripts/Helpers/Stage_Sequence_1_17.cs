using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_17 : Stage_Sequence_Helper {
	
	////////////////////////////////////////////////////
	//
	//		Constants
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Script variables
	//
	////////////////////////////////////////////////////
	
	GameObject possession_cube_1;
	Possession_Cube possession_cube_script_1;
	
	GameObject possession_cube_2;
	Possession_Cube possession_cube_script_2;
	
	GameObject possession_cube_3;
	Possession_Cube possession_cube_script_3;
	
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
	public Stage_Sequence_1_17 (Stage_Sequence_Reader given_squence_reader) 	{
		
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
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3( 0.0F, 22.0F, -15.0F), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_POSSESSION, 
			                                                                                      new Vector3( 0.0F, 40.0F, -121.0F), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			sequence_phase_index = 1;
		}
		else if(sequence_phase_index == 1)
		{
			if(Vector3.Distance(Camera.main.transform.position, new Vector3(0.0F, 22.0F, -15.0F)) <= 1.00F)
			{
				sequence_phase_index = 2;
			}
		}
		else if(sequence_phase_index == 2)
		{

			possession_cube_1 = spawn_possession_cube(new Vector3( 0.0F, 64.0F, -121.0F));
			possession_cube_script_1 = possession_cube_1.transform.FindChild("3D_Model").gameObject.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script_1.lookAt_position = new Vector3( 0.0F, 22.0F, -15.0F);
			possession_cube_script_1.up_vector = Vector3.up;
			
			possession_cube_2 = spawn_possession_cube(new Vector3( 0.0F, 22.0F, -15.0F));
			possession_cube_script_2 = possession_cube_2.transform.FindChild("3D_Model").gameObject.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script_2.lookAt_position = new Vector3( 0.0F, 40.0F, -121.0F);
			possession_cube_script_2.up_vector = Vector3.up;
			
			sequence_phase_index = 3;
		}
		else if(sequence_phase_index == 3)
		{
			
			spawn_list.Add(spawnBear(new Vector3( 64F, 22F, -100F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -67F, 22F, -100F ), 2));
			spawn_list.Add(spawnBear(new Vector3( -20F, 62f, -120F ), 1));

			sequence_phase_index = 4;
		}
		else if(sequence_phase_index == 4)
		{
			/*
			 * Wave is complete if all the spawns are 
			 * destroyed
			 * */
			if(check_spawn_list_empty())
			{
				sequence_phase_index = 5;
			}
		}
		else if(sequence_phase_index == 5)
		{
			
			spawn_list.Add(spawnBear(new Vector3( 50F, 22F, -115F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -57F, 22F, -115F ), 2));
			spawn_list.Add(spawnBear(new Vector3( 50F, 66f, -98F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 69F, 66F, -98F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 12F, 62F, -118F ), 2));
			spawn_list.Add(spawnBear(new Vector3( -54F, 22f, -99F ), 1));
			
			sequence_phase_index = 6;
		}
		else if(sequence_phase_index == 6)
		{
			/*
			 * Wave is complete if all the spawns are 
			 * destroyed
			 * */
			if(check_spawn_list_empty())
			{
				sequence_phase_index = 7;
			}
		}
		else if(sequence_phase_index == 7)
		{
			
			spawn_list.Add(spawnBear(new Vector3( 37F, 21F, -12F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 49F, 49F, -12F ), 2));
			spawn_list.Add(spawnBear(new Vector3( 69F, 49f, -12F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 17F, 21F, 33F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -21F, 21F, 33F ), 2));
			spawn_list.Add(spawnBear(new Vector3( -69F, 21f, -15F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -63F, 48f, -12F ), 1));
			
			sequence_phase_index = 8;
		}
		else if(sequence_phase_index == 8)
		{
			/*
			 * Wave is complete if all the spawns are 
			 * destroyed
			 * */
			if(check_spawn_list_empty())
			{
				sequence_phase_index = 9;
			}
		}
		else if(sequence_phase_index == 9)
		{
			
			spawn_list.Add(spawnBear(new Vector3( 64F, 22F, -100F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -67F, 22F, -100F ), 2));
			spawn_list.Add(spawnBear(new Vector3( -20F, 62f, -120F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 50F, 22F, -115F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -57F, 22F, -115F ), 2));
			spawn_list.Add(spawnBear(new Vector3( 50F, 66f, -98F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 69F, 66F, -98F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 12F, 62F, -118F ), 2));
			spawn_list.Add(spawnBear(new Vector3( -54F, 22f, -99F ), 1));
			
			sequence_phase_index = 10;
		}
		else if(sequence_phase_index == 10)
		{
			/*
			 * Wave is complete if all the spawns are 
			 * destroyed
			 * */
			if(check_spawn_list_empty())
			{
				sequence_phase_index = 11;
			}
		}
		else if(sequence_phase_index == 11)
		{
			
			
			spawn_list.Add(spawnBear(new Vector3( 37F, 21F, -12F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 49F, 49F, -12F ), 2));
			spawn_list.Add(spawnBear(new Vector3( 69F, 49f, -12F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 17F, 21F, 33F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -21F, 21F, 33F ), 2));
			spawn_list.Add(spawnBear(new Vector3( -69F, 21f, -15F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -63F, 48f, -12F ), 1));
			
			spawn_list.Add(spawnBear(new Vector3( -53F, 48F, -14F ), 1));
			spawn_list.Add(spawnBear(new Vector3( -20F, 21F, 14F ), 2));
			spawn_list.Add(spawnBear(new Vector3( 19F, 21f, -13F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 68F, 21F, -13F ), 1));
			spawn_list.Add(spawnBear(new Vector3( 5F, 21F, 47F ), 2));
			//spawn_list.Add(spawnBear(new Vector3( -36F, 21f, 144F ), 1));
			//spawn_list.Add(spawnBear(new Vector3( 28F, 21f, 144F ), 1));
			
			sequence_phase_index = 12;
		}
		else if(sequence_phase_index == 12)
		{
			/*
			 * Wave is complete if all the spawns are 
			 * destroyed
			 * */
			if(check_spawn_list_empty())
			{
				sequence_phase_index = 13;
			}
		}
		else if(sequence_phase_index == 13)
		{
			if(Vector3.Distance(Camera.main.transform.position, new Vector3(0.0F, 64.0F, -121.0F)) <= 1.00F)
			{
				GameObject.Destroy(possession_cube_1);
				GameObject.Destroy(possession_cube_2);

				sequence_phase_index = 14;
			}
		}
		else if(sequence_phase_index == 14)
		{
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3( 0.0F, 64.0F, -121.0F), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_POSSESSION, 
			                                                                                      new Vector3( 0.0F, 140.0F, 11.0F), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			
			possession_cube_3 = spawn_possession_cube(new Vector3( 0.0F, 140.0F, 11.0F));
			possession_cube_script_3 = possession_cube_3.transform.FindChild("3D_Model").gameObject.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script_3.lookAt_position = new Vector3( 0.0F, 137.0F, -120.0F);
			possession_cube_script_3.up_vector = Vector3.up;


			sequence_phase_index = 15;
		}
		else if(sequence_phase_index == 5)
		{
			if(Vector3.Distance(Camera.main.transform.position, new Vector3(0.0F, 140.0F, 11.0F)) <= 1.00F)
			{
				sequence_phase_index = 6;
			}
		}
		else if(sequence_phase_index == 16)
		{
			
			GameObject.Destroy(possession_cube_3);
			
			sequence_phase_index = 17;
		}
		else if(sequence_phase_index == 17)
		{
			/*
			 * Move on to next sequence
			 * */
			attached_reader.attached_sequence = new Stage_Sequence_1_18(attached_reader);
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
}