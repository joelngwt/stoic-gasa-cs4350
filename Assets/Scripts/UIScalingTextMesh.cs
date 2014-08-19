using UnityEngine;
using System.Collections;

//UI scaling for text mesh components
public class UIScalingTextMesh : MonoBehaviour {

	public int fontSize;
	[SerializeField] float unitsDividedBy;		// Screen width divided by these units

	// Use this for initialization
	void Start () 
	{
		fontSize = (int)(Screen.width/unitsDividedBy);
		GetComponent<TextMesh>().fontSize = fontSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		fontSize = (int)(Screen.width/unitsDividedBy);
		GetComponent<TextMesh>().fontSize = fontSize;
	}
}
