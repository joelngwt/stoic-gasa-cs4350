using UnityEngine;

public class TimerForIngress: MonoBehaviour {
	public float seconds;
	public float miliseconds;
	public bool runTimer;

	void Start(){
	// Update these depending on which style is used
		seconds = 20;
		miliseconds = 0;
		runTimer = false;
	}
	
	void Update(){
		if(seconds<0) {
			stopTimer();
			GetComponent<GUIText>().text = "00:00";
		}

		if(runTimer){
			if(miliseconds <= 0){
				if(seconds >= 0){
					seconds--;
				}
				
				miliseconds = 100;
			}
			
			miliseconds -= Time.deltaTime * 100;
			
//			guiText.text = string.Format("{0} : {1}", seconds, (int)miliseconds);
			GetComponent<GUIText>().text = ToString();

		}	
	}

	public void stopTimer(){
		runTimer=false;
	}

	public void startTimer(){
		runTimer=true;
	}

	override public string ToString(){
		string s;

		if(seconds > 9){
			s = seconds.ToString();
		} else {
			s = "0"+seconds.ToString();
		}

		int ms = Mathf.FloorToInt(miliseconds);

		if(miliseconds <= 0){
			s = s+":00";
		}else if(ms > 9){
			s = s+":"+ms.ToString();
		}else{
			s = s+":0"+ms.ToString();
		}

		return s;
	}
}