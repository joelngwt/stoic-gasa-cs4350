using UnityEngine;
using System.Collections;

public class Stage_Sequence_Skewer_1_1 : Stage_Sequence_Helper {
	
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

	GameObject possession_cube;
	Possession_Cube possession_cube_script;

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
	public Stage_Sequence_Skewer_1_1 (Stage_Sequence_Reader given_squence_reader) 	{
		
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
			
			/*
			 * Initialize 
			 * */
			spawn_list.Add(spawnBear(new Vector3( -5.5f, 0f, 245f ), 1)); // target1
			
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
			if(possession_cube != null)
			{
				GameObject.Destroy(possession_cube);
			}

			/*
			 * Spawn the possession cube
			 * */
			possession_cube = spawn_possession_cube(new Vector3( -10.0f, 40f, 223f));
			possession_cube_script = possession_cube.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script.lookAt_position = new Vector3( -41.50F, 0.00F, 285.00F);
			possession_cube_script.up_vector = Vector3.up;

			sequence_phase_index = 3;
		}
		else if(sequence_phase_index == 3)
		{
			/*
			 * Check if the player is in the correct 
			 * position
			 * */
			if(Vector3.Distance(Camera.main.transform.position, possession_cube.transform.position) <= 1.00F)
			{
				sequence_phase_index = 4;
			}
		}
		else if(sequence_phase_index == 4)
		{
			GameObject.Destroy(possession_cube);
			
			possession_cube = spawn_possession_cube(new Vector3( -7.0f, 9f, 301f));
			possession_cube_script = possession_cube.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script.lookAt_position = new Vector3( -13.50F, 19.50F, 61.00F);
			possession_cube_script.up_vector = Vector3.up;
			
			sequence_phase_index = 5;
		}
		else if(sequence_phase_index == 5)
		{
			/*
			 * Check if the player is in the correct 
			 * position
			 * */
			if(Vector3.Distance(Camera.main.transform.position, possession_cube.transform.position) <= 1.00F)
			{
				sequence_phase_index = 2;
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
