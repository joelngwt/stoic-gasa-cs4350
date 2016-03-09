using UnityEngine;
using System.Collections;

public class IngressInstructionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		this.guiText.enabled = false;
		this.GetComponent<GUIText>().fontSize = 60;
	}


	public void setInstruction(int i){
		switch(i){
		case 1: this.GetComponent<GUIText>().text = "Observe!"; 	break;
		case 2: this.GetComponent<GUIText>().text = "Duplicate!"; 	break;
		case 3: this.GetComponent<GUIText>().text = "Ready?"; 		break;
		case 4: this.GetComponent<GUIText>().text = "Go!"; 			break;
		case 5: this.GetComponent<GUIText>().text = "Correct!"; 	break;
		case 6: this.GetComponent<GUIText>().text = "Wrong!"; 		break;
		default: this.GetComponent<GUIText>().text = "ERROR!"; 		break;

		}
	}

	public void stopShowing(){
		Destroy(this);
	}

	public void setColor(int c){
		switch(c){
		case 0: 	this.GetComponent<GUIText>().color = new Color(0,87,255); break;	//blue
		case 1: 	this.GetComponent<GUIText>().color = new Color(255,99,0); break;	//orange
		case 2: 	this.GetComponent<GUIText>().color = new Color(251,255,0); break;	//yellow
		case 3: 	this.GetComponent<GUIText>().color = new Color(0,255,12); break;	//green
		case 4: 	this.GetComponent<GUIText>().color = new Color(255,0,96); break;	//red
		default: 	this.GetComponent<GUIText>().color = Color.black; break;
		
		}

	}


	// Update is called once per frame
	void Update () {
	
	}
}
