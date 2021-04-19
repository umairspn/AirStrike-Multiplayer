using UnityEngine;
using System.Collections;

public class FlightOnDead : MonoBehaviour {
	void Start () {
	}
	
	public virtual void OnDead(GameObject killer){
		Debug.LogError ("--called-2--");

	}

//
//	public virtual void OnDead2(GameObject killer){
//
//	}

}
