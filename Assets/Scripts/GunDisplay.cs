using UnityEngine;
using System.Collections;

public class GunDisplay : MonoBehaviour {

	//public GUITexture weapon;
	public Texture2D HMG;
	public Texture2D Shotgun;
	public Texture2D Pistol;
	public string currentSelection;
	public Texture2D bullet;
	public int ammoCountPistol;
	public int ammoCountHMG;
	public int ammoCountShotgun;
	public int ammoCountTotalHMG;
	public int ammoCountTotalShotgun;
	private bool selectionOpen = false;
	public GUIText RELOADtext;
	private bool reloadTextPistol;
	private bool reloadTextShotgun;
	private bool reloadTextHMG;

	// Use this for initialization
	void Start () 
	{
		//guiTexture.texture = Pistol; 
		currentSelection = "Pistol";
		if(Application.loadedLevelName == "MainHall"){
			ammoCountPistol = 6;
			ammoCountHMG = 40;
			ammoCountShotgun = 5;
			ammoCountTotalHMG = 80;
			ammoCountTotalShotgun = 10;
		}
		else{
			if (PlayerPrefs.HasKey ("HMGTotalAmmo")) {
				ammoCountTotalHMG = PlayerPrefs.GetInt ("HMGTotalAmmo");
			}
			if (PlayerPrefs.HasKey ("ShotgunTotalAmmo")) {
				ammoCountTotalShotgun = PlayerPrefs.GetInt ("ShotgunTotalAmmo");
			}
			if (PlayerPrefs.HasKey ("HMGAmmo")) {
				ammoCountHMG = PlayerPrefs.GetInt ("HMGAmmo");
			}
			if (PlayerPrefs.HasKey ("ShotgunAmmo")) {
				ammoCountShotgun = PlayerPrefs.GetInt ("ShotgunAmmo");
			}
		}
		RELOADtext.enabled = false;
		reloadTextPistol = false;
		reloadTextShotgun = false;
		reloadTextHMG = false;
	}

	void Update() {
		// Scroll up 
		// Cycle: Pistol --> HMG --> Shotgun --> Pistol
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			if (currentSelection == "Pistol") {
				currentSelection = "HMG";
			}
			else if (currentSelection == "HMG") {
				currentSelection = "Shotgun";
			}
			else if (currentSelection == "Shotgun") {
				currentSelection = "Pistol";
			}
		}
		
		// Scroll down
		// Cycle: Pistol --> Shotgun --> HMG --> Pistol
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			if (currentSelection == "Pistol") {
				currentSelection = "Shotgun";
			}
			else if (currentSelection == "Shotgun") {
				currentSelection = "HMG";
			}
			else if (currentSelection == "HMG") {
				currentSelection = "Pistol";
			}
		}
	
	}
	
	// OnGUI is called every frame
	void OnGUI()
	{ 
		if (selectionOpen == true) {
			// Bottom button
			if(currentSelection == "Pistol"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.18, Screen.height*(float)0.17), Pistol) && Time.timeScale > 0){
					selectionOpen = false;
				}
			}
			else if(currentSelection == "HMG"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.18, Screen.height*(float)0.17), HMG) && Time.timeScale > 0){
					selectionOpen = false;
				}
			}
			else if(currentSelection == "Shotgun"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.18, Screen.height*(float)0.17), Shotgun) && Time.timeScale > 0){
					selectionOpen = false;
				}
			
			}
			// ---------
			
			// Pop up choices
			if (/*currentSelection != "HMG" &&*/ GUI.Button (new Rect (Screen.width*(float)0.79, Screen.height*(float)0.62, Screen.width*(float)0.2, Screen.height*(float)0.2), HMG)) { // bottom
				guiTexture.texture = HMG;
				currentSelection = "HMG";
				selectionOpen = false;
			}
			else if(/*currentSelection != "Shotgun" &&*/ GUI.Button (new Rect (Screen.width*(float)0.79, Screen.height*(float)0.4, Screen.width*(float)0.2, Screen.height*(float)0.2), Shotgun)) { // middle
				guiTexture.texture = Shotgun;
				currentSelection = "Shotgun";
				selectionOpen = false;
			}
			else if(/*currentSelection != "Pistol" &&*/ GUI.Button (new Rect (Screen.width*(float)0.79, Screen.height*(float)0.18, Screen.width*(float)0.2, Screen.height*(float)0.2), Pistol)) { // top
				guiTexture.texture = Pistol;
				currentSelection = "Pistol";
				selectionOpen = false;
			}
			// ----------
		} 
		else if (selectionOpen == false)
		{
			// Display current selection
			if(currentSelection == "Pistol"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.18, Screen.height*(float)0.17), Pistol) && Time.timeScale > 0){
					selectionOpen = true;
				}
			}
			else if(currentSelection == "HMG"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.18, Screen.height*(float)0.17), HMG) && Time.timeScale > 0){
					selectionOpen = true;
				}
			}
			else if(currentSelection == "Shotgun"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.18, Screen.height*(float)0.17), Shotgun) && Time.timeScale > 0){
					selectionOpen = true;
				}
			}
		}

		// Ammo display
		if(currentSelection == "Pistol"){
			for(int i = 0; i < ammoCountPistol; i++){
				// Draws the ammo (marbles) at the bottom of the screen
				GUI.DrawTexture(new Rect(Screen.width *(float)0.73 - (Screen.width*(float)0.019 * i), Screen.height *(float)0.88, Screen.width*(float)0.05, Screen.height*(float)0.08), bullet);
			}
			if(ammoCountPistol == 0 && reloadTextPistol == false){
				reloadTextPistol = true;
				StartCoroutine(RELOADpistol());
			}
		}
		else if(currentSelection == "Shotgun"){
			for(int i = 0; i < ammoCountShotgun; i++){
				// Draws the ammo (marbles) at the bottom of the screen
				GUI.DrawTexture(new Rect(Screen.width *(float)0.73 - (Screen.width*(float)0.019 * i), Screen.height *(float)0.88, Screen.width*(float)0.05, Screen.height*(float)0.08), bullet);
			}
			if(ammoCountShotgun == 0 && reloadTextShotgun == false){
				reloadTextShotgun = true;
				StartCoroutine(RELOADshotgun());
			}
		}
		else if(currentSelection == "HMG"){
			for(int i = 0; i < ammoCountHMG; i++){
				// Draws the ammo (marbles) at the bottom of the screen
				if(i % 2 == 0){
					GUI.DrawTexture(new Rect(Screen.width *(float)0.73 - (Screen.width*(float)0.003 * i), Screen.height *(float)0.88, Screen.width*(float)0.05, Screen.height*(float)0.08), bullet);
				}
				else{
					GUI.DrawTexture(new Rect(Screen.width *(float)0.72 - (Screen.width*(float)0.003 * i), Screen.height *(float)0.83, Screen.width*(float)0.05, Screen.height*(float)0.08), bullet);
				}
			}
			if(ammoCountHMG == 0 && reloadTextHMG == false){
				reloadTextHMG = true;
				StartCoroutine(RELOADHMG());
			}
		}
		
	}
	
	IEnumerator RELOADpistol(){
		while(ammoCountPistol == 0 && currentSelection == "Pistol"){
			RELOADtext.enabled = true;
			yield return new WaitForSeconds(0.5F);
			RELOADtext.enabled = false;
			yield return new WaitForSeconds(0.5F);
		}
		reloadTextPistol = false;
		yield break;
	}
	
	IEnumerator RELOADshotgun(){
		while(ammoCountShotgun == 0 && currentSelection == "Shotgun"){
			RELOADtext.enabled = true;
			yield return new WaitForSeconds(0.5F);
			RELOADtext.enabled = false;
			yield return new WaitForSeconds(0.5F);
		}
		reloadTextShotgun = false;
		yield break;
	}
	
	IEnumerator RELOADHMG(){
		while(ammoCountHMG == 0 && currentSelection == "HMG"){
			RELOADtext.enabled = true;
			yield return new WaitForSeconds(0.5F);
			RELOADtext.enabled = false;
			yield return new WaitForSeconds(0.5F);
		}
		reloadTextHMG = false;
		yield break;
	}
}