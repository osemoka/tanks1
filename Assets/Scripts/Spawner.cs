using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		
	public float spawnDelay = 3f;		
	public GameObject[] enemies;		
    public int enemyCount = 0;
    public int maxEnemies = 8;

    public int[] iloscEnemy;
    
    void Start ()
	{
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }


	void Spawn ()
	{
        if (enemyCount < iloscEnemy[0])
        {
            if ( enemyCount < maxEnemies)
                Instantiate(enemies[0], transform.position, transform.rotation);
            enemyCount++;
        }
        else if (enemyCount < iloscEnemy[0] + iloscEnemy[1])
        {
            if ( enemyCount < maxEnemies)
                Instantiate(enemies[1], transform.position, transform.rotation);
            enemyCount++;
        }
        else if (enemyCount < iloscEnemy[0] + iloscEnemy[1] + iloscEnemy[2])
        {
            if (enemyCount < maxEnemies)
                Instantiate(enemies[2], transform.position, transform.rotation);
            enemyCount++;
        }
 
	}

    public IEnumerator Delay()
    {
        Debug.Log("Stopping spawners");
        CancelInvoke("Spawn");
        yield return StartCoroutine(Wait(8.0f));
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
