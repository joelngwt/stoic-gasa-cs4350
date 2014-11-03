using UnityEngine;
using System.Collections;

public class Stage_Sequence_2_3 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_2_3 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 3;
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
			spawn_list.Add(spawnBear (new Vector3( 46.5f, 0f, -17.5f ), 1)); // target17
			               spawn_list.Add(spawnBear (new Vector3( 46.5f, 0f, 53f ),  1)); // target18
			               spawn_list.Add(spawnBear (new Vector3( 53.6f, 0f, 32f ),  2)); // target19
			               spawn_list.Add(spawnBear (new Vector3( 53.6f, 0f, -27f),  2)); // target20
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
				attached_reader.attached_sequence = new Stage_Sequence_2_4(attached_reader);
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