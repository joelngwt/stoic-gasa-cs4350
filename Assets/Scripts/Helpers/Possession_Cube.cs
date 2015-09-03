using UnityEngine;
using System.Collections;

public class Possession_Cube : MonoBehaviour {
	
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

	public Vector3 lookAt_position;
	public Vector3 up_vector;

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
	 * Issue a task to the main character 
	 * to move to the cube's position
	 * */
	public void trigger_possession () 	{
		
		/*
		 * For now, only the main 
		 * camera is representative of the 
		 * player character, so we will 
		 * assume that the movement script 
		 * is on the main camera
		 * */
		Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(transform.parent.transform.position, 
		                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_POSSESSION, 
		                                                                                      lookAt_position, 
		                                                                                      up_vector, 
		                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
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
