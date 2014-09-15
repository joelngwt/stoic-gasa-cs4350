using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_12 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_1_12 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 12;
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
			spawn_list.Add(spawnBear(new Vector3( 34f, 21f, 138f ), 0));
			spawn_list.Add(spawnBear(new Vector3( 64f, 21f, 105f ), 0));
			spawn_list.Add(spawnBear(new Vector3( 23f, 0f, 169f ), 2));
			spawn_list.Add(spawnLollipop(new Vector3 ( -55f, 100f, 280f )));
			
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
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(38.2f, 8f, 94.8f), 
			                                                                           World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(38.2f, 31f, 134.7f), 
			                                                                           World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3(33.1f, 33.8f, 160.6f), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT, 
			                                                                                      new Vector3(30f, 32f, 170f), 
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
				attached_reader.attached_sequence = new Stage_Sequence_1_13(attached_reader);
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