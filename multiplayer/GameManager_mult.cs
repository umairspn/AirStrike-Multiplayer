using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager_mult : NetworkBehaviour{

	// basic game score
	[SyncVar]
	public int Score = 0;
	[SyncVar]
	public int Killed = 0;
	int modern_planes_killed, classic_planes_killed;
	Text score_t, kills_t;

	void Start () 
	{
		score_t = GameObject.Find ("scoret").GetComponent<Text> ();
		kills_t = GameObject.Find ("killst").GetComponent<Text> ();

//		if (!isLocalPlayer) 
//		{
//			Destroy (this);
//			return;
//		}

		modern_planes_killed = PlayerPrefs.GetInt ("modern_killed");
		classic_planes_killed = PlayerPrefs.GetInt ("classic_killed");

		Score = 0;
		Killed = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	// add score function
	public void AddScore(int score)
	{

		Debug.LogError("okay bro + ONLINE");

		Score += score;
		Killed +=1;

		classic_planes_killed += 1;
		PlayerPrefs.SetInt ("classic_killed",classic_planes_killed);

		kills_t.text= Killed.ToString();
		score_t.text = Score.ToString ();


	}
	
	void OnGUI(){
		//GUI.Label(new Rect(20,20,300,30),"Kills "+Score);
	}
	// game over fimction
	public void GameOver(){

		Debug.LogError ("Gamemamager_mult 1");

		if (!isLocalPlayer)
			return;
		else 
		{
			GameUI_mult menu = (GameUI_mult)GameObject.FindObjectOfType (typeof(GameUI_mult));
			if (menu) {
				menu.Mode = 1;	
			}
		}

	}
}
