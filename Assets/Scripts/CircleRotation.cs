using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotation : MonoBehaviour {

	public float rotatingSpeed;
	public int directionVector;

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround (transform.position, directionVector * transform.forward, rotatingSpeed * 50f * Time.deltaTime);
	}
}
