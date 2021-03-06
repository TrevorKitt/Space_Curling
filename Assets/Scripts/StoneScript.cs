﻿using UnityEngine;
using System.Collections;

public class StoneScript : MonoBehaviour 
{

	Vector3 startPosition;
	bool isGrabbed = false;
	bool isLaunched = false;
	bool isFlying = false;

	public bool IsFlying { get { return isFlying; } }
	public bool IsLaunched { get { return isLaunched; } }

	public float launchFactor = 1f;
	public string TeamName;

	// Use this for initialization
	void Start () 
	{
		// Prevent forces from acting on it until we launch it
		this.rigidbody2D.isKinematic = true;

		this.startPosition = this.transform.position;
		//print (this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrabbed) 
		{
			Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePositionInWorld.z = 0f;
			this.transform.position = mousePositionInWorld;
		}
		if(isFlying)
		{
			float cameraHeight = Camera.main.transform.position.z; 
			Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cameraHeight);
		}
		if(this.gameObject.rigidbody2D.velocity.x < 0.15 && this.gameObject.rigidbody2D.velocity.y < 0.15 && isFlying)
		{
			isFlying = false;
			returnCamera();
		}
	}
	
	void OnMouseDown()
	{
		if(!isFlying)
		{
			isGrabbed = true;
			//print (startPosition);
			//print ("Mouse is Down!");
		}
	}
	
	void OnMouseUp()
	{
		if(isGrabbed)
		{
			isGrabbed = false;
			isFlying = true;
			isLaunched = true;
			this.rigidbody2D.isKinematic = false;  // Forces act on it.
			
			Vector3 difference = this.startPosition - this.transform.position;
			this.rigidbody2D.velocity = difference * this.launchFactor;
			//print ("Mouse is Up!");
		}
	}
	
	void returnCamera()
	{
		float cameraHeight = Camera.main.transform.position.z;
		Camera.main.transform.position = new Vector3(this.startPosition.x, this.startPosition.y, cameraHeight);
	}
}
