using UnityEngine;
using System.Collections;

public class Stage_Sequence_2_1 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_2_1 (Stage_Sequence_Reader given_squence_reader) 	{

		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;

		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 1;
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
				
			/*Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3(-25.574f, 3.5f, 268.1f), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT,new Vector3(-6f, 8f, 207f), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			*/
			/*
			 * Initialize 
			 * */
			//0 duck
			//left
			//right
			spawn_list.Add(spawnBear (new Vector3( -8f, 0f, -18f ), 1)); // target1
			               spawn_list.Add(spawnBear (new Vector3( 26f, 0f, -5f  ), 0)); // target2
			               spawn_list.Add(spawnBear (new Vector3( 22f, 0f, 34.5f ),  0)); // target5
			spawn_list.Add(spawnBear (new Vector3( -20f, 0f, -30f ), 2)); // target6
			               spawn_list.Add(spawnBear (new Vector3(  -6.5f, 0f, 53f ),  1)); // target7
			               spawn_list.Add(spawnBear (new Vector3( 5f, 0f, -5f  ),  0)); // target8
			               spawn_list.Add(spawnLollipop (new Vector3 ( 10f, 10f, 65f ))); // target9

			sequence_phase_index = 1;
		}
		else if(sequence_phase_index == 1)
		{
			/*
			 * Wave is complete if all the spawns are 
			 * destroyed
			 * */
			if(check_spawn_list_empty())
			{
				sequence_phase_index = 2;
			}
		}
		else if(sequence_phase_index == 2)
		{
			/*
			 * Move on to next sequence
			 * */
			attached_reader.attached_sequence = new Stage_Sequence_2_2(attached_reader);
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
