using UnityEngine;
using System.Collections;

public class EventManager_MainHall : MonoBehaviour {
	// Note: Copy the Main Camera from the Menu GUI prefabs. The camera nested in the character does not work properly with this code.
	
	public float RotationSpeed = 5;
	public float movementSpeed = 20;

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;
	private GameObject theCamera;
	private int num;
	public GameObject theCharacter;
	
	// Audio
	public AudioClip footsteps;
	
	// Disable these scripts while moving
	public Shooting shootScript;
	public Shield shieldScript;
	
	// Need to access these scripts to save info
	public InGameScoreScript scoreScript;
	public GunDisplay gunScript;
	public LifeCounter lifeScript;
	public TimerScript timeScript;
	
	// Spawning
	public GameObject bearPrefab;
	private int count;
	public GameObject lollipopPrefab;
	public GameObject EggPrefab;
	private bool reached = false;

	public Stage_Sequence_Reader attached_sequence_reader;
	public bool trial = false;

	/*
	 * Helpers
	 * */
	public World_Object_Movement_Helper main_character_movement_helper;

	void Start(){
		theCamera = Camera.main.gameObject;
		theCharacter = GameObject.FindWithTag("MainCharacter");
		GetComponent<AudioSource>().clip = footsteps;
		num = 0;
		theCharacter.transform.rotation = theCamera.transform.rotation;
		count = 1;

		attached_sequence_reader = new Stage_Sequence_Reader();

		// Start
		attached_sequence_reader.attached_sequence = new Stage_Sequence_1_1(attached_sequence_reader);
		// Moving while shooting
		//attached_sequence_reader.attached_sequence = new Stage_Sequence_1_13(attached_sequence_reader);
		// Minigame
		//attached_sequence_reader.attached_sequence = new Stage_Sequence_1_17_A(attached_sequence_reader);

		//attached_sequence_reader.attached_sequence = new Stage_Sequence_Skewer_1_1(attached_sequence_reader);
		//attached_sequence_reader.attached_sequence = new Stage_Sequence_1_17(attached_sequence_reader);

//		/*
//		 * Attach a movement helper to the character 
//		 * and the main camera
//		 * */
//		main_character_movement_helper = new World_Object_Movement_Helper(Camera.main.gameObject);
//		main_character_movement_helper.add_associated_world_object(GameObject.FindWithTag("MainCharacter"));
	}

	// Update is called once per frame
	void Update () {
		// character hitbox follows the camera around
		//theCharacter.transform.position = theCamera.transform.position;

//		/*
//		 * Update all the movement helpers
//		 * */
//		main_character_movement_helper.process_update();

		/*
		 * Update the sequence
		 * */
		attached_sequence_reader.process_update();

//		// Position 1, wave 1
//		if(count <= 5){
//			#if UNITY_EDITOR
//			Debug.Log("Wave 1");
//			#endif
//			spawnBear (new Vector3( -60f, 0f, 246f ), 1); // target1
//			spawnBear (new Vector3( -47f, 0f, 218f ), 2); // target2
//			spawnBear (new Vector3( 48f, 0f, 218f ), 1); // target3
//			spawnBear (new Vector3( -34f, 21f, 154f ), 0); // target4
//			spawnBear (new Vector3( 52f, 21f, 154f ), 0); // target5
//			//count++;
//		}
//		// Position 1, wave 2
//		else if(count > 5 && count <= 10 && !(GameObject.Find ("Target1") || GameObject.Find ("Target2") || GameObject.Find ("Target3") || GameObject.Find ("Target4") || GameObject.Find ("Target5"))){
//			#if UNITY_EDITOR
//			Debug.Log("Wave 2");
//			#endif
//			spawnBear (new Vector3( -60f, 0f, 246f ), 1); // target6
//			spawnBear (new Vector3( 48f, 0f, 218f ), 1); // target7
//			spawnBear (new Vector3( -16f, 0f, 173f ), 0); // target8
//			spawnBear (new Vector3( 34f, 21f, 154f ), 0); // target9
//			spawnBear (new Vector3( 52f, 21f, 154f ), 0); // target10
//		}
//		// Position 1, wave 3
//		else if(count > 10 && count <= 12 && !(GameObject.Find ("Target6") || GameObject.Find ("Target7") || GameObject.Find ("Target8") || GameObject.Find ("Target9") || GameObject.Find ("Target10"))){
//			#if UNITY_EDITOR
//			Debug.Log("Wave 3");
//			#endif
//			spawnBear (new Vector3( -47f, 0f, 218f ), 2); // target11
//			spawnBear (new Vector3( 48f, 0f, 218f ), 1); // target12
//		}
//		
//		// Position 2
//		else if(count > 12 && count <= 23 && !(GameObject.Find ("Target11") || GameObject.Find ("Target12"))){
//			if (num == 0)
//				num = TranslateTo( new Vector3(-65.4f, 8f, 266.1f), num);	
//			else if (num == 1)
//				num = TranslateTo( new Vector3(-64f, 8f, 227.7f), num);
//			else if (num == 2)
//				num = TranslateTo( new Vector3(-51.6f, 8f, 224f), num);
//			else if (num == 3)
//				num = LookAt( new Vector3 (-46f, 12f, 202.5f), num);
//			if(trial == false)
//			{
//				theCamera.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(-65.4f, 8f, 266.1f), 
//				                                                 						World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
//
//				theCamera.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(-64f, 8f, 227.7f), 
//				                                                 						World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
//
//				theCamera.GetComponent<World_Object_Movement_Helper>().add_movement_task(new Vector3(-51.6f, 8f, 224f), 
//				                                                 						World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT);
//
//				theCamera.GetComponent<World_Object_Movement_Helper>().add_lookAt_task(new Vector3 (-46f, 12f, 202.5f), 
//				                                               							Vector3.up, 
//				                                              	 						World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
//
//				theCamera.GetComponent<World_Object_Movement_Helper>().add_movement_and_lookAt_task(new Vector3(-51.6f, 8f, 224f), 
//				                                                            						World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_MOVEMENT_SPEED_DEFAULT, 
//				                                                           							new Vector3 (-46f, 12f, 202.5f), 
//				                                                            						Vector3.up, 
//				                                                            						World_Object_Movement_Helper.PLAYER_WORLD_OBJECT_ROTATION_SPEED_DEFAULT);
//				trial = true;
//			}
//				
//			if(count > 12 && count <= 16 && reached == true){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 4");
//				#endif
//				spawnBear (new Vector3( 13f, 0f, 173f ), 0); // target13
//				spawnBear (new Vector3( -34f, 21f, 154f ), 0); // target14
//				spawnBear (new Vector3( -73f, 21f, 154f ), 0); // target15
//				spawnBear (new Vector3( -18f, 0f, 156f ), 1); // target16
//				reached = false;
//			}
//			else if(count > 16 && count <= 20 && !(GameObject.Find ("Target13") || GameObject.Find ("Target14") || GameObject.Find ("Target15") || GameObject.Find ("Target16"))){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 5");
//				#endif
//				spawnBear (new Vector3( -34f, 21f, 154f ), 0);// target17
//				spawnBear (new Vector3( -55f, 21f, 154f ), 0); // target18
//				spawnBear (new Vector3( -73f, 21f, 154f ), 0);// target19
//				spawnBear (new Vector3( 34f, 21f, 154f ), 0); // target20
//			}
//			
//			else if(count > 20 && count <= 23 && !(GameObject.Find ("Target17") || GameObject.Find ("Target18") || GameObject.Find ("Target19") || GameObject.Find ("Target20"))){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 6");
//				#endif
//				spawnBear (new Vector3( 34f, 21f, 154f ), 0); // target21
//				spawnBear (new Vector3( -18f, 0f, 156f ), 1); // target22
//				spawnLollipop( new Vector3 ( -50f, 35f, 36f )); // target23
//			}
//		}
//		
//		// Position 3
//		else if(count > 23 && count <= 36 && !(GameObject.Find ("Target21") || GameObject.Find ("Target22") || GameObject.Find ("Target23"))){
//			if (num == 4)
//				num = TranslateTo( new Vector3(-17.9f, 8f, 176.2f), num);
//			else if (num == 5)
//				num = LookAt( new Vector3 (-10f, 11f, 161f), num);
//			//theCamera.transform.LookAt( new Vector3 (-10f, 152.6f, 0f));
//			
//			if(count > 23 && count <= 28 && reached == true){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 7");
//				#endif
//				spawnBear (new Vector3( -23f, 0f, 93f ), 2); // target24
//				spawnBear (new Vector3( 34f, 21f, 138f ), 0);// target25
//				spawnBear (new Vector3( 49f, 21f, 61f ), 0); // target26
//				spawnBear (new Vector3( 15f, 21f, 61f ), 0);// target27
//				spawnBear (new Vector3( -15f, 21f, 61f ), 0); // target28
//				reached = false;
//			}
//			else if(count > 28 && count <= 32 && !(GameObject.Find ("Target24") || GameObject.Find ("Target25") || GameObject.Find ("Target26") || GameObject.Find ("Target27") || GameObject.Find ("Target28"))){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 8");
//				#endif
//				spawnBear (new Vector3( -23f, 0f, 93f ), 2); // target29
//				spawnBear (new Vector3( 34f, 21f, 138f ), 0); // target30
//				spawnBear (new Vector3( 49f, 21f, 61f ), 0); // target31
//				spawnEgg( new Vector3 ( 0, 0, 70 )); // target32
//			}
//			
//			else if(count > 32 && count <= 36 && !(GameObject.Find ("Target29") || GameObject.Find ("Target30") || GameObject.Find ("Target31") || GameObject.Find ("Target32"))){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 9");
//				#endif
//				spawnBear (new Vector3( 34f, 21f, 138f ), 0); // target33
//				spawnBear (new Vector3( 15f, 21f, 61f ), 0); // target34
//				spawnBear (new Vector3( -15f, 21f, 61f ), 0); // target35
//				spawnEgg( new Vector3 ( 0, 0, 70 )); // target36
//			}
//		}
//		
//		// Position 4
//		else if(count > 36 && count <= 46 && !(GameObject.Find ("Target33") || GameObject.Find ("Target34") || GameObject.Find ("Target35") || GameObject.Find ("Target36"))){
//			if (num == 6)
//				num = TranslateTo( new Vector3(0f, 8f, 68f), num);
//			else if (num == 7)
//				num = LookAt( new Vector3 (33.8f, 25f, 140.5f), num);
//			
//			if(count > 36  && count <= 42 && reached == true){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 10");
//				#endif
//				spawnBear (new Vector3( 64f, 21f, 130f ), 0); // target37
//				//spawnBear (new Vector3( 64f, 21f, 80f ), 0); // target38
//				count++;
//				spawnBear (new Vector3( 23f, 0f, 169f ), 2); // target39
//				spawnBear (new Vector3( -23f, 0f, 169f ), 1); // target40
//				spawnBear (new Vector3( 70f, 0f, 107f ), 1); // target41
//				spawnLollipop( new Vector3 ( -55f, 100f, 280f )); // target42
//				reached = false;
//			}
//			else if(count > 42 && count <= 46 && !(GameObject.Find ("Target37") || GameObject.Find ("Target38") || GameObject.Find ("Target39") || GameObject.Find ("Target40") || GameObject.Find ("Target41") || GameObject.Find ("Target42"))){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 11");
//				#endif
//				spawnBear (new Vector3( 34f, 21f, 138f ), 0); // target43
//				spawnBear (new Vector3( 64f, 21f, 105f ), 0); // target44
//				spawnBear (new Vector3( 23f, 0f, 169f ), 2); // target45
//				spawnLollipop( new Vector3 ( -55f, 100f, 280f )); // target46
//			}
//		}
//		
//		// Position 5
//		else if(count > 46 && count <= 54 && !(GameObject.Find ("Target43") || GameObject.Find ("Target44") || GameObject.Find ("Target45") || GameObject.Find ("Target46"))){
//			if (num == 8)
//				num = TranslateTo( new Vector3(38.2f, 8f, 94.8f), num);	
//			else if (num == 9)
//				num = TranslateTo( new Vector3(38.2f, 31f, 134.7f), num);
//			else if (num == 10)
//				num = TranslateTo( new Vector3(33.1f, 33.8f, 160.6f), num);
//			else if (num == 11)
//				num = LookAt( new Vector3 (30f, 32f, 170f), num);
//			
//			if(count > 46 && count <= 50 && reached == true){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 12");
//				#endif
//				spawnBear (new Vector3( -60f, 0f, 234f ), 2); // target47
//				spawnBear (new Vector3( 38f, 0f, 260f ), 2); // target48
//				//spawnBear (new Vector3( 53f, 0f, 260f ), 1); // target49
//				count++;
//				spawnLollipop( new Vector3 ( 55f, 100f, 280f )); // target50
//				reached = false;
//			}
//			else if(count > 50 && count <= 54 && !(GameObject.Find ("Target47") || GameObject.Find ("Target48") || GameObject.Find ("Target49") || GameObject.Find ("Target50"))){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 13");
//				#endif
//				spawnBear (new Vector3( -60f, 0f, 234f ), 2); // target51
//				spawnBear (new Vector3( 38f, 0f, 260f ), 2); // target52
//				//spawnBear (new Vector3( 53f, 0f, 260f ), 1); // target53
//				count++;
//				spawnLollipop( new Vector3 ( -55f, 100f, 280f )); // target54
//			}
//		}
//		
//		// Position 6
//		else if(count > 54 && count <= 68 && !(GameObject.Find ("Target51") || GameObject.Find ("Target52") || GameObject.Find ("Target53") || GameObject.Find ("Target54"))){
//			if (num == 12)
//				num = TranslateTo( new Vector3(65.1f, 33.8f, 136.6f), num);	
//			else if (num == 13)
//				num = TranslateTo( new Vector3(65.1f, 33.8f, 55.9f), num);
//			else if (num == 14){
//				num = TranslateTo( new Vector3(49f, 35.8f, 64.9f), num);
//				reached = true;
//			}
//			else if (num == 15){
//				num = LookAt( new Vector3 (31.5f, 33f, 81f), num);
//				#if UNITY_EDITOR
//				Debug.Log ("count = " + count);
//				Debug.Log ("reached = " + reached);
//				#endif
//			}
//
//			if(count > 54 && count <= 61 && reached == true){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 14");
//				#endif
//				spawnBear (new Vector3( 34f, 21f, 154f ), 0); // target55
//				spawnBear (new Vector3( -23f, 0f, 169f ), 1); // target56
//				spawnBear (new Vector3( -66f, 0f, 107f ), 2); // target57
//				spawnBear (new Vector3( -64f, 21f, 69f ), 0); // target58
//				spawnBear (new Vector3( -64f, 21f, 138f ), 0); // target59
//				spawnBear (new Vector3( 17f, 0f, 112f ), 2); // target60
//				spawnLollipop( new Vector3 ( 55f, 100f, 280f )); // target61
//				reached = false;
//			}////////////////////////
//			else if(count > 61 && count <= 68 && !(GameObject.Find ("Target55") || GameObject.Find ("Target56") || GameObject.Find ("Target57") || GameObject.Find ("Target58") || GameObject.Find ("Target59") || GameObject.Find ("Target60") || GameObject.Find ("Target61"))){
//				#if UNITY_EDITOR
//				Debug.Log ("Wave 15");
//				#endif
//				spawnBear (new Vector3( -34f, 21f, 154f ), 0); // target62
//				spawnBear (new Vector3( 34f, 21f, 154f ), 0); // target63
//				spawnBear (new Vector3( -66f, 0f, 107f ), 2); // target64
//				spawnBear (new Vector3( -64f, 21f, 69f ), 0); // target65
//				spawnBear (new Vector3( -64f, 21f, 138f ), 0); // target66
//				spawnBear (new Vector3( 17f, 0f, 112f ), 2); // target67
//				spawnLollipop( new Vector3 ( 55f, 100f, 280f )); // target68
//			}
//		}
//		
//		// Move to next zone
//		else if(count > 68 && !(GameObject.Find ("Target62") || GameObject.Find ("Target63") || GameObject.Find ("Target64") || GameObject.Find ("Target65") || GameObject.Find ("Target66") || GameObject.Find ("Target67") || GameObject.Find ("Target68"))){
//			#if UNITY_EDITOR
//			Debug.Log("no 6 " + num);
//			#endif
//			if (num == 15){
//				#if UNITY_EDITOR
//				Debug.Log ("here");
//				#endif
//				num = TranslateTo( new Vector3(0f, 35.8f, 60f), num);	
//			}else if (num == 16)
//				num = LookAt( new Vector3(0f, 35.8f, 42f), num);
//			else if (num == 17)
//				num = TranslateTo( new Vector3(0f, 35.8f, 42f), num);
//			else if (num == 18)
//				num = TranslateTo( new Vector3(0f, 35.8f, 25.7f), num);
//			
//			// Load next level
//			else if(num == 19){
//				saveGame ();
//				Application.LoadLevel("DiningHall");
//			}
//		}
	}
	
	
	private int LookAt( Vector3 position , int num) {
		
		//find the vector pointing from our position to the target
		_direction = (position - transform.position).normalized;
		
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		theCharacter.transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		
		if (transform.rotation == _lookRotation){
			num += 1;
			reached = true;
		}
		
		return num;
	}
	
	private int TranslateTo( Vector3 position , int num) {
		
		// Calculate the distance between the follower and the leader.
		float range1 = Vector3.Distance(theCamera.transform.position, position );
		//Debug.Log ("Range = " + range1);
		
		if (range1 > 1.0) {
			// prevent shooting and using the shield while moving
			shootScript.enabled = false;
			shieldScript.enabled = false;
			
			//find the vector pointing from our position to the target
			_direction = (position - transform.position).normalized;
			
			//create the rotation we need to be in to look at the target
			_lookRotation = Quaternion.LookRotation (_direction);
			
			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
			// calculate direction and move towards the target
			Vector3 dir = position - theCamera.transform.position;
			dir = dir.normalized;
			
			if(!GetComponent<AudioSource>().isPlaying && Time.timeScale == 1){
				GetComponent<AudioSource>().Play ();
			}
			// Stop playing the footsteps if paused
			else if(GetComponent<AudioSource>().isPlaying && Time.timeScale == 0){
				GetComponent<AudioSource>().Stop();
			}
			
			//theCharacter.transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
			theCamera.transform.Translate (dir * movementSpeed * Time.deltaTime, Space.World);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
		} else if (range1 <= 1.0) {
			// Reached position, enable shielding and shooting
			shootScript.enabled = true;
			shieldScript.enabled = true;
		
			GetComponent<AudioSource>().Stop ();
			num += 1;
			#if UNITY_EDITOR
			Debug.Log (num);
			#endif
			
		}
		return num;
	}
	
	private void spawnBear(Vector3 position, int cover){
		GameObject bear = Instantiate(bearPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		bear.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = GetComponent<Camera>().transform.position - bear.transform.position; // make the instantiated bear face the camera
		bear.transform.rotation = Quaternion.LookRotation(direction);
		
		Enemy e = bear.GetComponent<Enemy>();
		e.coverType = cover;
		
		count++;
	}
	
	private void spawnLollipop(Vector3 position){
		GameObject lollipop = Instantiate(lollipopPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		lollipop.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = GetComponent<Camera>().transform.position - lollipop.transform.position; // make the instantiated bear face the camera
		lollipop.transform.rotation = Quaternion.LookRotation(direction);
		count++;
	}
	
	private void spawnEgg(Vector3 position){
		GameObject egg = Instantiate(EggPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		egg.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = GetComponent<Camera>().transform.position - egg.transform.position; // make the instantiated bear face the camera
		egg.transform.rotation = Quaternion.LookRotation(direction);
		count++;
	}
	
	private void saveGame(){
		PlayerPrefs.SetInt ("currentScore", (int)scoreScript.currentScore);
		PlayerPrefs.SetInt ("HMGTotalAmmo", (int)gunScript.ammoCountTotalHMG);
		PlayerPrefs.SetInt ("ShotgunTotalAmmo", (int)gunScript.ammoCountTotalShotgun);
		PlayerPrefs.SetInt ("HMGAmmo", (int)gunScript.ammoCountHMG);
		PlayerPrefs.SetInt ("ShotgunAmmo", (int)gunScript.ammoCountShotgun);
		PlayerPrefs.SetInt ("RocketLauncherAmmo", (int)gunScript.ammoCountRocketLauncher);
		PlayerPrefs.SetInt ("RocketTotalAmmo", (int)gunScript.ammoCountTotalRocketLauncher);
		PlayerPrefs.SetInt("playedTakeDamage", (int)lifeScript.playedTakeDamage);
		PlayerPrefs.SetInt("timeLeft", (int)timeScript.seconds);
		PlayerPrefs.SetInt("playerLoadedHealth", (int)lifeScript.playerHealth);
		#if UNITY_EDITOR
		Debug.Log ("Game saved");
		#endif
	}
}
