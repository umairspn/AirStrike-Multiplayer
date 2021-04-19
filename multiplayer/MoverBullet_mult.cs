using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MoverBullet_mult : WeaponBase_mult
{
    public int Lifetime;
    public float Speed = 80;
    public float SpeedMax = 80;
    public float SpeedMult = 1;

    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }
	// move bullet by force
    private void FixedUpdate()
    {
		if(!this.GetComponent<Rigidbody>())
			return;
		
        if (!RigidbodyProjectile)
        {
            GetComponent<Rigidbody>().velocity = transform.forward*Speed;
        }else{
//			Debug.Log ("mover_bulllet 1");

			if(this.GetComponent<Rigidbody>().velocity.normalized!=Vector3.zero)
			this.transform.forward = this.GetComponent<Rigidbody>().velocity.normalized;
//			NetworkServer.Spawn (gameObject);
		}
        if (Speed < SpeedMax)
        {
            Speed += SpeedMult * Time.fixedDeltaTime;

        }
    }

}
