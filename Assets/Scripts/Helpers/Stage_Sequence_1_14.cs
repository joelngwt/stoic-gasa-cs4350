using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_14 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_1_14 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 14;
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
			spawn_list.Add(spawnBear(new Vector3( -60f, 0f, 234f ), 2));
			spawn_list.Add(spawnBear(new Vector3( 38f, 0f, 260f ), 2));
			spawn_list.Add(spawnLollipop(new Vector3( -55f, 100f, 280f )));
			
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
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3(65.1f, 33.8f, 136.6f), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT, 
			                                                                                      new Vector3(31.5f, 33f, 81f), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(65.1f, 33.8f, 55.9f), 
			                                                                           World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(49f, 35.8f, 64.9f), 
			                                                                           World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
			
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
				attached_reader.attached_sequence = new Stage_Sequence_1_15(attached_reader);
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