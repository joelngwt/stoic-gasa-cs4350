using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_10 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_1_10 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 10;
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
			spawn_list.Add(spawnBear(new Vector3( 4f, 0f, 133f ), 0));
			spawn_list.Add(spawnBear(new Vector3( 15f, 21f, 61f ), 0));
			spawn_list.Add(spawnBear(new Vector3( -15f, 21f, 61f ), 0));
			spawn_list.Add(spawnEgg(new Vector3 ( 0, 0, 70 )));
			
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
			 * All the spawns are dead. Move the main 
			 * character
			 * */
			
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(0f, 8f, 141f), 
			                                                                           World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_lookAt_task(new Vector3(29,20f, 139f), 
			                                                                         Vector3.up, 
			                                                                         World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			sequence_phase_index = 3;
		}
		else if(sequence_phase_index == 3)
		{
			/*
			 * Waiting for character to reach destination
			 * */
			if(Camera.main.GetComponent<World_Object_Movement_Helper>().task_complete)
			{
				/*
				 * Move on to next sequence
				 * */
				attached_reader.attached_sequence = new Stage_Sequence_1_11(attached_reader);
			}
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