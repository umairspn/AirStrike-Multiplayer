using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponController_mult : NetworkBehaviour
{
	public string[] TargetTag = new string[1]{"Enemy"};
	public WeaponLauncher_mult[] WeaponLists;
	public int CurrentWeapon = 0;
	public bool ShowCrosshair;
	
	void Awake ()
	{
		// find all attached weapons.
		if (this.transform.GetComponentsInChildren (typeof(WeaponLauncher_mult)).Length > 0) {
			var weas = this.transform.GetComponentsInChildren (typeof(WeaponLauncher_mult));
			WeaponLists = new WeaponLauncher_mult[weas.Length];
			for (int i=0; i<weas.Length; i++) {
				WeaponLists [i] = weas [i].GetComponent<WeaponLauncher_mult> ();
				WeaponLists [i].TargetTag = TargetTag;
			}
		}
	}
	public WeaponLauncher_mult GetCurrentWeapon(){
		if (CurrentWeapon < WeaponLists.Length && WeaponLists [CurrentWeapon] != null) {
			return WeaponLists [CurrentWeapon];
		}
		return null;
	}
	
	private void Start ()
	{

		for (int i=0; i<WeaponLists.Length; i++) {
			if (WeaponLists [i] != null) {
				WeaponLists [i].TargetTag = TargetTag;
				WeaponLists [i].ShowCrosshair = ShowCrosshair;
			}
		}
	}

	private void Update ()
	{
		
		for (int i=0; i<WeaponLists.Length; i++) {
			if (WeaponLists [i] != null) {
				WeaponLists [i].OnActive = false;
			}
		}
		if (CurrentWeapon < WeaponLists.Length && WeaponLists [CurrentWeapon] != null) {
			WeaponLists [CurrentWeapon].OnActive = true;
		}
	
	}

//	[Command]
	public void LaunchWeapon (int index)
	{
		Debug.Log ("weapon_controller 1");

		CurrentWeapon = index;
		if (CurrentWeapon < WeaponLists.Length && WeaponLists [index] != null) {
			WeaponLists [index].Shoot ();
			Debug.Log ("weapon_controller 2");

		}
	}
	
	public void SwitchWeapon ()
	{
		CurrentWeapon += 1;
		if (CurrentWeapon >= WeaponLists.Length) {
			CurrentWeapon = 0;	
		}
	}

//	[Command]
	public void LaunchWeapon ()
	{
		Debug.Log ("weapon_controller 3");

		if (CurrentWeapon < WeaponLists.Length && WeaponLists [CurrentWeapon] != null) 
		{
			WeaponLists [CurrentWeapon].Shoot ();
			Debug.Log ("weapon_controller 4");
		}
	}
}
