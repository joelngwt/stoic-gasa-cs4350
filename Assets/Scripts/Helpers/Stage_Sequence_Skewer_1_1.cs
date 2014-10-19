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

			/*
			 * Spawn the possession cube
			 * */
			possession_cube = spawn_possession_cube(new Vector3( 0.0F, 64.0F, -121.0F));
			possession_cube_script = possession_cube.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script.lookAt_position = new Vector3( 0.0F, 22.0F, -15.0F);
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
			
			possession_cube = spawn_possession_cube(new Vector3( 0.0F, 22.0F, -15.0F));
			possession_cube_script = possession_cube.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script.lookAt_position = new Vector3( 0.0F, 40.0F, -121.0F);
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
				sequence_phase_index = 6;
			}
		}
		else if(sequence_phase_index == 6)
		{
			GameObject.Destroy(possession_cube);

			/*
			 * Spawn the possession cube
			 * */
			possession_cube = spawn_possession_cube(new Vector3( 0.0F, 64.0F, -121.0F));
			possession_cube_script = possession_cube.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script.lookAt_position = new Vector3( 0.0F, 22.0F, -15.0F);
			possession_cube_script.up_vector = Vector3.up;
			
			sequence_phase_index = 7;
		}
		else if(sequence_phase_index == 7)
		{
			/*
			 * Check if the player is in the correct 
			 * position
			 * */
			if(Vector3.Distance(Camera.main.transform.position, possession_cube.transform.position) <= 1.00F)
			{
				sequence_phase_index = 8;
			}
		}
		else if(sequence_phase_index == 8)
		{
			GameObject.Destroy(possession_cube);
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_lookAt_task(new Vector3(0.0F, 138.0F, 13.0F), 
			                                                                         Vector3.up, 
			                                                                         World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			sequence_phase_index = 9;
		}
		else if(sequence_phase_index == 9)
		{
			GameObject.Destroy(possession_cube);
			
			possession_cube = spawn_possession_cube(new Vector3( 0.0F, 140.0F, 11.0F));
			possession_cube_script = possession_cube.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script.lookAt_position = new Vector3( 0.0F, 137.0F, -120.0F);
			possession_cube_script.up_vector = Vector3.up;
			
			sequence_phase_index = 10;
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
