/// <summary>
/// Spawner. this scripts just a spawner object.
/// </summary>
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	private Transform Objectman;// object to spawn 
	private float timeSpawn = 0;
	private int timeSpawnMax;
	private float enemyCount = 0;
	private int radiun;

	private void Start ()
	{
		if (GetComponent<Renderer>())
			GetComponent<Renderer>().enabled = false;

	}

	private void Update ()
	{
		// find the spawned objects
		GameObject[] gos = GameObject.FindGameObjectsWithTag (Objectman.tag);
		timeSpawn += 1;
		if (gos.Length < enemyCount) {
			int timespawnmax = timeSpawnMax;
			if (timespawnmax <= 0) {
				timespawnmax = 10;
			}
			if (timeSpawn >= timespawnmax) {
				GameObject enemyCreated =
                    (GameObject)
					Instantiate (Objectman.GetComponent<GameObject>(),
                                transform.position +
                                new Vector3 (Random.Range (-radiun, radiun), 20, Random.Range (-radiun, radiun)),
                                Quaternion.identity);

				enemyCreated.transform.localScale = new Vector3 (Random.Range (5, 20), enemyCreated.transform.localScale.x,
                                                                enemyCreated.transform.localScale.x);

				timeSpawn = 0;

			}
		}

	}

}
