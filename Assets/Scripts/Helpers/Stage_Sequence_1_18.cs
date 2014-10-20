using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_18 : Stage_Sequence_Helper {
	
	////////////////////////////////////////////////////
	//
	//		Constants
	//
	////////////////////////////////////////////////////

	public readonly int OPERATOR_INDEX_ADD = 1;
	public readonly int OPERATOR_INDEX_SUBTRACT = 2;

	////////////////////////////////////////////////////
	//
	//		Script variables
	//
	////////////////////////////////////////////////////

	public GameObject whiteboard;
	public int equation_number_1; 
	public int equation_number_2;
	public int equation_operator;
	public int equation_answer;
	public int equation_solution;

	public string equation_operator_string;
	public string equation_question_text;
	public string equation_answer_text;

	public GameObject equation_question_text_mesh_gameObject;
	public TextMesh equation_question_text_mesh;
	
	public GameObject equation_answer_text_mesh_gameObject;
	public TextMesh equation_answer_text_mesh;

	public GameObject equation_plus_gameObject;
	public GameObject equation_subtract_gameObject;
	
	public Button_Trigger equation_plus_buttonTrigger;
	public Button_Trigger equation_subtract_buttonTrigger;

	public Material font_material;
	
	GameObject possession_cube_1;
	Possession_Cube possession_cube_script_1;

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
	public Stage_Sequence_1_18 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 18;
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
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3( 0.0F, 140.0F, 11.0F), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_POSSESSION, 
			                                                                                      new Vector3( 0.0F, 137.0F, -120.0F), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			sequence_phase_index = 1;
		}
		else if(sequence_phase_index == 1)
		{
			if(Vector3.Distance(Camera.main.transform.position, new Vector3(0.0F, 140.0F, 11.0F)) <= 1.00F)
			{
				sequence_phase_index = 2;
			}
		}
		else if(sequence_phase_index == 2)
		{

			whiteboard = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
			whiteboard.name = "Whiteboard";
			whiteboard.transform.position = new Vector3(0F, 160F, -100F);
			whiteboard.transform.localScale = new Vector3(100F, 50F, 1F);

			do
			{
				equation_number_1 = Random.Range(1, 10);
				equation_number_2 = Random.Range(1, 10);
				equation_operator = Random.Range(1, 3);
				
				equation_answer = 0;
				
				if(equation_operator == OPERATOR_INDEX_ADD)
				{
					equation_solution = equation_number_1 + equation_number_2;
					equation_operator_string = "+";
				}
				else
				{
					equation_solution = equation_number_1 - equation_number_2;
					equation_operator_string = "-";
				}

			} while(equation_solution == equation_answer);

			equation_question_text = "" + equation_number_1.ToString() + " " + equation_operator_string + " " + equation_number_2.ToString() + " = ?";

			equation_question_text_mesh_gameObject = GameObject.Find("Equation_Text");
			equation_question_text_mesh_gameObject.transform.position = new Vector3(39F, 183F, -90F);
			equation_question_text_mesh_gameObject.transform.rotation = Quaternion.Euler( new Vector3(0F, -180F, 0F) );
			equation_question_text_mesh_gameObject.name = "Equation question";	

			equation_question_text_mesh = equation_question_text_mesh_gameObject.GetComponent<TextMesh>();
			equation_question_text_mesh.text = equation_question_text;
			equation_question_text_mesh.characterSize = 1;
			equation_question_text_mesh.fontSize = 200;
			
			equation_answer_text_mesh_gameObject = GameObject.Find("Equation_Answer");
			equation_answer_text_mesh_gameObject.transform.position = new Vector3(10F, 160F, -90F);
			equation_answer_text_mesh_gameObject.transform.rotation = Quaternion.Euler( new Vector3(0F, -180F, 0F) );
			equation_answer_text_mesh_gameObject.name = "Equation answer";	
			
			equation_answer_text_mesh = equation_answer_text_mesh_gameObject.GetComponent<TextMesh>();
			equation_answer_text_mesh.text = equation_answer.ToString();
			equation_answer_text_mesh.characterSize = 1;
			equation_answer_text_mesh.fontSize = 200;

			equation_plus_gameObject = GameObject.Find("Equation_Plus");
			equation_plus_gameObject.transform.position = new Vector3(-40F, 150F, -90F);
			equation_plus_buttonTrigger = equation_plus_gameObject.GetComponent<Button_Trigger>();

			equation_subtract_gameObject = GameObject.Find("Equation_Subtract");
			equation_subtract_gameObject.transform.position = new Vector3(40F, 150F, -90F);
			equation_subtract_buttonTrigger = equation_subtract_gameObject.GetComponent<Button_Trigger>();

			sequence_phase_index = 3;
		}
		else if(sequence_phase_index == 3)
		{
			if(equation_subtract_buttonTrigger.activated)
			{
				equation_answer -= 1;

				equation_subtract_buttonTrigger.activated = false;
			}
			if(equation_plus_buttonTrigger.activated)
			{
				equation_answer += 1;

				equation_plus_buttonTrigger.activated = false;
			}
			
			equation_answer_text_mesh.text = equation_answer.ToString();

			if(equation_answer == equation_solution)
			{
				GameObject.Destroy(whiteboard);
				GameObject.Destroy(equation_question_text_mesh_gameObject);
				GameObject.Destroy(equation_answer_text_mesh_gameObject);
				GameObject.Destroy(equation_plus_gameObject);
				GameObject.Destroy(equation_subtract_gameObject);

				sequence_phase_index = 4;
			}
		}
		else if(sequence_phase_index == 4)
		{
			
			possession_cube_1 = spawn_possession_cube(new Vector3( 1.0F, 135.0F, -90.0F));
			possession_cube_script_1 = possession_cube_1.transform.FindChild("3D_Model").gameObject.AddComponent("Possession_Cube") as Possession_Cube;
			possession_cube_script_1.lookAt_position = new Vector3( 1.0F, 135.0F, -160.0F);
			possession_cube_script_1.up_vector = Vector3.up;


			sequence_phase_index = 5;
		}
		else if(sequence_phase_index == 5)
		{
			if(Vector3.Distance(Camera.main.transform.position, new Vector3(1.0F, 135.0F, -90.0F)) <= 1.00F)
			{
				sequence_phase_index = 6;
			}
		}
		else if(sequence_phase_index == 6)
		{
			
			Camera.main.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3( 0.0F, 135.0F, -145.0F), 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT, 
			                                                                                      new Vector3( 0.0F, 135.0F, -165.0F), 
			                                                                                      Vector3.up, 
			                                                                                      World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
			
			sequence_phase_index = 7;
		}
		else if(sequence_phase_index == 7)
		{
			if(Vector3.Distance(Camera.main.transform.position, new Vector3(0.0F, 135.0F, -145.0F)) <= 1.00F)
			{
				sequence_phase_index = 8;
			}
		}
		else if(sequence_phase_index == 8)
		{
			PlayerPrefs.SetInt ("currentScore", (int)Camera.main.GetComponent<EventManager_MainHall>().scoreScript.currentScore);
			PlayerPrefs.SetInt ("HMGTotalAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountTotalHMG);
			PlayerPrefs.SetInt ("ShotgunTotalAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountTotalShotgun);
			PlayerPrefs.SetInt ("HMGAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountHMG);
			PlayerPrefs.SetInt ("ShotgunAmmo", (int)Camera.main.GetComponent<EventManager_MainHall>().gunScript.ammoCountShotgun);
			PlayerPrefs.SetInt("playedTakeDamage", (int)Camera.main.GetComponent<EventManager_MainHall>().lifeScript.playedTakeDamage);
			PlayerPrefs.SetInt("timeLeft", (int)Camera.main.GetComponent<EventManager_MainHall>().timeScript.seconds);
			PlayerPrefs.SetInt("playerLoadedHealth", (int)Camera.main.GetComponent<EventManager_MainHall>().lifeScript.playerHealth);

			Application.LoadLevel("DiningHall");
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