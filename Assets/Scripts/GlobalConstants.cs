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
	
	public const float FLY_TO_PLAYER_SPEED = 3.5f;
	// ----------------------
	
	// Score gain amount when you shoot stuff
	public const int SCORE_BEAR = 10;
	public const int SCORE_LOLLIPOP = 20;
	public const int SCORE_EGG = 30;
	
	// Weapon info (rate of fire, reload speed, ammo sizes)
	public const float SHOTGUN_SHOOT_SPEED = 0.4f;
	public const float SHOTGUN_RELOAD_SPEED = 0.5f;
	public const int SHOTGUN_STARTING_AMMO = 10;
	public const int SHOTGUN_MAGAZINE_SIZE = 5;
	public const float HMG_SHOOT_SPEED = 0.05f;
	public const float HMG_RELOAD_SPEED = 0.05f;
	public const int HMG_STARTING_AMMO = 80;
	public const int HMG_MAGAZINE_SIZE = 40;
	public const float PISTOL_RELOAD_SPEED = 0.1f;
	public const int PISTOL_MAGAZINE_SIZE = 6;
	
	// Boss stats
	public const int BOSS_TOTAL_HEALTH = 240;
}

public class GlobalConstants : MonoBehaviour {
	// Empty class needed for Unity to detect this	
}
