#pragma strict

function Start () {
	var height = Camera.main.orthographicSize * 2.0f;
    var width = height / Screen.height * Screen.width;
    
    this.transform.localScale.x = width;
    this.transform.localScale.y = height;
}