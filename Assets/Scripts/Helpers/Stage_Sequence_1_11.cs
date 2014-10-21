using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_11 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_1_11 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 11;
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
			spawn_list.Add(spawnBear(new Vector3( 28f, 21f, 157f ), 0));
			spawn_list.Add(spawnBear(new Vector3( 29f, 21f, 148f  ), 2));
			spawn_list.Add(spawnBear(new Vector3( 30f, 21f, 140f  ), 1));
			
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
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_lookAt_task(new Vector3(23,8f, 139f), 
			                                                                         Vector3.up, 
			                                                                         World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(0f, 8f, 141f), 
			                                                                           World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3(37f, 4f, 91f), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT, 
			                                                                                      new Vector3(40,20,134f), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			attached_reader.attached_sequence = new Stage_Sequence_1_12(attached_reader);
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