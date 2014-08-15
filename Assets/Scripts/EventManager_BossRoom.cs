using UnityEngine;
using System.Collections;

public class EventManager_BossRoom : MonoBehaviour {
	// Note: Copy the Main Camera from the Menu GUI prefabs. The camera nested in the character does not work properly with this code.
	
	public float RotationSpeed = 10;
	public float movementSpeed = 40;

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
	
	// Spawning
	public GameObject bearPrefab;
	private int count;
	public GameObject lollipopPrefab;
	public GameObject EggPrefab;
	private bool reached = false;

	void Start(){
		theCamera = Camera.main.gameObject;
		theCharacter = GameObject.FindWithTag("MainCharacter");
		audio.clip = footsteps;
		num = 0;
		theCharacter.transform.rotation = theCamera.transform.rotation;
		count = 1;
	}

	// Update is called once per frame
	void Update () {
		// character hitbox follows the camera around
		theCharacter.transform.position = theCamera.transform.position;

		// Position 1
		if(count > 0 && count <= 12){
			if (num == 0) 
				num = TranslateTo( new Vector3(40f, 38f, 100f), 20f, num);	
			else if (num == 1){
				num = LookAt( new Vector3(11f, 16f, 12f), num);
			}
			else if (num == 2)
				StartCoroutine(longLook( new Vector3(10f, 38f, 105f)));
			
			if(count > 0 && count <= 6 && reached == true){
				Debug.Log ("Wave 1");
				spawnBear (new Vector3( -29f, 30f, 122f ), 2); // target1
				spawnBear (new Vector3( -23f, 30f, 91.5f  ), 1); // target2
				spawnBear (new Vector3( -70f, 60f, 49f  ),  1); // target3
				spawnLollipop (new Vector3 (  -83f, 25f, 24f )); // target4
				spawnLollipop (new Vector3 ( -40f, 140f, 180f )); // target5
				//count++;
				//count++;
				spawnBear (new Vector3( 10.5f, 35f, 127f ),  0); // target6
				reached = false;
			}
			else if(count > 6 && count <= 12 && !(GameObject.Find ("Target1") || GameObject.Find ("Target2") || GameObject.Find ("Target3") || GameObject.Find ("Target4"))){
				Debug.Log ("Wave 2");
				spawnBear (new Vector3( -31f, 60f, 135f ), 1); // target7
				spawnBear (new Vector3( 10.5f, 35f, 127f  ), 0); // target8
				spawnBear (new Vector3( -23f, 30f, 91.5f ),  1); // target9
				spawnEgg(new Vector3 ( -50f, 30f, 115f )); // target10
				spawnBear (new Vector3( -38f, 60f, 160f ),  0); // target11
				spawnBear (new Vector3( -70f, 60f, 49f  ),  1); // target12
			}
		}
		
		// Position 2
		else if(count > 12 && count <= 20 && !(GameObject.Find ("Target8") || GameObject.Find ("Target9") || GameObject.Find ("Target10") || GameObject.Find ("Target11") || GameObject.Find ("Target12") || GameObject.Find ("Target7"))){
			if (num == 3)
				num = TranslateTo( new Vector3(18f, 38f, 100f), num);	
			else if (num == 4)
				num = TranslateTo( new Vector3(-8.5f, 38f, 88.2f), num);
			else if (num == 5)
				num = LookAt( new Vector3(-8.5f, 40f, 98.2f), num);
				
			if(count > 12 && count <= 15 && reached == true){
				Debug.Log ("Wave 3");
				spawnBear (new Vector3(  -33f, 30f, 130f ), 2); // target13
				spawnBear (new Vector3(  30f, 60f, 136f ),  2); // target14
				spawnBear (new Vector3( 6f, 60f, 177f ),  0); // target15
				reached = false;
			}
			else if(count > 15 && count <= 18 && !(GameObject.Find ("Target14") || GameObject.Find ("Target15") || GameObject.Find ("Target13"))){
				Debug.Log ("Wave 4");
				spawnBear (new Vector3( -54f, 60f, 135f ),  1); // target16
				spawnBear (new Vector3(-33f, 30f, 130f  ),  2); // target17
				spawnBear (new Vector3( 6f, 60f, 177f),  0); // target18
			}
			
			else if(count > 18 && count <= 20 && !(GameObject.Find ("Target17") || GameObject.Find ("Target18") || GameObject.Find ("Target16"))){
				Debug.Log ("Wave 5");
				spawnBear (new Vector3(  30f, 60f, 136f  ),  2); // target19
				spawnBear (new Vector3(-54f, 60f, 135f  ), 1); // target20
			}
		}
		// Position 3
		else if(count > 20 && count <= 32 && !(GameObject.Find ("Target19") || GameObject.Find ("Target20"))){
			if (num == 6)
				num = TranslateTo( new Vector3(-8.5f, 38f, 99f), num);
			else if (num == 7)
				num = TranslateTo( new Vector3(-54.5f, 38f, 90f), num);
			else if (num == 8)
				num = TranslateTo( new Vector3(-54.5f, 35f, 86f), num);
			else if (num == 9)
				num = LookAt( new Vector3 (0f, 49.5f, 105.8f), num);
			//theCamera.transform.LookAt( new Vector3 (-10f, 152.6f, 0f));
			
			if(count > 20 && count <= 25 && reached == true){
				Debug.Log ("Wave 6");
				spawnBear (new Vector3(  23.5f, 30f, 91.5f ), 2); // target21
				spawnBear (new Vector3(  67f, 60f, 59f ),  2); // target22
				spawnBear (new Vector3( 30f, 60f, 136f ),  2); // target23
				spawnLollipop (new Vector3 (  76f, 90f, 190f )); // target24
				spawnLollipop (new Vector3 (  83f, 25f, 24f )); // target25
				reached = false;
			}
			else if(count > 25 && count <= 32 && !(GameObject.Find ("Target21") || GameObject.Find ("Target22") || GameObject.Find ("Target23") || GameObject.Find ("Target24") || GameObject.Find ("Target25"))){
				Debug.Log ("Wave 7");
				spawnBear (new Vector3(50f, 26.5f, 85.5f ), 0); // target26
				spawnBear (new Vector3( 32f, 30f, 125f   ),  1); // target27
				spawnBear (new Vector3( 70f, 60f, 49f), 2); // target28
				spawnLollipop (new Vector3 (  76f, 90f, 190f )); // target29
				spawnLollipop (new Vector3 (  83f, 25f, 24f )); // target30
				spawnBear (new Vector3( -14.5f, 35f, 127f   ),  0); // target31
				spawnBear (new Vector3( 23.5f, 30f, 91.5f), 2); // target32
			}
		}
		// Position 4
		else if(count > 32 && count <= 46 && !(GameObject.Find ("Target26") || GameObject.Find ("Target27") || GameObject.Find ("Target28") || GameObject.Find ("Target29") || GameObject.Find ("Target30") || GameObject.Find ("Target31") || GameObject.Find ("Target32"))){
			if (num == 10)
				num = TranslateTo( new Vector3(-40f, 30f, 81f), num);
			else if (num == 11)
				num = TranslateTo( new Vector3 (-40f, 6f, 34f), num);
			else if (num == 12)
				num = TranslateTo( new Vector3 (-39f, 6f, -5f), num);
			else if (num == 13)
				num = LookAt( new Vector3 (-20f, 14.5f, 43.4f), num);
			
			if(count > 32 && count <= 38 && reached == true){
				Debug.Log ("Wave 8");
				spawnBear (new Vector3(   -25f, 15f, 64.5f ), 0); // target33
				spawnBear (new Vector3( 29f, 5f, 47.5f  ),  0); // target34
				spawnBear (new Vector3(  67f, 60f, 59f ),  2); // target35
				spawnBear (new Vector3 ( -54f, 60f, 135f  ), 1); // target36
				spawnLollipop (new Vector3 ( -40f, 140f, 180f )); // target37
				spawnLollipop (new Vector3 ( 76f, 90f, 190f )); // target38
				reached = false;
			}
			else if(count > 38 && count <= 41 && !(GameObject.Find ("Target33") || GameObject.Find ("Target34") || GameObject.Find ("Target35") || GameObject.Find ("Target36") || GameObject.Find ("Target37") || GameObject.Find ("Target38"))){
				Debug.Log ("Wave 9");
				spawnLollipop (new Vector3( 40f, 140f, 180f )); // target39
				spawnLollipop (new Vector3( -73f, 90f, 190f   )); // target40
				spawnEgg (new Vector3( 10f, 0f, 70f)); // target41
			}
			else if(count > 41 && count <= 46 && !(GameObject.Find ("Target39") || GameObject.Find ("Target40") || GameObject.Find ("Target41"))){
				Debug.Log ("Wave 10");
				spawnBear (new Vector3(-10.5f, 30f, 75f  ), 1); // target42
				spawnBear (new Vector3( -54.5f, 15f, 64.5f   ),  0); // target43
				spawnBear (new Vector3( 67f, 60f, 59f), 2); // target44
				spawnBear (new Vector3( 29f, 5f, 47.5f), 0); // target45
				spawnEgg (new Vector3 (  10f, 0f, 70f )); // target46
			}
		}
		// Move to next zone
		else if(count > 46 && !(GameObject.Find ("Target42") || GameObject.Find ("Target43") || GameObject.Find ("Target44") || GameObject.Find ("Target45") || GameObject.Find ("Target46"))){
			//cutscene before minigame
			if (num == 14)
				num = TranslateTo( new Vector3(-27f, 6f, 7f), num);
			else if (num == 15)
				num = TranslateTo( new Vector3 (-5.5f, 6f, -8f), num);
			else if (num == 16)
				num = LookAt( new Vector3(-2f, 6f, -20.7f), num);
			else if (num == 17)
				StartCoroutine(endLook( new Vector3(-5.5f, 12f, -21f)));
			else if (num == 18)
				num = TranslateTo( new Vector3(0f, 6f, -8f), 5f, num);
			else if (num == 19)
				num = TranslateTo( new Vector3 (0f, 4f, -16f), 5f, num);
			else if (num == 20){
				saveScore ();
				Application.LoadLevel ("MiniGameOne-Ingress");
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

		Debug.Log ("intended = " + _lookRotation + ", actual = " + transform.rotation);


		if (Mathf.Abs(Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f){
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
			
			if(!audio.isPlaying){
				audio.Play ();
			}
			
			//theCharacter.transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
			theCamera.transform.Translate (dir * movementSpeed * Time.deltaTime, Space.World);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
		} else if (range1 <= 1.0) {
			// Reached position, enable shielding and shooting
			shootScript.enabled = true;
			shieldScript.enabled = true;
		
			audio.Stop ();
			num += 1;
			Debug.Log (num);
			
		}
		return num;
	}

	private int TranslateTo( Vector3 position , float spd, int num) {
		
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
			
			if(!audio.isPlaying){
				audio.Play ();
			}
			
			//theCharacter.transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
			theCamera.transform.Translate (dir * spd * Time.deltaTime, Space.World);
			theCharacter.transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			
		} else if (range1 <= 1.0) {
			// Reached position, enable shielding and shooting
			shootScript.enabled = true;
			shieldScript.enabled = true;
			
			audio.Stop ();
			num += 1;
			Debug.Log (num);
			
		}
		return num;
	}

	IEnumerator longLook( Vector3 vec ) {

		yield return new WaitForSeconds(1.5f);

		_direction = (vec - transform.position);
		_lookRotation = Quaternion.LookRotation(_direction);
		
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		theCharacter.transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		
		if (Mathf.Abs (Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f) 
			num = 3;
			
		reached = true;
	}

	IEnumerator endLook( Vector3 vec ) {
		
		yield return new WaitForSeconds(0.5f);
		
		_direction = (vec - transform.position);
		_lookRotation = Quaternion.LookRotation(_direction);
		
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		theCharacter.transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		
		if (Mathf.Abs (Mathf.Abs (transform.rotation.y) - Mathf.Abs (_lookRotation.y)) < 0.000001f) 
			num = 18;
	}
	
	private void spawnBear(Vector3 position, int cover){
		GameObject bear = Instantiate(bearPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		bear.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = camera.transform.position - bear.transform.position; // make the instantiated bear face the camera
		bear.transform.rotation = Quaternion.LookRotation(direction);
		
		Enemy e = bear.GetComponent<Enemy>();
		e.coverType = cover;
		
		count++;
	}
	
	private void spawnLollipop(Vector3 position){
		GameObject lollipop = Instantiate(lollipopPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		lollipop.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = camera.transform.position - lollipop.transform.position; // make the instantiated bear face the camera
		lollipop.transform.rotation = Quaternion.LookRotation(direction);
		count++;
	}
	
	private void spawnEgg(Vector3 position){
		GameObject egg = Instantiate(EggPrefab, new Vector3(position.x, position.y, position.z), transform.rotation) as GameObject; // add public GameObject bearPrefab at the top
		egg.name = string.Concat("Target", count.ToString()); // give them unique numbered names (remember to initialize count)
		Vector3 direction = camera.transform.position - egg.transform.position; // make the instantiated bear face the camera
		egg.transform.rotation = Quaternion.LookRotation(direction);
		count++;
	}
	
	private void saveScore(){
		PlayerPrefs.SetInt ("currentScore", (int)scoreScript.currentScore);
		Debug.Log ("Score saved");
	}
}
