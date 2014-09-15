using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_6 : Stage_Sequence_Helper {
	
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
	public Stage_Sequence_1_6 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 6;
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
			spawn_list.Add(spawnBear(new Vector3( -34f, 21f, 154f ), 0)); // target19
			spawn_list.Add(spawnBear(new Vector3( -55f, 21f, 154f ), 0)); // target20
			spawn_list.Add(spawnBear(new Vector3( -73f, 21f, 154f ), 0)); // target21
			spawn_list.Add(spawnBear(new Vector3( 34f, 21f, 154f ), 0)); // target22
			
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
			attached_reader.attached_sequence = new Stage_Sequence_1_7(attached_reader);
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