using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	[SerializeField] Platform platform1;
	[SerializeField] float xPush = 2f;
	[SerializeField] float yPush = 15f;
	//state
	Vector2 platformToBallVector;
	bool hasStarted = false;
	// Use this for initialization
	void Start () {
		platformToBallVector = transform.position - platform1.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasStarted){
			LockBallToPaddle();
			LaunchOnClick();
		}
	}
	private void LockBallToPaddle(){
		Vector2 platformPos = new Vector2(platform1.transform.position.x, platform1.transform.position.y);
		transform.position = platformPos + platformToBallVector;
	}
	private void LaunchOnClick(){
		if(Input.GetMouseButtonDown(0)){
			hasStarted = true;
			GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision){
		if(hasStarted){
			GetComponent<AudioSource>().Play();
		}
	}

}
