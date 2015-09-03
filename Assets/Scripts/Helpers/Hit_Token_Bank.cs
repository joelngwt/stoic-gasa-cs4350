using UnityEngine;
using System.Collections;

public class Hit_Token_Bank : MonoBehaviour {
	
	////////////////////////////////////////////////////
	//
	//		Constants
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Script variables
	//
	////////////////////////////////////////////////////

	public int number_of_hit_tokens_stored;
	public int max_token_storage_size;
	public int token_recharge_amount;

	public float token_recharge_time;
	public float token_recharge_remaining_timer;

	////////////////////////////////////////////////////
	//
	//		MonoBehaviour functions
	//
	////////////////////////////////////////////////////

	public void Start () 	{

	}

	public void Update () 	{

		/*
		 * Decrement the remaining timer and 
		 * check if a token has been recharged
		 * */
		token_recharge_remaining_timer = token_recharge_remaining_timer - Time.deltaTime;

		if(token_recharge_remaining_timer <= 0.00F)
		{
			/*
			 * Recharge timer has expired. Increment 
			 * the stored tokens up to the maximum
			 * */
			number_of_hit_tokens_stored = Mathf.Min(max_token_storage_size, (number_of_hit_tokens_stored + token_recharge_amount));

			/*
			 * Reset the timer
			 * */
			token_recharge_remaining_timer = token_recharge_time;
		}

	}

	public bool request_withdraw_token () 	{

		/*
		 * Check if there is at least one 
		 * token to withdraw
		 * */
		if(number_of_hit_tokens_stored >= 1)
		{
			/*
			 * There is at least one token. 
			 * Decrement it and return true
			 * */
			number_of_hit_tokens_stored--;

			return true;
		}
		else
		{
			/*
			 * There is not at least one token. 
			 * Return false
			 * */
			return false;
		}
	}
	
	////////////////////////////////////////////////////
	//
	//		Custom functions for other scripts
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Functions available via inheritance
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Private functions for this script
	//
	////////////////////////////////////////////////////
}
