/// <summary>
/// Player controller.
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(FlightSystem_mult))]

public class PlayerController_mult : NetworkBehaviour {

	Transform maincamera;
	Vector3 cameraoffset;
	float cameradistance=20f;
	float cameraheight=8;

	FlightSystem_mult flight;// Core plane system
	WeaponLauncher_mult_edited flight_new;
	FlightView_mult View;
	
	public bool Active = true;
	public bool SimpleControl;// make it easy to control Plane will turning easier.
	public bool Acceleration;// Mobile*** enabled gyroscope controller
	public float AccelerationSensitivity = 5;// Mobile*** gyroscope sensitivity
	private TouchScreenVal controllerTouch;// Mobile*** move
	private TouchScreenVal fireTouch;// Mobile*** fire
	private TouchScreenVal switchTouch;// Mobile*** swich
	private TouchScreenVal sliceTouch;// Mobile*** slice
	private bool directVelBack;
	public GUISkin skin;
	public bool ShowHowto;
	Button change_cam, change_wpn, gyroscope, simplecont;
	public GameObject cam;
	void Start () 
	{
		if (!isLocalPlayer) 
		{
			Destroy (this);
			return;
		}
		cam=(GameObject)Instantiate(Resources.Load("Main Camera"));


		cameraoffset = new Vector3 (0, cameraheight, -cameradistance);
//		maincamera = Camera.main.transform;
		maincamera=cam.transform;
		movecam ();

		change_cam=GameObject.Find("changeacm").GetComponent<Button>();
		change_cam.onClick.AddListener(change_cam_click);

		change_wpn=GameObject.Find("changewpn").GetComponent<Button>();
		change_wpn.onClick.AddListener(change_wpn_click);

		gyroscope=GameObject.Find("gyroscope").GetComponent<Button>();
		gyroscope.onClick.AddListener(gyroscope_click);

		simplecont=GameObject.Find("simplecont").GetComponent<Button>();
		simplecont.onClick.AddListener(simplecont_click);

		flight = this.GetComponent<FlightSystem_mult>();
		flight_new = this.GetComponent<WeaponLauncher_mult_edited>();

		View = (FlightView_mult)GameObject.FindObjectOfType(typeof(FlightView_mult));
		// setting all Touch screen controller in the position
		controllerTouch = new TouchScreenVal (new Rect (0, 0, Screen.width / 2, Screen.height - 100));
		fireTouch = new TouchScreenVal (new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height));
		switchTouch = new TouchScreenVal (new Rect (0, Screen.height - 100, Screen.width / 2, 100));
		
		sliceTouch = new TouchScreenVal (new Rect (0, 0, Screen.width / 2, 50));
		
		if(flight)
		directVelBack = flight.DirectVelocity;

		Invoke ("change_cam_click", 0.2f);
		Invoke ("change_cam_click", 0.2f);
	
	}


	void FixedUpdate()
	{
		
	}


	void change_cam_click()
	{
		Debug.Log ("cam clicked.............");
		if(View)  
			View.SwitchCameras ();	
	}

	void change_wpn_click()
	{
		if(flight) 
			flight.WeaponControl.SwitchWeapon (); 
	}

	void gyroscope_click()
	{
		Debug.Log ("GYROSCOPE CLICKED");
		Acceleration = !Acceleration;
	}

	void simplecont_click()
	{
		Debug.Log ("SIMPLE CLICKED");
		if(flight)
			SimpleControl = !SimpleControl;
	}

	void Update () {

//		if (Input.GetKey ("a"))
//			gameObject.transform.position = new Vector3 (gameObject.transform.position.x-5f*Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
//		if (Input.GetKey ("d"))
//			gameObject.transform.position = new Vector3 (gameObject.transform.position.x+5f*Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
//		
//
		if(!flight || !Active)
			return;
		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		// On Desktop
		CmdDesktopController();
		#else
		// On Mobile device
		MobileController();
		#endif

		movecam ();



	}
	
//	[Command]
	void CmdDesktopController(){
		// Desktop controller
		flight.SimpleControl = SimpleControl;
		
		// lock mouse position to the center.
		Screen.lockCursor = true;
		
		flight.AxisControl(new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y") ));
		if(SimpleControl){
			flight.DirectVelocity = false;
			flight.TurnControl(Input.GetAxis("Mouse X"));
		}else{
			flight.DirectVelocity = directVelBack;	
		}

		flight.TurnControl(Input.GetAxis ("Horizontal"));
		flight.SpeedUp(Input.GetAxis ("Vertical"));
		
		
//		if(Input.GetButton("Fire1"))
//		{
////			flight.WeaponControl.LaunchWeapon();
//			flight_new.CmdShoot ();
//			Debug.Log ("BY MOUSE");
//        }
		
		if(Input.GetButtonDown("Fire2")){
            flight.WeaponControl.SwitchWeapon();
        }
		
       	if (Input.GetKeyDown (KeyCode.C)) {
			if(View)
				View.SwitchCameras ();	
		}	
	}
	
	
	void MobileController(){
		// Mobile controller
		
		flight.SimpleControl = SimpleControl;
		
		if (Acceleration) {
			// get axis control from device acceleration
			Vector3 acceleration = Input.acceleration;
			Vector2 accValActive = new Vector2 (acceleration.x, (acceleration.y + 0.3f) * 0.5f) * AccelerationSensitivity;
			flight.FixedX = false;
			flight.FixedY = false;
			flight.FixedZ = true;
			
			flight.AxisControl (accValActive);
			flight.TurnControl (accValActive.x);
		} else {
			flight.FixedX = true;
			flight.FixedY = false;
			flight.FixedZ = true;
			// get axis control from touch screen
			Vector2 dir = controllerTouch.OnDragDirection (true);
			dir = Vector2.ClampMagnitude(dir,1.0f);
			flight.AxisControl (new Vector2 (dir.x,-dir.y) * AccelerationSensitivity * 0.7f);
			flight.TurnControl (dir.x * AccelerationSensitivity * 0.3f);
		}
		sliceTouch.OnDragDirection(true);
		// slice speed
		flight.SpeedUp(sliceTouch.slideVal.x);
		
		if (fireTouch.OnTouchPress ()) {
			flight.WeaponControl.LaunchWeapon ();
			Debug.Log("func touch playercontroller");
		}	
	}
	
	
	// you can remove this part..
	void OnGUI ()
	{
		if(!ShowHowto)
			return;
		
		if(skin)
			GUI.skin = skin;
		//start
//		if(GUI.Button(new Rect(20,150,200,40),"Gyroscope "+Acceleration)){
//			Acceleration = !Acceleration;
//		}
//		
//		if(GUI.Button(new Rect(20,200,200,40),"Change View")){
//			if(View)
//				View.SwitchCameras ();	
//		}
//		
//		if(GUI.Button(new Rect(20,250,200,40),"Change Weapons")){
//			if(flight)
//				flight.WeaponControl.SwitchWeapon ();
//		}
//		
//		if(GUI.Button(new Rect(20,300,200,40),"Simple Control "+SimpleControl)){
//			if(flight)
//				SimpleControl = !SimpleControl;
//		}
//
//		if(GUI.Button(new Rect(20,400,100,40),"Pause Game "+SimpleControl)){
//			if (flight)
//				Time.timeScale = 0;
//		}
		//end
	}



	public void pausegame()
	{
		if (flight)
			Time.timeScale = 0;
	}

	public void changewpn()
	{

	}
	public void changecam()
	{

	}


	void movecam()
	{
		maincamera.position = transform.position;
		maincamera.rotation= transform.rotation;
		maincamera.Translate (cameraoffset);
		maincamera.LookAt (transform);

	}
}
