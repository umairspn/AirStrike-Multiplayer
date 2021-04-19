using UnityEngine;
using System.Collections;

public class EnemyDead_mult : FlightOnDead_mult {
	// giving score.
	public int ScoreAdd = 250;
	
	void Start () {}
	

	public override void OnDead (GameObject killer)
	{
		Debug.LogError ("enemy dead= ONLINE");


		if(killer){// check if killer is exist
			// check if PlayerManager are included.
			if(killer.gameObject.GetComponent<PlayerManager_mult>()){
				// find gameMAnager and Add score
				GameManager_mult score = (GameManager_mult)GameObject.FindObjectOfType(typeof(GameManager_mult));
				score.AddScore(ScoreAdd);
			}
		}
		base.OnDead (killer);
	}



}
