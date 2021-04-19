using UnityEngine;
using System.Collections;

public class FlightOnDead_mult : MonoBehaviour {
	void Start () {
	}
	
	public virtual void OnDead(GameObject killer){
		Debug.LogError ("--CALLED Flight_on_mult--");
	}

	public virtual void OnDead_enemy(GameObject killer){
		Debug.LogError ("--CALLED enemy dead--");
	}
}
