using UnityEngine;
using System.Collections;

public class EventManager_DiningHall : MonoBehaviour {
	// Note: Copy the Main Camera from the Menu GUI prefabs. The camera nested in the character does not work properly with this code.
	
	public float RotationSpeed = 15;
	public float movementSpeed = 25;

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

	//public Stage_Sequence_Reader attached_sequence_reader;
	//public bool trial = false;

	/*
	 * Helpers
	 * */
	//public World_Object_Movement_Helper main_character_movement_helper;
	
	void Start(){
		theCamera = Camera.main.gameObject;
		theCharacter = GameObject.FindWithTag("MainCharacter");
		GetComponent<AudioSource>().clip = footsteps;
		num = 0;
		theCharacter.transform.rotation = theCamera.transform.rotation;
		count = 1;

		//attached_sequence_reader = new Stage_Sequence_Reader();
		//attached_sequence_reader.attached_sequence = new Stage_Sequence_2_1(attached_sequence_reader);
	}

	// Update is called once per frame
	void Update () {

		//attached_sequence_reader.process_update();
		
		// character hitbox follows the camera around
		theCharacter.transform.position = theCamera.transform.position;

		// Position 1
		if(count <= 9){
			Debug.Log("Wave 1");
			spawnBear (new Vector3( -8f, 0f, -18f ), 1); // target1
			spawnBear (new Vector3( 26f, 0f, -5f  ), 0); // target2
			count++;
			count++;
			spawnBear (new Vector3( 22f, 0f, 34.5f ),  0); // target5
			spawnBear (new Vector3( -20f, 0f, -30f ), 2); // target6
			spawnBear (new Vector3(  -6.5f, 0f, 53f ),  1); // target7
			spawnBear (new Vector3( 5f, 0f, -5f  ),  0); // target8
			spawnLollipop (new Vector3 ( 10f, 10f, 65f )); // target9
			//count++;
		}
		// Position 1
		else if(count > 9 && count <= 15 && !(GameObject.Find ("Target1") || GameObject.Find ("Target2") || GameObject.Find ("Target3") || GameObject.Find ("Target4") || GameObject.Find ("Target5") || GameObject.Find ("Target6") || GameObject.Find ("Target7") || GameObject.Find ("Target8") || GameObject.Find ("Target9"))){
			Debug.Log("Wave 2");
			spawnBear (new Vector3(  -20f, 0f, 5f),  2); // target10
			spawnBear (new Vector3( -20f, 0f, -30f  ), 2); // target11
			count++;
			count++;
			spawnBear (new Vector3( -6.5f, 0f, 53f ),  1); // target14
			spawnBear (new Vector3(  22f, 0f, 34.5f  ),  0); // target15
			spawnLollipop (new Vector3 ( 40f, 10f, 65f  )); // target16
			//count++;
		}
		// Position 2
		else if(count > 16 && count <= 29 && !(GameObject.Find ("Target10") || GameObject.Find ("Target11") || GameObject.Find ("Target12") || GameObject.Find ("Target13") || GameObject.Find ("Target14") || GameObject.Find ("Target15") || GameObject.Find ("Target16"))){
			if (num == 0){
				num = TranslateTo( new Vector3(51.2f, 3.5f, -43.3f), num);
			}
			else if (num == 1){
				num = LookAt( new Vector3(51.2f, 3.5f, -30f), num);
			}
			if(count > 16 && count <= 20 && reached == true){
				Debug.Log ("Wave 3");
				spawnBear (new Vector3( 46.5f, 0f, -17.5f ), 1); // target17
				spawnBear (new Vector3( 46.5f, 0f, 53f ),  1); // target18
				spawnBear (new Vector3( 53.6f, 0f, 32f ),  2); // target19
				spawnBear (new Vector3( 53.6f, 0f, -27f),  2); // target20
				reached = false;
			}
			else if(count > 20 && count <= 24 && !(GameObject.Find ("Target17") || GameObject.Find ("Target18") || GameObject.Find ("Target19") || GameObject.Find ("Target20"))){
				Debug.Log ("Wave 4");
				spawnBear (new Vector3( 46.5f, 0f, 17.5f ),  1); // target21
				spawnBear (new Vector3(53.6f, 0f, 32f ),  2); // target22
				spawnBear (new Vector3( 53.6f, 0f, -27f),  2); // target23
				spawnBear (new Vector3( 46.5f, 0f, 53f), 1); // target24
				spawnEgg(new Vector3 ( 50f, 0f, 62f )); // target25
			}
			
			else if(count > 25 && count <= 29 && !(GameObject.Find ("Target21") || GameObject.Find ("Target22") || GameObject.Find ("Target23") || GameObject.Find ("Target24"))){
				Debug.Log ("Wave 5");
				spawnBear (new Vector3( 53.6f, 0f, -27f  ),  2); // target26
				spawnBear (new Vector3(46.5f, 0f, -17.5f  ), 1); // target27
				spawnBear (new Vector3( 46.5f, 0f, 17.5f),  1); // target28
				spawnBear (new Vector3( 53.6f, 0f, 32f ), 2); // target29
			}
		}
		// Position 3
		else if(count > 29 && count <= 46 && !(GameObject.Find ("Target26") || GameObject.Find ("Target27") || GameObject.Find ("Target28") || GameObject.Find ("Target29"))){
			if (num == 2)
				num = TranslateTo( new Vector3(51.2f, 3.5f, -19f), num);
			else if (num == 3)
				num = TranslateTo( new Vector3(42.7f, 3.5f, -9f), num);
			else if (num == 4)
				num = LookAt( new Vector3 (34f, 3.5f, -6.5f), num);
				
			if(count > 29 && count <= 35 && reached == true){
				Debug.Log ("Wave 6");
				spawnBear (new Vector3( -1.5f, 0f, 2f),  0); // target30
				spawnBear (new Vector3(-6.5f, 0f, 53f ),  1); // target31
				spawnBear (new Vector3(-19f, 0f, -21f ),  1); // target32
				spawnBear (new Vector3( -7f, 0f, 17.5f), 1); // target33
				spawnBear (new Vector3( -20f, 0f, 41f),  2); // target34
				spawnBear (new Vector3( 10f, 0f, 34.5f ),  0); // target35
				reached = false;
			}
			else if(count > 35 && count <= 40 && !(GameObject.Find ("Target30") || GameObject.Find ("Target31") || GameObject.Find ("Target32") || GameObject.Find ("Target33") || GameObject.Find ("Target34") || GameObject.Find ("Target35"))){
				Debug.Log ("Wave 7");
				spawnBear (new Vector3( -20f, 0f, 41f),  2); // target36
				spawnLollipop (new Vector3 ( -26f, 10f, -25f  )); // target37
				spawnBear (new Vector3( -8f, 0f, -34f),  2); // target38
				spawnBear (new Vector3( -20f, 0f, 5f),  2); // target38
				spawnLollipop (new Vector3 ( 10f, 10f, 65f )); // target39
				spawnBear (new Vector3( -19f, 0f, -21f),  1); // target40
			}
			
			else if(count > 40 && count <= 46 && !(GameObject.Find ("Target36") || GameObject.Find ("Target37") || GameObject.Find ("Target38") || GameObject.Find ("Target39") || GameObject.Find ("Target40"))){
				Debug.Log ("Wave 8");
				spawnBear (new Vector3( 10f, 0f, 34.5f  ),  0); // target41
				spawnLollipop (new Vector3 ( -26f, 10f, -25f  )); // target42
				spawnBear (new Vector3(-20f, 0f, 5f  ),  2); // target43
				spawnBear (new Vector3( -20f, 0f, 41f  ),  2); // target44
				spawnBear (new Vector3(  -19f, 0f, -21f  ),  1); // target45
				spawnBear (new Vector3( -7f, 0f, 17.5f  ),  1); // target46
			}
		}
		// Position 4
		else if(count > 40 && count <= 61 && !(GameObject.Find ("Target41") || GameObject.Find ("Target42") || GameObject.Find ("Target43") || GameObject.Find ("Target44") || GameObject.Find ("Target45") || GameObject.Find ("Target46"))){
			if (num == 5)
				num = TranslateTo( new Vector3(34.4f, 3.5f, 1.6f), num);
			else if (num == 6)
				num = LookAt( new Vector3 (31f, 3.5f, 7f), num);
			
			if(count > 40 && count <= 53 && reached == true){
				Debug.Log ("Wave 9");
				spawnBear (new Vector3( 10f, 0f, 34.5f),  0); // target47
				spawnBear (new Vector3(24f, 0f, 55f ),  0); // target48
				spawnBear (new Vector3(-6.5f, 0f, 53f ),  1); // target49
				spawnBear (new Vector3( -7f, 0f, 17.5f), 1); // target50
				spawnBear (new Vector3(  7f, 0f, 55f),  0); // target51
				spawnBear (new Vector3( -20f, 0f, 41f ),  2); // target52
				spawnBear (new Vector3(  36f, 0f, 17.5f ),  2); // target53
				reached = false;
			}
			else if(count > 53 && count <= 61 && !(GameObject.Find ("Target47") || GameObject.Find ("Target48") || GameObject.Find ("Target49") || GameObject.Find ("Target50") || GameObject.Find ("Target51") || GameObject.Find ("Target52") || GameObject.Find ("Target53"))){
				Debug.Log ("Wave 10");
				spawnBear (new Vector3( 22f, 0f, 34.5f),  0); // target54
				spawnLollipop (new Vector3 (  -26f, 10f, 30f )); // target55
				spawnBear (new Vector3( -6.5f, 0f, 53f),  1); // target56
				spawnBear (new Vector3( 36f, 0f, 17.5f),  2); // target57
				spawnBear (new Vector3( 7f, 0f, 55f ),  0); // target58
				spawnBear (new Vector3(  -20f, 0f, 41f),  2); // target59
				spawnLollipop (new Vector3 ( 40f, 10f, 65f  )); // target60
				spawnBear (new Vector3( -7f, 0f, 17.5f ),  1); // target61
			}
		}
		// Position 5
		else if(count > 61 && count <= 73 && !(GameObject.Find ("Target54") || GameObject.Find ("Target55") || GameObject.Find ("Target56") || GameObject.Find ("Target57") || GameObject.Find ("Target58") || GameObject.Find ("Target59") || GameObject.Find ("Target60") || GameObject.Find ("Target61"))){
			if (num == 7)
				num = TranslateTo( new Vector3(34.4f, 3.5f, 23f), num);	
			else if (num == 8)
				num = LookAt( new Vector3 (33f, 3.5f, 25f), num);
			
			if(count > 61 && count <= 68 && reached == true){
				Debug.Log ("Wave 11");
				//spawnBear (new Vector3( 36f, 0f, 17.5f),  2); // target62
				//spawnBear (new Vector3(-6.5f, 0f, 53f  ),  1); // target63
				count++;
				count++;
				spawnBear (new Vector3(10f, 0f, 34.5f ),  0); // target64
				spawnBear (new Vector3( 24f, 0f, 55f), 0); // target65
				spawnBear (new Vector3(  -20f, 0f, 41f),  2); // target66
				spawnBear (new Vector3( 22f, 0f, 34.5f ),  0); // target67
				spawnBear (new Vector3(   7f, 0f, 55f ),  0); // target68
				reached = false;
			}
			else if(count > 68 && count <= 73 && !(GameObject.Find ("Target62") || GameObject.Find ("Target63") || GameObject.Find ("Target64") || GameObject.Find ("Target65") || GameObject.Find ("Target66") || GameObject.Find ("Target67") || GameObject.Find ("Target68"))){
				Debug.Log ("Wave 12");
				spawnBear (new Vector3( -20f, 0f, 41f),  2); // target69
				spawnLollipop (new Vector3 (  -26f, 10f, 30f)); // target70
				spawnLollipop (new Vector3 ( 40f, 10f, 65f  )); // target71
				spawnBear (new Vector3(  7f, 0f, 55f  ),  0); // target72
				spawnBear (new Vector3(  10f, 0f, 34.5f),  0); // target73
			}
		}
		
		// Position 6
		else if(count > 73 && count <= 98 && !(GameObject.Find ("Target69") || GameObject.Find ("Target70") || GameObject.Find ("Target71") || GameObject.Find ("Target72") || GameObject.Find ("Target73"))){
			if (num == 9)
				num = TranslateTo( new Vector3(38f, 3.5f, 32.4f), num);	
			else if (num == 10)
				num = TranslateTo( new Vector3(33f, 3.5f, 35.3f), num);
			else if (num == 11)
				num = TranslateTo( new Vector3(26f, 3.5f, 36.4f), num);
			else if (num == 12)
				num = LookAt( new Vector3 (22f, 3.5f, 26.4f), num);
			
			if(count > 73 && count <= 81 && reached == true){
				Debug.Log ("Wave 13");
				spawnBear (new Vector3(  25f, 0f, -35f),  0); // target74
				spawnBear (new Vector3(37f, 0f, 2f  ),  1); // target75
				spawnBear (new Vector3(-5.5f, 0f, -34f  ),  2); // target76
				spawnLollipop (new Vector3 ( 55f, 10f, -24f)); // target77
				spawnBear (new Vector3( -19f, 0f, -21f ), 1); // target78
				spawnBear (new Vector3( 5f, 0f, -5f),  0); // target79
				spawnBear (new Vector3(  37f, 0f, -32f ),  1); // target80
				spawnBear (new Vector3(-20f, 0f, 14.5f ), 1); // target81
				reached = false;
			}
			else if(count > 81 && count <= 88 && !(GameObject.Find ("Target74") || GameObject.Find ("Target75") || GameObject.Find ("Target76") || GameObject.Find ("Target77") || GameObject.Find ("Target78") || GameObject.Find ("Target79") || GameObject.Find ("Target80") || GameObject.Find ("Target81"))){
				Debug.Log ("Wave 14");
				spawnBear (new Vector3(-1.5f, 0f, 2f ),  0); // target82
				spawnBear (new Vector3(  -5.5f, 0f, -34f  ),  2); // target83
				spawnLollipop (new Vector3 (   55f, 10f, -24f)); // target84
				spawnBear (new Vector3(   -6f, 0f, 3f  ),  2); // target85
				spawnBear (new Vector3(  26f, 0f, -5f  ),  0); // target86
				spawnLollipop (new Vector3 (  55f, 10f, -24f  )); // target87
				spawnBear (new Vector3(  25f, 0f, -35f  ),  0); // target88
			}
			else if(count > 88 && count <= 98 && !(GameObject.Find ("Target82") || GameObject.Find ("Target83") || GameObject.Find ("Target84") || GameObject.Find ("Target85") || GameObject.Find ("Target86") || GameObject.Find ("Target87") || GameObject.Find ("Target88"))){
				Debug.Log ("Wave 15");
				spawnBear (new Vector3(5f, 0f, -5f),  0); // target89
				spawnBear (new Vector3( 26f, 0f, -5f ),  0); // target90
				spawnBear (new Vector3(   37f, 0f, 2f),  1); // target91
				spawnBear (new Vector3( -20f, 0f, 14.5f),  1); // target92
				spawnBear (new Vector3(  25f, 0f, -35f ),  0); // target93
				spawnBear (new Vector3(   -19f, 0f, -21f),  1); // target94
				
				spawnBear (new Vector3( -6f, 0f, 3f),  2); // target95
				spawnBear (new Vector3(  37f, 0f, -32f  ), 1); // target96
				spawnBear (new Vector3(  -5.5f, 0f, -34f),  2); // target97
				spawnBear (new Vector3( -1.5f, 0f, 2f),  0); // target98
			}
		}
		// Move to next zone
		else if(count > 98 && !(GameObject.Find ("Target89") || GameObject.Find ("Target90") || GameObject.Find ("Target91") || GameObject.Find ("Target92") || GameObject.Find ("Target93") || GameObject.Find ("Target94") || GameObject.Find ("Target95") || GameObject.Find ("Target96") || GameObject.Find ("Target97") || GameObject.Find ("Target98"))){
			if (num == 13)
				num = TranslateTo( new Vector3(4f, 3.5f, 36.4f), num);	
			else if (num == 14)
				num = TranslateTo( new Vector3(-7f, 3.5f, 58f), num);
			else if (num == 15)
				num = TranslateTo( new Vector3(-10.5f, 3.5f, 86f), num);
			else if (num == 16){
				saveGame ();
				Application.LoadLevel ("BossRoom");
			}
		}
	}
	
	
	private int LookAt( Vector3 position , int num) {

		//find the vector pointing from our position to the target
		_direction = (position - transform.position);//.normalized;
		
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		theCharacter.transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

		if (Mathf.Abs(Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f) {
			num += 1;
			reached = true;
		}

		return num;
	}

	private int TranslateTo( Vector3 position , int num) {
		
		// Calculate the distance between the follower and the leader.
		float range1 = Vector3.Distance(theCamera.transform.position, position );
		//Debug.Log ("Range = " + range1);
		
		if (range1 > 0.5) {
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
			
		} else if (range1 <= 0.5) {
			// Reached position, enable shielding and shooting
			shootScript.enabled = true;
			shieldScript.enabled = true;
			
			GetComponent<AudioSource>().Stop ();
			num += 1;

		}
		return num;
	}
	
	private void spawnBear(Vector3 position, int cover){
		GameObject bear = Instantiate(bearPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		bear.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = GetComponent<Camera>().transform.position - bear.transform.position; // make the instantiated bear face the camera
		bear.transform.rotation = Quaternion.LookRotation(direction);
		bear.transform.FindChild ("Cube").GetComponent<SkinnedMeshRenderer>().materials[0].color = new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f),1);

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
		Debug.Log ("Game saved");
	}
}
