using UnityEngine;
using System.Collections;
[ExecuteInEditMode] 

// UI scaling for different devices/resolution
public class UIScaling : MonoBehaviour 
{
	// The values for the UI size is stored in the GlobalConstants script
	public float unitsHeight, unitsLength;
	public float pixelHeight, pixelLength; 	// Actual pixel height and width, for debugging
	public int fontNumber;					// Font size
	public bool middleScreenWidth, rightOfScreen, leftOfScreen, editable;	// Flags to use certain positioning properties

	public float lengthGet
	{
		get
		{
			return this.pixelLength;
		}
	}

	// Adjusts the scale of the UI according to screen size
	public void Rescale()
	{
		pixelHeight = (Screen.height/20f) * unitsHeight;
		pixelLength = (Screen.width/20f) * unitsLength;
		if (gameObject.GetComponent<GUITexture>() != null)
		{
			textureScaling = true;
			GUIPositions();
		}
		else if (gameObject.GetComponent<GUIText>() != null)
		{
			textScaling = true;
			GUITextPos();
		}
	}

	bool textScaling, textureScaling;

	// Use this for initialization
	void Awake() 
	{
		editable = false;
		Rescale();
	}

	// Update is called once per frame
	void Update () 
	{
		// Rescales while updating
		if (editable)
		{
			if (textureScaling)
			{
				pixelHeight = (Screen.height/20f) * unitsHeight;
				pixelLength = (Screen.width/20f) * unitsLength;
				GUIPositions();
			}
			if (textScaling)
			{
				fontNumber = (int)(Screen.width/unitsLength);
				GUITextPos();
			}
		}
	}

	// Applies scaling to texture
	void GUIPositions()
	{
		if (middleScreenWidth)
		{
			GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2 - pixelLength/2, 0, pixelLength, pixelHeight);
		}
		else if (rightOfScreen)
		{
			GetComponent<GUITexture>().pixelInset = new Rect(Screen.width - (pixelLength + (Screen.width/20f)), 0, pixelLength, pixelHeight);
		}
		else if (leftOfScreen)
		{
			GetComponent<GUITexture>().pixelInset = new Rect((Screen.width/20f), 0, pixelLength, pixelHeight);
		}
		else
		{
			GetComponent<GUITexture>().pixelInset = new Rect(0, 0, pixelLength, pixelHeight);
		}
	}

	// Applies scaling to text
	void GUITextPos()
	{
		fontNumber = (int)(Screen.width/unitsLength);
		GetComponent<GUIText>().fontSize = fontNumber;
	}
}
