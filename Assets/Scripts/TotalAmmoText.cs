using UnityEngine;
using System.Collections;

public class TotalAmmoText : MonoBehaviour {

	//public TextMesh textMesh;
	public GunDisplay gunDisplayScript;

	// Update is called once per frame
	void Update () {
		if (gunDisplayScript.currentSelection == "Pistol") {
			GetComponent<GUIText>().text = "∞";
		}
		else if (gunDisplayScript.currentSelection == "HMG") {
			GetComponent<GUIText>().text = gunDisplayScript.ammoCountTotalHMG.ToString ();
		}
		else if(gunDisplayScript.currentSelection == "Shotgun"){
			GetComponent<GUIText>().text = gunDisplayScript.ammoCountTotalShotgun.ToString ();
		}
		else if(gunDisplayScript.currentSelection == "RocketLauncher"){
			GetComponent<GUIText>().text = gunDisplayScript.ammoCountTotalRocketLauncher.ToString ();
		}
	}
}
