using UnityEngine;
using System.Collections;

public static class Constants {
	// Pickups
	// ----------------------
	public const int DROP_CHANCE_HEALTH = 30;	// percentage chance
	public const int DROP_CHANCE_AMMO = 20;	// percentage chance
	public const int DROP_CHANCE_BOOST = 10;		// percentage chance
	
	public const int AMMO_PICKUP_HMG = 30;		// number of bullets
	public const int AMMO_PICKUP_SHOTGUN = 5;	// number of bullets
	public const int HEALTH_PICKUP_GAIN = 1;
	public const float BOOST_TIME = 5.0f;		// in seconds
	// ----------------------
	
	// Score gain amount when you shoot stuff
	public const int SCORE_BEAR = 10;
	public const int SCORE_LOLLIPOP = 20;
	public const int SCORE_EGG = 30;
	
	// Weapon delay between shots and reloading speed
	//public const float 
}

public class GlobalConstants : MonoBehaviour {
	// Empty class needed for unity to detect this	
}
