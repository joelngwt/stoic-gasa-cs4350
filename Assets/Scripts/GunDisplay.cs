using UnityEngine;
using System.Collections;

public class GunDisplay : MonoBehaviour {

	//public GUITexture weapon;
	public Texture2D HMG;
	public Texture2D Shotgun;
	public Texture2D Pistol;
	public Texture2D RocketLauncher;
	public Texture2D marbleBullet;
	public Texture2D rocketAmmo;
	public string currentSelection;
	public int ammoCountPistol;
	public int ammoCountHMG;
	public int ammoCountShotgun;
	public int ammoCountRocketLauncher;
	public int ammoCountTotalHMG;
	public int ammoCountTotalShotgun;
	public int ammoCountTotalRocketLauncher;
	private bool selectionOpen = false;
	public GUIText RELOADtext;
	private bool reloadTextPistol;
	private bool reloadTextShotgun;
	private bool reloadTextHMG;
	private bool reloadTextRocketLauncher;

	// Use this for initialization
	void Start () 
	{
		//guiTexture.texture = Pistol; 
		currentSelection = "Pistol";
		if(Application.loadedLevelName == "MainHall"){
			ammoCountPistol = Constants.PISTOL_MAGAZINE_SIZE;
			ammoCountHMG = Constants.HMG_MAGAZINE_SIZE;
			ammoCountShotgun = Constants.SHOTGUN_MAGAZINE_SIZE;
			ammoCountRocketLauncher = Constants.ROCKET_MAGAZINE_SIZE;
			ammoCountTotalHMG = Constants.HMG_STARTING_AMMO;
			ammoCountTotalShotgun = Constants.SHOTGUN_STARTING_AMMO;
			ammoCountTotalRocketLauncher = Constants.ROCKET_STARTING_AMMO;
		}
		else{
			if (PlayerPrefs.HasKey ("HMGTotalAmmo")) {
				ammoCountTotalHMG = PlayerPrefs.GetInt ("HMGTotalAmmo");
			}
			if (PlayerPrefs.HasKey ("ShotgunTotalAmmo")) {
				ammoCountTotalShotgun = PlayerPrefs.GetInt ("ShotgunTotalAmmo");
			}
			if (PlayerPrefs.HasKey ("RocketTotalAmmo")) {
				ammoCountTotalRocketLauncher = PlayerPrefs.GetInt ("RocketTotalAmmo");
			}
			if (PlayerPrefs.HasKey ("HMGAmmo")) {
				ammoCountHMG = PlayerPrefs.GetInt ("HMGAmmo");
			}
			if (PlayerPrefs.HasKey ("ShotgunAmmo")) {
				ammoCountShotgun = PlayerPrefs.GetInt ("ShotgunAmmo");
			}
			if (PlayerPrefs.HasKey ("RocketLauncherAmmo")) {
				ammoCountHMG = PlayerPrefs.GetInt ("RocketLauncherAmmo");
			}
		}
		RELOADtext.enabled = false;
		reloadTextPistol = false;
		reloadTextShotgun = false;
		reloadTextHMG = false;
		reloadTextRocketLauncher = false;
	}

	void Update() {
		// Scroll up 
		// Cycle: Pistol --> HMG --> Shotgun --> Rocket launcher --> Pistol
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			if (currentSelection == "Pistol") {
				currentSelection = "HMG";
			}
			else if (currentSelection == "HMG") {
				currentSelection = "Shotgun";
			}
			else if (currentSelection == "Shotgun") {
				currentSelection = "RocketLauncher";
			}
			else if (currentSelection == "RocketLauncher") {
				currentSelection = "Pistol";
			}
		}
		
		// Scroll down
		// Cycle: Pistol --> Rocket launcher --> Shotgun --> HMG --> Pistol
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			if (currentSelection == "Pistol") {
				currentSelection = "RocketLauncher";
			}
			else if (currentSelection == "RocketLauncher") {
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
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), Pistol) && Time.timeScale > 0){
					selectionOpen = false;
				}
			}
			else if(currentSelection == "HMG"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), HMG) && Time.timeScale > 0){
					selectionOpen = false;
				}
			}
			else if(currentSelection == "Shotgun"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), Shotgun) && Time.timeScale > 0){
					selectionOpen = false;
				}
			}
			else if(currentSelection == "RocketLauncher"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), RocketLauncher) && Time.timeScale > 0){
					selectionOpen = false;
				}
				
			}
			// ---------
			
			// Pop up choices
			if (/*currentSelection != "HMG" &&*/ GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.67, Screen.width*(float)0.15, Screen.height*(float)0.15), HMG)) { // bottom
				guiTexture.texture = HMG;
				currentSelection = "HMG";
				selectionOpen = false;
			}
			else if(/*currentSelection != "Shotgun" &&*/ GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.51, Screen.width*(float)0.15, Screen.height*(float)0.15), Shotgun)) { // middle bottom
				guiTexture.texture = Shotgun;
				currentSelection = "Shotgun";
				selectionOpen = false;
			}
			else if(/*currentSelection != "Pistol" &&*/ GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.35, Screen.width*(float)0.15, Screen.height*(float)0.15), RocketLauncher)) { // middle top
				guiTexture.texture = RocketLauncher;
				currentSelection = "RocketLauncher";
				selectionOpen = false;
			}
			else if(/*currentSelection != "Pistol" &&*/ GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.19, Screen.width*(float)0.15, Screen.height*(float)0.15), Pistol)) { // top
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
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), Pistol) && Time.timeScale > 0){
					selectionOpen = true;
				}
			}
			else if(currentSelection == "HMG"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), HMG) && Time.timeScale > 0){
					selectionOpen = true;
				}
			}
			else if(currentSelection == "Shotgun"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), Shotgun) && Time.timeScale > 0){
					selectionOpen = true;
				}
			}
			else if(currentSelection == "RocketLauncher"){
				if(GUI.Button (new Rect (Screen.width*(float)0.81, Screen.height*(float)0.83, Screen.width*(float)0.15, Screen.height*(float)0.15), RocketLauncher) && Time.timeScale > 0){
					selectionOpen = true;
				}
			}
		}

		// Ammo display
		if(currentSelection == "Pistol"){
			for(int i = 0; i < ammoCountPistol; i++){
				// Draws the ammo (marbles) at the bottom of the screen
				GUI.DrawTexture(new Rect(Screen.width *(float)0.73 - (Screen.width*(float)0.019 * i), Screen.height *(float)0.88, Screen.width*(float)0.05, Screen.height*(float)0.08), marbleBullet);
			}
			if(ammoCountPistol == 0 && reloadTextPistol == false){
				reloadTextPistol = true;
				StartCoroutine(RELOADpistol());
			}
		}
		else if(currentSelection == "Shotgun"){
			for(int i = 0; i < ammoCountShotgun; i++){
				// Draws the ammo (marbles) at the bottom of the screen
				GUI.DrawTexture(new Rect(Screen.width *(float)0.73 - (Screen.width*(float)0.019 * i), Screen.height *(float)0.88, Screen.width*(float)0.05, Screen.height*(float)0.08), marbleBullet);
			}
			if(ammoCountShotgun == 0 && reloadTextShotgun == false){
				reloadTextShotgun = true;
				StartCoroutine(RELOADshotgun());
			}
		}
		else if(currentSelection == "RocketLauncher"){
			for(int i = 0; i < ammoCountRocketLauncher; i++){
				// Draws the ammo (marbles) at the bottom of the screen
				GUI.DrawTexture(new Rect(Screen.width *(float)0.73 - (Screen.width*(float)0.019 * i), Screen.height *(float)0.87, Screen.width*(float)0.05, Screen.height*(float)0.1), rocketAmmo);
			}
			if(ammoCountRocketLauncher == 0 && reloadTextRocketLauncher == false){
				reloadTextRocketLauncher = true;
				StartCoroutine(RELOADshotgun());
			}
		}
		else if(currentSelection == "HMG"){
			for(int i = 0; i < ammoCountHMG; i++){
				// Draws the ammo (marbles) at the bottom of the screen
				if(i % 2 == 0){
					GUI.DrawTexture(new Rect(Screen.width *(float)0.73 - (Screen.width*(float)0.003 * i), Screen.height *(float)0.88, Screen.width*(float)0.05, Screen.height*(float)0.08), marbleBullet);
				}
				else{
					GUI.DrawTexture(new Rect(Screen.width *(float)0.72 - (Screen.width*(float)0.003 * i), Screen.height *(float)0.83, Screen.width*(float)0.05, Screen.height*(float)0.08), marbleBullet);
				}
			}
			if(ammoCountHMG == 0 && reloadTextHMG == false){
				reloadTextHMG = true;
				StartCoroutine(RELOADHMG());
			}
		}
		
	}
	
	// Flashing RELOAD word
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
	
	// Flashing RELOAD word
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
	
	// Flashing RELOAD word
	IEnumerator RELOADrocketLauncher(){
		while(ammoCountRocketLauncher == 0 && currentSelection == "RocketLauncher"){
			RELOADtext.enabled = true;
			yield return new WaitForSeconds(0.5F);
			RELOADtext.enabled = false;
			yield return new WaitForSeconds(0.5F);
		}
		reloadTextRocketLauncher = false;
		yield break;
	}
	
	// Flashing RELOAD word
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