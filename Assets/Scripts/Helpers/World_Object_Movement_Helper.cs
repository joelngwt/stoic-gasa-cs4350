/**
 * 
 * This class helps handle movement for the attached world object
 * 
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World_Object_Movement_Helper : MonoBehaviour{
	
	////////////////////////////////////////////////////
	//
	//		Constants
	//
	////////////////////////////////////////////////////

	public static readonly float MOVEMENT_COMPLETION_DISTANCE_THRESHOLD = 0.01F;
	public static readonly float LOOKAT_COMPLETION_ANGLE_THRESHOLD = 5.00F;

	public static readonly float PSEUDO_INFINITE_DISTANCE = 100.00F;

	public static readonly float PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT = 80.00F;
	public static readonly float PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT = 180.00F;
	public static readonly float PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_POSSESSION = 80.00F;
	
	////////////////////////////////////////////////////
	//
	//		Script variables
	//
	////////////////////////////////////////////////////

	public GameObject attached_world_object;

	public bool task_complete;
	public List<Vector3> objective_destination_position_list;
	public List<float> objective_movement_speed_list;

	public List<Vector3> objective_lookAt_position_list;
	public List<Vector3> objective_lookAt_upVector_list;
	public List<float> objective_rotation_speed_list;

	public List<GameObject> associated_world_object_list;

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

	public void Start () 	{
		
		/*
		 * Create the data structures
		 * */
//		objective_destination_position_list = new List<Vector3>();
//		objective_movement_speed_list = new List<float>();
//		objective_lookAt_position_list = new List<Vector3>();
//		objective_lookAt_upVector_list = new List<Vector3>();
//		objective_rotation_speed_list = new List<float>();
//		
//		associated_world_object_list = new List<GameObject>();
		
		/*
		 * Attach the given world object to 
		 * reference
		 * */
		attached_world_object = this.gameObject;
		
		/*
		 * By default the task is complete because 
		 * there is no default task
		 * */
		task_complete = true;
	}

	public void Update () 	{

		process_update();
	}

	/*
	 * Constructor
	 * */
	public World_Object_Movement_Helper (GameObject given_world_object) 	{

		/*
		 * Create the data structures
		 * */
		objective_destination_position_list = new List<Vector3>();
		objective_movement_speed_list = new List<float>();
		objective_lookAt_position_list = new List<Vector3>();
		objective_lookAt_upVector_list = new List<Vector3>();
		objective_rotation_speed_list = new List<float>();

		associated_world_object_list = new List<GameObject>();


		/*
		 * Attach the given world object to 
		 * reference
		 * */
		attached_world_object = given_world_object;

		/*
		 * By default the task is complete because 
		 * there is no default task
		 * */
		task_complete = true;
	}

	/*
	 * Clears the objective list and marks 
	 * the task as complete
	 * */
	public void clear_objectives () 	{

		objective_destination_position_list.Clear();
		objective_lookAt_position_list.Clear();
		task_complete = true;
	}

	/*
	 * Adds a world object as an associated 
	 * world object
	 * */
	public void add_associated_world_object (GameObject given_world_object) 	{

		/*
		 * Append to the end of the collection
		 * */
		associated_world_object_list.Add(given_world_object);

		/*
		 * Initialize the position and rotation of 
		 * the associated objects
		 * */
		given_world_object.transform.position = attached_world_object.transform.position;
		given_world_object.transform.rotation = attached_world_object.transform.rotation;
	}
	
	/*
	 * Assigns a new desitnation positions and 
	 * points to look at
	 * */
	public void add_movement_task (Vector3 given_destination_position, 
	                               float given_movement_speed) 	{
		
		Vector3 chosen_destination_position;
		float chosen_movement_speed;

		Vector3 chosen_lookAt_position;
		Vector3 chosen_lookAt_upVector;
		float chosen_rotation_speed;

		Vector3 chosen_forward_vector;

		/*
		 * There is no given lookAt. We will 
		 * copy the latest lookAt if possible, 
		 * or else we will use the attached object's 
		 * current lookAt
		 * */
		if(objective_lookAt_position_list.Count > 0)
		{
			/*
			 * There is at least one position in the 
			 * collection. We will duplicate that one.
			 * We do the duplication later.
			 * */
			chosen_lookAt_position = objective_lookAt_position_list[objective_lookAt_position_list.Count - 1];
			chosen_lookAt_upVector = objective_lookAt_upVector_list[objective_lookAt_upVector_list.Count - 1];
			chosen_rotation_speed = objective_rotation_speed_list[objective_rotation_speed_list.Count - 1];
			
		}
		else
		{
			/*
			 * There are no lookAt on the list. We 
			 * will have to extrapolate the the current 
			 * lookAt position at put it reasonably far 
			 * away so that a small movement will not 
			 * cause the attached object to change its 
			 * lookAt direction
			 * */
			chosen_destination_position = attached_world_object.transform.position;
			chosen_forward_vector = attached_world_object.transform.forward;
			chosen_lookAt_position = new Vector3(chosen_destination_position.x + chosen_forward_vector.x * PSEUDO_INFINITE_DISTANCE, 
			                                     chosen_destination_position.y + chosen_forward_vector.y * PSEUDO_INFINITE_DISTANCE, 
			                                     chosen_destination_position.z + chosen_forward_vector.z * PSEUDO_INFINITE_DISTANCE);
			chosen_lookAt_upVector = attached_world_object.transform.up;
			chosen_rotation_speed = PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT;
			
		}
		
		/*
		 * Duplicate the correct position and add it into the 
		 * collection using deep copy
		 * */
		objective_lookAt_position_list.Add(new Vector3(chosen_lookAt_position.x, 
		                                               chosen_lookAt_position.y, 
		                                               chosen_lookAt_position.z));
		objective_lookAt_upVector_list.Add(new Vector3(chosen_lookAt_upVector.x, 
		                                               chosen_lookAt_upVector.y, 
		                                               chosen_lookAt_upVector.z));
		objective_rotation_speed_list.Add(chosen_rotation_speed);
		
		/*
		 * Add the valid given parameter into the collection
		 * */
		objective_destination_position_list.Add(new Vector3(given_destination_position.x, 
		                                                    given_destination_position.y, 
		                                                    given_destination_position.z));
		objective_movement_speed_list.Add(given_movement_speed);
		
		/*
		 * New objectives received. Mark the 
		 * task as incomplete
		 * */
		task_complete = false;
	}

	/*
	 * Adds a new lookAt task
	 * */
	public void add_lookAt_task (Vector3 given_lookAt_position, 
	                             Vector3 given_upVector, 
	                             float given_rotation_speed) 	{
		
		Vector3 chosen_destination_position;
		float chosen_movement_speed;

		Vector3 chosen_lookAt_position;
		Vector3 chosen_forward_vector;

		
		/*
		 * There is no given movement destination. We will 
		 * copy the latest position if possible, 
		 * or else we will use the attached object's 
		 * current position
		 * */
		if(objective_destination_position_list.Count > 0)
		{
			/*
			 * There is at least one position in the 
			 * collection. We will duplicate that one.
			 * We do the duplication later.
			 * */
			chosen_destination_position = objective_destination_position_list[objective_destination_position_list.Count - 1];
			chosen_movement_speed = objective_movement_speed_list[objective_movement_speed_list.Count - 1];
			
		}
		else
		{
			/*
			 * There are no positions on the list. We 
			 * will duplicate the attached object's 
			 * current position. We do the duplication 
			 * later.
			 * */
			chosen_destination_position = attached_world_object.transform.position;
			chosen_movement_speed = PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT;
		}
		
		/*
		 * Duplicate the correct position and add it into the 
		 * collection using deep copy
		 * */
		objective_destination_position_list.Add(new Vector3(chosen_destination_position.x, 
		                                                    chosen_destination_position.y, 
		                                                    chosen_destination_position.z));
		objective_movement_speed_list.Add(chosen_movement_speed);

		
		/*
		 * Add the valid given parameter into the collection
		 * */
		objective_lookAt_position_list.Add(new Vector3(given_lookAt_position.x, 
		                                               given_lookAt_position.y, 
		                                               given_lookAt_position.z));

		objective_lookAt_upVector_list.Add(new Vector3(given_upVector.x, 
		                                               given_upVector.y, 
		                                               given_upVector.z));
		objective_rotation_speed_list.Add(given_rotation_speed);
		
		/*
		 * New objectives received. Mark the 
		 * task as incomplete
		 * */
		task_complete = false;
	}

	/*
	 * Assigns a new desitnation positions and 
	 * points to look at
	 * */
	public void add_movement_and_lookAt_task (Vector3 given_destination_position, 
	                                          float given_movement_speed, 
	                                          Vector3 given_lookAt_position, 
	                                          Vector3 given_lookAt_upVector, 
	                                          float given_rotation_speed) 	{

		Vector3 chosen_destination_position;
		Vector3 chosen_lookAt_position;
		Vector3 chosen_forward_vector;

		/*
		 * Check if at least one of the given parameters is valid
		 * */
		if(given_destination_position == null 
		   && given_lookAt_position == null)
		{
			/*
			 * Both given parameters are invalid. 
			 * Do nothing
			 * */
			Debug.Log("assign_movement_and_lookAt_task: both given parameters are invalid!");
			return;
		}
		else
		{

			/*
			 * At least one of the given parameters is 
			 * valid. Find out which one it is and 
			 * process it accordingly.
			 * */
			if(given_destination_position == null)
			{
				/*
				 * Instead call the correct function
				 * */
				add_lookAt_task(given_lookAt_position, 
				                given_lookAt_upVector, 
				                given_rotation_speed);
			}
			else if(given_lookAt_position == null)
			{
				/*
				 * Instead call the correct function
				 * */
				add_movement_task(given_destination_position, 
				                  given_movement_speed);
			}
			else if(given_destination_position != null 
			        && given_lookAt_position != null)
			{
				/*
				 * Both parameters given are valid. We 
				 * will use them with deep copy
				 * */
				objective_destination_position_list.Add(new Vector3(given_destination_position.x, 
				                                                    given_destination_position.y, 
				                                                    given_destination_position.z));
				objective_movement_speed_list.Add(given_movement_speed);

				objective_lookAt_position_list.Add(new Vector3(given_lookAt_position.x, 
				                                               given_lookAt_position.y, 
				                                               given_lookAt_position.z));
				objective_lookAt_upVector_list.Add(new Vector3(given_lookAt_upVector.x, 
				                                               given_lookAt_upVector.y, 
				                                               given_lookAt_upVector.z));
				objective_rotation_speed_list.Add(given_rotation_speed);
			}
			
			/*
			 * New objectives received. Mark the 
			 * task as incomplete
			 * */
			task_complete = false;
		}
	}

	/*
	 * Process an update frame
	 * */
	public void process_update () 	{

		Vector3 current_object_position;
		Vector3 chosen_objective_destination;
		Vector3 chosen_objective_destination_delta_vector;
		float chosen_objective_destination_delta_distance;
		float chosen_movement_speed;

		Vector3 chosen_objective_lookAt_position;
		Vector3 chosen_objective_lookAt_upVector;
		Quaternion chosen_objective_lookAt_quaternion;
		float chosen_rotation_speed;

		float chosen_objective_lookAt_delta_angle;
		float chosen_slerp_coefficient;

		float distance_from_objective;
		float angle_from_objective;

		/*
		 * Check if the task has previously 
		 * been completed
		 * */
		if(task_complete == false)
		{
			/*
			 * The task is incomplete; there 
			 * is at least one iteam left on 
			 * the objective collections
			 * */

			/*
			 * Retrieve the objectives first
			 * */
			chosen_objective_destination = objective_destination_position_list[0];
			chosen_movement_speed = objective_movement_speed_list[0];

			chosen_objective_lookAt_position = objective_lookAt_position_list[0];
			chosen_objective_lookAt_upVector = objective_lookAt_upVector_list[0];
			chosen_rotation_speed = objective_rotation_speed_list[0];


			/*
			 * Move the attached object towards the 
			 * destination while looking at the 
			 * lookAt position
			 * */

			/*
			 * Find the delta vector
			 * */
			current_object_position = attached_world_object.transform.position;
			chosen_objective_destination_delta_vector = new Vector3(chosen_objective_destination.x - current_object_position.x, 
			                                                          chosen_objective_destination.y - current_object_position.y, 
			                                                          chosen_objective_destination.z - current_object_position.z).normalized;

			/*
			 * Clamp the distance that can be 
			 * traveled
			 * */
			chosen_objective_destination_delta_distance = Vector3.Distance(current_object_position, chosen_objective_destination);
			chosen_objective_destination_delta_distance = Mathf.Min(Time.deltaTime * chosen_movement_speed, chosen_objective_destination_delta_distance);

			/*
			 * Translate the attached world 
			 * object
			 * */
			attached_world_object.transform.Translate(chosen_objective_destination_delta_vector.x * chosen_objective_destination_delta_distance, 
			                                          chosen_objective_destination_delta_vector.y * chosen_objective_destination_delta_distance, 
			                                          chosen_objective_destination_delta_vector.z * chosen_objective_destination_delta_distance, 
			                                          Space.World);

			/*
			 * Find the final quaternion
			 * */
			chosen_objective_lookAt_quaternion = Quaternion.LookRotation((chosen_objective_lookAt_position - current_object_position).normalized, chosen_objective_lookAt_upVector);

			/*
			 * Find the angle between the two 
			 * quaternions to determine how much to 
			 * rotate
			 * */
			chosen_objective_lookAt_delta_angle = Mathf.Abs(Quaternion.Angle(attached_world_object.transform.rotation, chosen_objective_lookAt_quaternion));
			chosen_slerp_coefficient = chosen_rotation_speed / chosen_objective_lookAt_delta_angle;

			/*
			 * Slerp the rotation 
			 * */
			attached_world_object.transform.rotation = Quaternion.Slerp(attached_world_object.transform.rotation, chosen_objective_lookAt_quaternion, 
			                                                            Time.deltaTime * chosen_slerp_coefficient);

			/*
			 * All associated objects will copy the current 
			 * position and rotation
			 * */
			foreach(GameObject chosen_gameObject_inLoop in associated_world_object_list)
			{
				chosen_gameObject_inLoop.transform.position = attached_world_object.transform.position;
				chosen_gameObject_inLoop.transform.rotation = attached_world_object.transform.rotation;
			}

			/*
			 * Check if the task is complete
			 * */
			distance_from_objective = Vector3.Distance(attached_world_object.transform.position, chosen_objective_destination);
			angle_from_objective = Quaternion.Angle(Quaternion.LookRotation(attached_world_object.transform.forward), chosen_objective_lookAt_quaternion);

			if(Vector3.Distance(attached_world_object.transform.position, chosen_objective_destination) <= MOVEMENT_COMPLETION_DISTANCE_THRESHOLD 
			   && angle_from_objective <= LOOKAT_COMPLETION_ANGLE_THRESHOLD)
			{
				/*
				 * The object has moved within the threshold for 
				 * completion. Snap the object to the position and 
				 * mark the task as complete
				 * */
//				attached_world_object.transform.position = chosen_objective_destination;
//				attached_world_object.transform.LookAt(chosen_objective_lookAt_position);

				/*
				 * Remove the first element from both lists
				 * */
				objective_destination_position_list.RemoveAt(0);
				objective_lookAt_position_list.RemoveAt(0);

				/*
				 * Task is complete if both lists 
				 * are empty
				 * */
				if(objective_destination_position_list.Count == 0 
				   && objective_lookAt_position_list.Count == 0)
				{
					task_complete = true;
				}
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
