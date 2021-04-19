using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	// basic game score
	public int Score = 0;
	public int Killed = 0;
	int modern_planes_killed, classic_planes_killed;

	void Start () 
	{
		modern_planes_killed = PlayerPrefs.GetInt ("modern_killed");
		classic_planes_killed = PlayerPrefs.GetInt ("classic_killed");

		Score = 0;
		Killed = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// add score function
	public void AddScore(int score)
	{
		Score += score;
		Killed +=1;
		if (homescr.offlinemode == 2) 
		{
			modern_planes_killed += 1;
			PlayerPrefs.SetInt ("modern_killed",modern_planes_killed);
		}
		if (homescr.offlinemode == 1) 
		{
			Debug.Log ("-------SCORE ++ --------");
			classic_planes_killed += 1;
			PlayerPrefs.SetInt ("classic_killed",classic_planes_killed);
		}

	}
	
	void OnGUI(){
		//GUI.Label(new Rect(20,20,300,30),"Kills "+Score);
	}
	// game over fimction
	public void GameOver(){
		GameUI menu = (GameUI)GameObject.FindObjectOfType(typeof(GameUI));
		if(menu){
			menu.Mode = 1;	
		}
	}
}
