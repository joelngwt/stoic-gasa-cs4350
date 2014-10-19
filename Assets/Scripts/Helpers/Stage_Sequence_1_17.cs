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
			possession_cube_script_1 = possession_cube_1.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script_1.lookAt_position = new Vector3( 0.0F, 22.0F, -15.0F);
			possession_cube_script_1.up_vector = Vector3.up;
			
			possession_cube_2 = spawn_possession_cube(new Vector3( 0.0F, 22.0F, -15.0F));
			possession_cube_script_2 = possession_cube_2.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script_2.lookAt_position = new Vector3( 0.0F, 40.0F, -121.0F);
			possession_cube_script_2.up_vector = Vector3.up;
			
			sequence_phase_index = 3;
		}
		else if(sequence_phase_index == 3)
		{
			
			spawn_list.Add(spawnBear(new Vector3( -20.65516f, 0f, 277f ), 1)); // target1
			spawn_list.Add(spawnBear(new Vector3( -17.4f, 0f, 263f ), 2)); // target2
			spawn_list.Add(spawnBear(new Vector3( -0.33f, 3.6f, 242f ), 1)); // target3

			sequence_phase_index = 4;
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