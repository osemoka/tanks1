using UnityEngine;
using System.Collections;

public class FlameSpawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject flame;		// Array of enemy prefabs.
      
    void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }


	void Spawn ()
	{
        Instantiate(flame, transform.position, transform.rotation);     
 	}
}
