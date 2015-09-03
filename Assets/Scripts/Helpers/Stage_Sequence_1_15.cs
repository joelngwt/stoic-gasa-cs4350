using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_15 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_1_15 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 15;
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
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_lookAt_task(new Vector3(-33f,21f,139f), 
			                                                                         Vector3.up,
			                                                                         World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(65.1f, 30.8f, 55.9f), 
			                                                                           0.05f*World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(49f, 30.8f, 64.9f), 
			                                                                           0.05f*World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);

			/*
			 * Initialize 
			 * */
			spawn_list.Add(spawnBear(new Vector3( 34f, 21f, 154f ), 0));
			spawn_list.Add(spawnBear(new Vector3( -23f, 0f, 169f ), 1));
			spawn_list.Add(spawnBear(new Vector3( -66f, 0f, 107f ), 2));
			spawn_list.Add(spawnBear(new Vector3( -64f, 21f, 69f ), 0));
			spawn_list.Add(spawnBear(new Vector3( -64f, 21f, 138f ), 0));
			spawn_list.Add(spawnBear(new Vector3( 12f, 0f, 128f ), 2));
			spawn_list.Add(spawnLollipop(new Vector3( 55f, 50f, 140f )));
			
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
			attached_reader.attached_sequence = new Stage_Sequence_1_16(attached_reader);
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