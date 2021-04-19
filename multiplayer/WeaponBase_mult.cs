using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DamageBase_mult : NetworkBehaviour {

	public GameObject Effect;
	public float LifeTimeEffect = 3;
	[HideInInspector]
    public GameObject Owner;
    public int Damage = 20;
	public string[] TargetTag = new string[1]{"Enemy"};
}

public class WeaponBase_mult : NetworkBehaviour {
	[HideInInspector]
    public GameObject Owner;
	[HideInInspector]
	public GameObject Target;
    public string[] TargetTag = new string[1]{"Enemy"};
	public bool RigidbodyProjectile;
	public Vector3 TorqueSpeedAxis;
	public GameObject TorqueObject;
}

