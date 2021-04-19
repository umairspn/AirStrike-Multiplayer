using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public GUISkin skin;
	public Texture2D Logo;
	public int Mode;
	private GameManager game;
	private PlayerController play;
	private WeaponController weapon;
	GameObject gameover;
	public static int tss, tkk;

	Text score_t, kills_t, armor_t;
	void Start ()
	{
		gameover=GameObject.Find ("gameover");
		gameover.SetActive (false);

		game = (GameManager)GameObject.FindObjectOfType (typeof(GameManager));
		play = (PlayerController)GameObject.FindObjectOfType (typeof(PlayerController));
		weapon = play.GetComponent<WeaponController> ();

		score_t = GameObject.Find ("scoret").GetComponent<Text> ();
		kills_t = GameObject.Find ("killst").GetComponent<Text> ();
		armor_t = GameObject.Find ("armort").GetComponent<Text> ();

	}

	


	public void OnGUI ()
	{
		
		if (skin)
			GUI.skin = skin;
		
		
		switch (Mode) {
		case 0:
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Mode = 2;	
			}
			
			if (play) {
				
				play.Active = true;
			
//				GUI.skin.label.alignment = TextAnchor.UpperLeft;
//				GUI.skin.label.fontSize = 30;
//				GUI.Label (new Rect (20, 5, 200, 50), "Kills " + game.Killed.ToString ());
//				GUI.Label (new Rect (20, 30, 200, 50), "Score " + game.Score.ToString ());
//
				kills_t.text=game.Killed.ToString();
				score_t.text = game.Score.ToString ();
				armor_t.text = play.GetComponent<DamageManager> ().HP.ToString ();

				tss = game.Score;
				tkk = game.Killed;

//				GUI.skin.label.alignment = TextAnchor.UpperRight;
//				GUI.Label (new Rect (Screen.width - 220, 20, 200, 50), "ARMOR " + play.GetComponent<DamageManager> ().HP);
//				GUI.skin.label.fontSize = 16;
				
				// Draw Weapon system
				//if (weapon != null && weapon.WeaponLists.Length > 0 && weapon.WeaponLists.Length < weapon.CurrentWeapon && weapon.WeaponLists [weapon.CurrentWeapon] != null) {
					if (weapon.WeaponLists [weapon.CurrentWeapon].Icon)
						GUI.DrawTexture (new Rect (Screen.width - 100, Screen.height - 100, 80, 80), weapon.WeaponLists [weapon.CurrentWeapon].Icon);
				
					GUI.skin.label.alignment = TextAnchor.UpperRight;
					if (weapon.WeaponLists [weapon.CurrentWeapon].Ammo <= 0 && weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess > 0) {
						if (!weapon.WeaponLists [weapon.CurrentWeapon].InfinityAmmo)
							GUI.Label (new Rect (Screen.width - 230, Screen.height - 120, 200, 30), "Reloading " + Mathf.Floor ((1 - weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess) * 100) + "%");
					} else {
						if (!weapon.WeaponLists [weapon.CurrentWeapon].InfinityAmmo)
							GUI.Label (new Rect (Screen.width - 230, Screen.height - 120, 200, 30), weapon.WeaponLists [weapon.CurrentWeapon].Ammo.ToString ());
					}
				//}else{
					//weapon = play.GetComponent<WeaponController> ();
				//}
				
//				GUI.skin.label.alignment = TextAnchor.UpperLeft;
//				GUI.Label (new Rect (20, Screen.height - 50, 250, 30), "R Mouse : Switch Guns C : Change Camera");
			
			}else{
				play = (PlayerController)GameObject.FindObjectOfType (typeof(PlayerController));
				weapon = play.GetComponent<WeaponController> ();
			}
			break;
		case 1:
			if (play)
				play.Active = false;
			
			Screen.lockCursor = false;

			Debug.Log ("gameover screen appears");
			gameover.SetActive (true);

//			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
//			GUI.Label (new Rect (0, Screen.height / 2 + 10, Screen.width, 30), "Game Over");
//		
//			GUI.DrawTexture (new Rect (Screen.width / 2 - Logo.width / 2, Screen.height / 2 - 150, Logo.width, Logo.height), Logo);
//		
//			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 50, 300, 40), "Restart")) {
//				Application.LoadLevel (Application.loadedLevelName);
//			
//			}
//			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 40), "Main menu")) {
//				Application.LoadLevel ("airhome");
//			}


			break;
		
		case 2:
			if (play)
				play.Active = false;
			
			Screen.lockCursor = false;
			Time.timeScale = 0;



			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (0, Screen.height / 2 + 10, Screen.width, 30), "Pause");
		
			GUI.DrawTexture (new Rect (Screen.width / 2 - Logo.width / 2, Screen.height / 2 - 150, Logo.width, Logo.height), Logo);
		
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 50, 300, 40), "Resume")) {
				Mode = 0;
				Time.timeScale = 1;
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 40), "Main menu")) {
				Time.timeScale = 1;
				Mode = 0;
				Application.LoadLevel ("airhome");
			}
			break;
			
		}
		
	}

	public void gohome()
	{
		Time.timeScale = 1;
		Mode = 0;
		Application.LoadLevel ("airhome");
	}




}
