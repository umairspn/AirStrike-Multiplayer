/// <summary>
/// Damage manager. 
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DamageManager_mult : NetworkBehaviour
{
    public AudioClip[] HitSound;
    public GameObject Effect;
	[SyncVar]
    public int HP = 100;
	private int HPmax;
	public ParticleSystem OnFireParticle;
	Text armor_t, winlose;
	public static int hp_mult;
    private void Start()
    {
		winlose = GameObject.Find ("win-lose").GetComponent<Text> ();
		armor_t = GameObject.Find ("armort").GetComponent<Text> ();

		if (!isLocalPlayer) 
		{
			Destroy (this);
			return;
		}

		HPmax = HP;
		if(OnFireParticle){
			OnFireParticle.Stop();
		}
    }

//	[ServerCallback]
	void Update()
	{
		hp_mult = HP;
		armor_t.text = HP.ToString ();
	}

	// Damage function
	[ClientRpc]
    public void RpcApplyDamage(int damage,GameObject killer)
    {
		if (!isServer)
			return;

		if(HP<0)
			return;
	
        if (HitSound.Length > 0)
        {
            AudioSource.PlayClipAtPoint(HitSound[Random.Range(0, HitSound.Length)], transform.position);
        }
        HP -= damage;
		if(OnFireParticle){
			if(HP < (int)(HPmax/2.0f)){
				OnFireParticle.Play();
			}
		}
        if (HP <= 0)
        {
			Debug.LogError ("--called-3 * MULT--");


			if(this.gameObject.GetComponent<FlightOnDead_mult>())
			{
				Debug.LogError ("--called-4 * MULT--");
				this.gameObject.GetComponent<FlightOnDead_mult>().OnDead_enemy(killer);
			}
            RpcDead();
//			if (isLocalPlayer)
//				winlose.text = "GAMEOVER";
//			else
//				winlose.text = "WON !";
//			Debug.Log ("this RPCDEAD22222 is called");

        }
    }

	[ClientRpc]
    private void RpcDead()
    {
        if (Effect){
            GameObject obj = (GameObject)GameObject.Instantiate(Effect, transform.position, transform.rotation);
			if(this.GetComponent<Rigidbody>()){
				if(obj.GetComponent<Rigidbody>()){
					obj.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
					obj.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles * Random.Range(100,2000));
				}
			}
		}
		if (isLocalPlayer)
			winlose.text = "GAMEOVER";
//		else
//			winlose.text = "WON !";
		
		Debug.Log ("Damagemanager 1");

		NetworkServer.Destroy(this.gameObject);
    }

}
