using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class PlayerDead_mult : FlightOnDead_mult
{
	public int ScoreAdd = 250;


	void Start (){}
	
	// if player dead 
	public override void OnDead (GameObject killer)
	{
		Debug.LogError ("playerdead_mult onDead2");

		// if player dead call GameOver in GameManager
		GameManager_mult gamemanger = (GameManager_mult)GameObject.FindObjectOfType (typeof(GameManager_mult));
		gamemanger.GameOver ();
		base.OnDead (killer);
	}


	public override void OnDead_enemy (GameObject killer)
	{

		Debug.LogError ("HP_MULT: "+ DamageManager_mult.hp_mult);


		if (DamageManager_mult.hp_mult >= 10)			//working fine here = coin ++ in classic. 
		{
			Debug.LogError ("working=1");

			GameManager_mult score = (GameManager_mult)GameObject.FindObjectOfType (typeof(GameManager_mult));
			score.AddScore (ScoreAdd);
		} 
		else 
		{
			Debug.LogError ("working=2");			// have to add condition (if player touches ground and dies), then do this.

			GameManager_mult gamemanger = (GameManager_mult)GameObject.FindObjectOfType (typeof(GameManager_mult));
			gamemanger.GameOver ();
			base.OnDead (killer);
		}


	}

}



