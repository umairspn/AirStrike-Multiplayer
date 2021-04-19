/// <summary>
/// Player manager. this script will attached all Necessary components to the Plane automatically
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
// add all necessary components.
[RequireComponent(typeof(PlayerController_mult))]
[RequireComponent(typeof(Indicator_mult))]
[RequireComponent(typeof(RadarSystem_mult))]
[RequireComponent(typeof(PlayerDead_mult))]

public class PlayerManager_mult : NetworkBehaviour 
{
	[HideInInspector]
	public PlayerController_mult PlayerControl;
	[HideInInspector]
	public Indicator_mult Indicate;
	
	void Awake()
	{
		Indicate = this.GetComponent<Indicator_mult>();
		PlayerControl = this.GetComponent<PlayerController_mult>();
	}
	
	void Start () 
	{
		if (!isLocalPlayer) 
		{
			Destroy (this);
			return;
		}

		FlightView_mult view = (FlightView_mult)GameObject.FindObjectOfType(typeof(FlightView_mult));
		// setting cameras
		if(Indicate.CockpitCamera.Length > 0)
		{
			for(int i=0;i<Indicate.CockpitCamera.Length;i++)
			{
				if(Indicate.CockpitCamera[i]!=null){
					view.AddCamera(Indicate.CockpitCamera[i].gameObject);
				}
			}
		}
	}

	void Update () {
	
	}
}
