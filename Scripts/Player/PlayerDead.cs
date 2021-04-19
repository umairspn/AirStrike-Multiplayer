using UnityEngine;
using System.Collections;

public class PlayerDead : FlightOnDead
{
	void Start (){}
	
//	// if player dead 
//	public override void OnDead2 (GameObject killer)
//	{
//		// if player dead call GameOver in GameManager
//		GameManager_mult gamemanger = (GameManager_mult)GameObject.FindObjectOfType (typeof(GameManager_mult));
//		gamemanger.GameOver ();
//		base.OnDead2 (killer);
//	}


	public override void OnDead (GameObject killer)
	{
		Debug.LogError ("--called-1--");

		// if player dead call GameOver in GameManager
		GameManager gamemanger = (GameManager)GameObject.FindObjectOfType (typeof(GameManager));
		gamemanger.GameOver ();
		base.OnDead (killer);
	}


}
