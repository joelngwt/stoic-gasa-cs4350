using UnityEngine;
using System.Collections;

public class TotalAmmoText : MonoBehaviour {

	//public TextMesh textMesh;
	public GunDisplay gunDisplayScript;

	// Update is called once per frame
	void Update () {
		if (gunDisplayScript.currentSelection == "Pistol") {
			guiText.text = "∞";
		}
		else if (gunDisplayScript.currentSelection == "HMG") {
			guiText.text = gunDisplayScript.ammoCountTotalHMG.ToString ();
		}
		else if(gunDisplayScript.currentSelection == "Shotgun"){
			guiText.text = gunDisplayScript.ammoCountTotalShotgun.ToString ();
		}
	}
}
