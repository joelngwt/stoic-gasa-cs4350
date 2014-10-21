using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage_Sequence_Helper {
	
	////////////////////////////////////////////////////
	//
	//		Constants
	//
	////////////////////////////////////////////////////

	public static readonly string PREFAB_RESOURCE_PATH_BEAR = "Prefabs/Bear/Bear_donePrefab_DiningHall";
	public static readonly string PREFAB_RESOURCE_PATH_LOLLIPOP = "Prefabs/Lollipop/LolliPrefab";
	public static readonly string PREFAB_RESOURCE_PATH_EGG = "Prefabs/Egg/ChocEggPrefab";
	public static readonly string PREFAB_RESOURCE_PATH_POSSESSION_CUBE = "Prefabs/Possession_Cube/Possession_Cube";
	
	////////////////////////////////////////////////////
	//
	//		Script variables
	//
	////////////////////////////////////////////////////

	public Stage_Sequence_Reader attached_reader;

	public bool sequence_initialized = false;

	public int sequence_stage_index = 0;
	public int sequence_wave_index = 0;
	public int sequence_spawn_index = 1;
	public int sequence_phase_index = 0;

	public GameObject prefab_bear;
	public GameObject prefab_lollipop;
	public GameObject prefab_egg;

	public List<GameObject> spawn_list;
	
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
	public Stage_Sequence_Helper () 	{

		spawn_list = new List<GameObject>();
		Debug.Log ("next sequence");
	}

	/*
	 * Process an update
	 * */
	public virtual void process_update () 	{


	}
	
	////////////////////////////////////////////////////
	//
	//		Functions available via inheritance
	//
	////////////////////////////////////////////////////
	
	protected GameObject spawnBear (Vector3 position, int cover){
		GameObject prefab_bear = Resources.Load(PREFAB_RESOURCE_PATH_BEAR) as GameObject;

		GameObject bear = GameObject.Instantiate(prefab_bear, new Vector3(position.x, position.y, position.z), Quaternion.identity) as GameObject; // add public GameObject bearPrefab at the top
		bear.name = ("Bear_" + sequence_stage_index.ToString() + "_" + sequence_wave_index.ToString() + "_" + sequence_spawn_index.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = Camera.main.transform.position - bear.transform.position; // make the instantiated bear face the camera
		bear.transform.rotation = Quaternion.LookRotation(direction);


		bear.transform.FindChild ("Cube").GetComponent<SkinnedMeshRenderer>().materials[0].color = new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f),1);

		Enemy e = bear.GetComponent<Enemy>();
		e.coverType = cover;
		
		sequence_spawn_index++;

		return bear;
	}
	
	protected GameObject spawnLollipop (Vector3 position){
		GameObject prefab_lollipop = Resources.Load(PREFAB_RESOURCE_PATH_LOLLIPOP) as GameObject;

		GameObject lollipop = GameObject.Instantiate(prefab_lollipop, new Vector3(position.x, position.y, position.z), Quaternion.identity) as GameObject; // add public GameObject bearPrefab at the top
		lollipop.name = ("Lollipop_" + sequence_stage_index.ToString() + "_" + sequence_wave_index.ToString() + "_" + sequence_spawn_index.ToString());
		Vector3 direction = Camera.main.transform.position - lollipop.transform.position; // make the instantiated bear face the camera
		lollipop.transform.rotation = Quaternion.LookRotation(direction);
		
		sequence_spawn_index++;
		
		return lollipop;
	}
	
	protected GameObject spawnEgg (Vector3 position){
		GameObject prefab_egg = Resources.Load(PREFAB_RESOURCE_PATH_EGG) as GameObject;

		GameObject egg = GameObject.Instantiate(prefab_egg, new Vector3(position.x, position.y, position.z), Quaternion.identity) as GameObject; // add public GameObject bearPrefab at the top
		egg.name = ("Egg_" + sequence_stage_index.ToString() + "_" + sequence_wave_index.ToString() + "_" + sequence_spawn_index.ToString());
		Vector3 direction = Camera.main.transform.position - egg.transform.position; // make the instantiated bear face the camera
		egg.transform.rotation = Quaternion.LookRotation(direction);
		
		sequence_spawn_index++;
		
		return egg;
	}

	protected GameObject spawn_possession_cube (Vector3 given_position) 	{
		GameObject prefab_possession_cube = Resources.Load(PREFAB_RESOURCE_PATH_POSSESSION_CUBE) as GameObject;
		if(prefab_possession_cube == null)
		{
			Debug.Log("prefab is null");
		}
		GameObject possession_cube = GameObject.Instantiate(prefab_possession_cube, new Vector3(given_position.x, given_position.y, given_position.z), Quaternion.identity) as GameObject;
		possession_cube.name = ("Possession_Cube_" + sequence_stage_index.ToString() + "_" + sequence_wave_index.ToString() + "_" + sequence_spawn_index.ToString());
		Vector3 direction = Camera.main.transform.position - possession_cube.transform.position; // make the instantiated bear face the camera
		possession_cube.transform.rotation = Quaternion.LookRotation(direction);
		
		sequence_spawn_index++;
		
		return possession_cube;
	}

	protected bool check_spawn_list_empty () 	{
		
		/*
		 * Keep removing null references because when 
		 * an object is destroyed, it is replaced by 
		 * a null reference in any collection rather 
		 * then being removed
		 * */
		spawn_list.RemoveAll(spawn => spawn == null);
		
		if(spawn_list.Count < 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	////////////////////////////////////////////////////
	//
	//		Private functions for this script
	//
	////////////////////////////////////////////////////
}
