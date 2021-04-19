using UnityEngine;
using System.Collections;

public class EnemyDead : FlightOnDead {
	// giving score.
	public int ScoreAdd = 250;
	
	void Start () {}
	
	// if Enemy on Dead
	public override void OnDead (GameObject killer)
	{
		Debug.LogError ("enemy dead= OFFLINE");

		if(killer){// check if killer is exist
			// check if PlayerManager are included.
			if(killer.gameObject.GetComponent<PlayerManager>()){
				// find gameMAnager and Add score
				GameManager score = (GameManager)GameObject.FindObjectOfType(typeof(GameManager));
				score.AddScore(ScoreAdd);
			}
		}
		base.OnDead (killer);
	}




//	public override void OnDead2 (GameObject killer)
//	{
//		Debug.LogError ("enemy dead= ONLINE");
//
//
//		if(killer){// check if killer is exist
//			// check if PlayerManager are included.
//			if(killer.gameObject.GetComponent<PlayerManager_mult>()){
//				// find gameMAnager and Add score
//				GameManager_mult score = (GameManager_mult)GameObject.FindObjectOfType(typeof(GameManager_mult));
//				score.AddScore(ScoreAdd);
//			}
//		}
//		base.OnDead2 (killer);
//	}
//


}
