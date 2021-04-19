﻿/// <summary>
/// Nav mode. this script will display an enemys, cockpit HUD , crosshair and define a plane cameras.
/// </summary>
using UnityEngine;
using System.Collections;

public enum NavMode_mult
{
	Third,
	Cockpit,
	None
}

public class Indicator_mult : MonoBehaviour
{
	public string[] TargetTag;// all target tag
	public Texture2D[] NavTexture;
	public Texture2D Crosshair;
	public Texture2D Crosshair_in;
	public Vector2 CrosshairOffset;
	public Vector2 CrosshairOffset_in;
	public float DistanceSee = 800; // limited distance
	public float Alpha = 0.7f;// GUI opacity.
	
	public Camera[] CockpitCamera;// cameras list
	public int PrimaryCameraIndex;// which camera is the cockpit camera
	
	public bool Show = true;
	
	[HideInInspector]
	public NavMode_mult Mode = NavMode_mult.Third;// camera mode
	[HideInInspector]
	public FlightSystem_mult flight;// core plane system

	void Awake ()
	{
		if (CockpitCamera.Length <= 0) {
			if (this.transform.GetComponentsInChildren (typeof(Camera)).Length > 0) {
				var cams = this.transform.GetComponentsInChildren (typeof(Camera));
				CockpitCamera = new Camera[cams.Length];
				for (int i=0; i<cams.Length; i++) {
					CockpitCamera [i] = cams [i].GetComponent<Camera>();
				}
			}
		}
		flight = this.GetComponent<FlightSystem_mult> ();
	}
	
	void Start ()
	{

	}

	public void DrawNavEnemy ()
	{
		// find all target in TargetTag[]
		for (int t=0; t<TargetTag.Length; t++) {
			if (GameObject.FindGameObjectsWithTag (TargetTag [t]).Length > 0) {
				GameObject[] objs = GameObject.FindGameObjectsWithTag (TargetTag [t]);
				for (int i = 0; i < objs.Length; i++) {
					if (objs [i]) {
						Vector3 dir = (objs [i].transform.position - transform.position).normalized;
						float direction = Vector3.Dot (dir, transform.forward);
						if (direction >= 0.7f) {
							float dis = Vector3.Distance (objs [i].transform.position, transform.position);
							if (DistanceSee > dis) {
								// Draw the indicator
								DrawTargetLockon (objs [i].transform, t);
							
							}
						}
					}
				}
			}
		}
	}

	void OnGUI ()
	{
		if (Show) {
			GUI.color = new Color (1, 1, 1, Alpha);
			switch (Mode) {
			case NavMode_mult.Third:
				if (Crosshair)
					GUI.DrawTexture (new Rect ((Screen.width / 2 - Crosshair.width / 2) + CrosshairOffset.x, (Screen.height / 2 - Crosshair.height / 2) + CrosshairOffset.y, Crosshair.width, Crosshair.height), Crosshair);	
				DrawNavEnemy ();
				break;
			case NavMode_mult.Cockpit:
				if (Crosshair_in)
					GUI.DrawTexture (new Rect ((Screen.width / 2 - Crosshair_in.width / 2) + CrosshairOffset_in.x, (Screen.height / 2 - Crosshair_in.height / 2) + CrosshairOffset_in.y, Crosshair_in.width, Crosshair_in.height), Crosshair_in);	
				DrawNavEnemy ();
				break;
			case NavMode_mult.None:
				
				break;
			}

			
		}
	}
	
	public void DrawTargetLockon (Transform aimtarget, int type)
	{
		if (Camera.current && Camera.current.GetComponent<Camera>() != null) {
			Vector3 dir = (aimtarget.position - Camera.current.GetComponent<Camera>().transform.position).normalized;
			float direction = Vector3.Dot (dir, Camera.current.GetComponent<Camera>().transform.forward);
			if (direction > 0.5f) {
				Vector3 screenPos = Camera.current.GetComponent<Camera>().WorldToScreenPoint (aimtarget.transform.position);
				float distance = Vector3.Distance (transform.position, aimtarget.transform.position);
				
//				GUI.DrawTexture (new Rect (screenPos.x - NavTexture [type].width / 2, Screen.height - screenPos.y - NavTexture [type].height / 2, NavTexture [type].width, NavTexture [type].height), NavTexture [type]);
            	
			}
		}
	}
	
	void Update ()
	{
		// check a current camera
		Mode = NavMode_mult.Third;
		for (int i=0; i<CockpitCamera.Length; i++) {
			if (CockpitCamera [i] != null) {
				if (CockpitCamera [i].GetComponent<Camera>().enabled) {
					if (i == PrimaryCameraIndex)
						Mode = NavMode_mult.Cockpit;	
				} 
			}
		}
	}
}
