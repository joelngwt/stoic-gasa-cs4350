using UnityEngine;
using System.Collections;

public class EventManager_Minigame_Skewer : MonoBehaviour {
	
	public Stage_Sequence_Reader attached_sequence_reader;

	// Use this for initialization
	void Start () {
	
		/*
		 * Attach the correct Stage Sequence to the reader
		 * */
		attached_sequence_reader.attached_sequence = new Stage_Sequence_Skewer_1_1(attached_sequence_reader);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
