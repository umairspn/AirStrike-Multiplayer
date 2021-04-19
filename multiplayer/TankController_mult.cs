using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class TankController_mult : NetworkBehaviour {

	float movementspeed=10.0f;
	float turnspeed=90.0f;
	float cameradistance=10f;
	float cameraheight=5;

	Rigidbody localbody;
	Transform maincamera;
	Vector3 cameraoffset;

	void Start () {
		if (!isLocalPlayer) 
		{
			Destroy (this);
			return;
		}
		localbody = GetComponent<Rigidbody> ();
		cameraoffset = new Vector3 (0, cameraheight, -cameradistance);
		maincamera = Camera.main.transform;
		movecam ();
	}
	
	void FixedUpdate () {
		if (Input.GetKey ("a"))
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x+5f*Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);

		if (Input.GetKey ("d"))
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x-5f*Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
		
		//		float turnamount = CrossPlatformInputManager.GetAxis ("Horizontal1");
//		float moveamount = CrossPlatformInputManager.GetAxis ("Vertical1");
//		Vector3 deltatransform = transform.position + transform.forward * movementspeed * moveamount * Time.deltaTime;
//		localbody.MovePosition (deltatransform);
//		Quaternion deltarotation = Quaternion.Euler (turnspeed* new Vector3(0,turnamount,0) * Time.deltaTime);
//		localbody.MoveRotation (localbody.rotation * deltarotation);
		movecam ();
	}


	void movecam()
	{
		maincamera.position = transform.position;
		maincamera.rotation= transform.rotation;
		maincamera.Translate (cameraoffset);
		maincamera.LookAt (transform);

	}

}
