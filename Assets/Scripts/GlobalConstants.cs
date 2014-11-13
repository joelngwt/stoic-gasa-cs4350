using UnityEngine;
using System.Collections;

public static class Constants {
	// Pickups
	// ----------------------
	public const int DROP_CHANCE_HEALTH = 30;		// percentage chance
	public const int DROP_CHANCE_AMMO = 20;			// percentage chance
	public const int DROP_CHANCE_BOOST = 10;		// percentage chance
	
	public const int AMMO_PICKUP_HMG = 30;			// number of HMG bullets gained from pickup
	public const int AMMO_PICKUP_SHOTGUN = 5;		// number of shotgun bullets gained from pickup
	public const int HEALTH_PICKUP_GAIN = 1;		// Amount of health gained from pickup
	public const float BOOST_TIME = 5.0f;			// in seconds
	public const float STAY_FOR = 10.0f;			// Amount of time the pickup stays in the level after spawning
	
	public const float FLY_TO_PLAYER_SPEED = 9.5f;	// Speed at which the pickup flies towards player
	// ----------------------
	
	// Score gain amount when you shoot stuff
	public const int SCORE_BEAR = 10;				// Amount of score gained when shooting the bear
	public const int SCORE_LOLLIPOP = 20;			// Amount of score gained when shooting the lollipop
	public const int SCORE_EGG = 30;				// Amount of score gained when shooting the egg
	
	// Weapon info (rate of fire, reload speed, ammo sizes)
	public const float SHOTGUN_SHOOT_SPEED = 0.25f;		// Shotgun rate of fire
	public const float SHOTGUN_RELOAD_SPEED = 0.3f;		// Shotgun reload delay between each bullet
	public const int SHOTGUN_STARTING_AMMO = 50;		// Amount of ammo the shotgun starts with in level 1
	public const int SHOTGUN_MAGAZINE_SIZE = 5;			// Amount of shots in 1 shotgun magazine
	public const float HMG_SHOOT_SPEED = 0.05f;			// HMG rate of fire
	public const float HMG_RELOAD_SPEED = 0.05f;		// HMG reload delay between each bullet
	public const int HMG_STARTING_AMMO = 160;        	// Amount of ammo the HMG starts with in level 1
	public const int HMG_MAGAZINE_SIZE = 40;			// Amount of shots in 1 HMG magazine
	public const float ROCKET_SHOOT_SPEED = 0.3f;		// Rocket launcher rate of fire
	public const float ROCKET_RELOAD_SPEED = 0.3f;		// Rocket launcher reload delay between each bullet
	public const int ROCKET_STARTING_AMMO = 30;			// Amount of ammo the rocket launcher starts with in level 1
	public const int ROCKET_MAGAZINE_SIZE = 3;			// Amount of shots in 1 rocket launcher magazine
	public const float ROCKET_EXPLOSION_RADIUS = 10.0f;	// Base radius
	public const float ROCKET_EXPLOSION_RADIUS_MAINHALL_MULTIPLIER = 2.5f;	
	public const float ROCKET_EXPLOSION_RADIUS_BOSSROOM_MULTIPLIER = 2.5f;
	public const float ROCKET_EXPLOSION_RADIUS_ACTUALBOSSROOM_MULTIPLIER = 2.0f;
	public const int ROCKET_FLYING_SPEED = 75;			// Speed at which the missile moves
	public const float PISTOL_RELOAD_SPEED = 0.05f;		// Pistol reload delay between each bullet
	public const int PISTOL_MAGAZINE_SIZE = 10;			// Amount of shots in 1 pistol magazine
	
	// Boss stats	
	public const float BOSS_TOTAL_HEALTH = 200.0f;				// Amount of health the boss has
		// First 20% - shooting rate
		public const float BOSS_SPRAY_TIME = 3.0f;				// Time spent shooting
		public const float BOSS_IDLE_TIME = 2.0f;				// Time spent idling
		public const float BOSS_FIRE_RATE = 0.1f;				// Time in between shots
		// Jellybean Bomb stats
		public const float BOSS_BOMB_FUSE_TIME = 10.0f;			// Time it takes before the bomb explodes
		public const float BOSS_BOMB_FAST_BLINK_SPEED = 0.25f;	// Fast blinking speed (going to explode)
		public const float BOSS_BOMB_SLOW_BLINK_SPEED = 0.5f;	// Slow blinking speed
		// Kinder Surprise stats
		public const float BOSS_KINDER_THROW_TIME = 0.025f;		// Amount of time the boss spends throwing 
		public const float BOSS_KINDER_IDLE_TIME = 10.0f;		// Amount of time the boss idles after throwing
		
	public const int CRACKED_ROOF_HEALTH = 25;					// Amount of health the roof has
}

public class GlobalConstants : MonoBehaviour {
	// Empty class needed for Unity to detect this	
}
