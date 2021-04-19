using UnityEngine;
using System.Collections;

public class Damage : DamageBase
{
    public bool Explosive;
    public float ExplosionRadius = 20;
    public float ExplosionForce = 1000;
	public bool HitedActive = true;
	public float TimeActive = 0;
	public bool RandomTimeActive;
	private float timetemp = 0;
	
	public GameObject DestroyAfterObject;
	public float DestryAfterDuration = 10;

    private void Start()
    {
		timetemp = Time.time;
		if(RandomTimeActive)
			TimeActive = Random.Range(TimeActive/2f,TimeActive);
		
        if (!Owner || !Owner.GetComponent<Collider>()) return;
        Physics.IgnoreCollision(GetComponent<Collider>(), Owner.GetComponent<Collider>());
		
		
    }

    private void Update()
    {
		if(TimeActive>0){
			if(Time.time >= (timetemp + TimeActive)){
				Active();
			}
		}
    }

    public void Active()
    {

        if (Effect)
        {
            GameObject obj = (GameObject) Instantiate(Effect, transform.position, transform.rotation);
            Destroy(obj, LifeTimeEffect);
        }

        if (Explosive)
            ExplosionDamage();

		if(DestroyAfterObject){
			DestroyAfterObject.transform.parent = null;
			if(DestroyAfterObject.GetComponent<ParticleSystem>())
				DestroyAfterObject.GetComponent<ParticleSystem>().emissionRate = 0;
			
			Destroy(DestroyAfterObject,DestryAfterDuration);
		}
			
        Destroy(gameObject);
    }

    private void ExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Collider hit = hitColliders[i];
            if (!hit)
                continue;

            if (hit.gameObject.GetComponent<DamageManager>())
            {
                if (hit.gameObject.GetComponent<DamageManager>())
                {
                    hit.gameObject.GetComponent<DamageManager>().ApplyDamage(Damage,Owner);
                }
            }
            if (hit.GetComponent<Rigidbody>())
                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f);
        }

    }

    private void NormalDamage(Collision collision)
    {
        if (collision.gameObject.GetComponent<DamageManager>())
        {
            collision.gameObject.GetComponent<DamageManager>().ApplyDamage(Damage,Owner);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
		if(HitedActive){
        	if (collision.gameObject.tag != "Particle" && collision.gameObject.tag != this.gameObject.tag)
        	{
            	if (!Explosive)
                	NormalDamage(collision);
            	Active();
        	}
		}
    }
}
