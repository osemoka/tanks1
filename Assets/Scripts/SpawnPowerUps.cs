using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour {

    public float spawnTime = 5f;        
    public float spawnDelay = 3f;       
    public GameObject[] powerUps;		
    public Vector2[] groundPosition;

 
    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }
    public float wymiar_x;
    public float wymiar_y;
    public float offset_planszy_x;
    public float offset_planszy_y;
    void Spawn()
    {
        float cosX = Random.Range(0, wymiar_x);
        float cosY = Random.Range(0, wymiar_y);
        cosX = cosX - offset_planszy_x;
        cosY = cosY - offset_planszy_y;

        Vector2 range = new Vector2(cosX,cosY);
        int powerUpIndex = Random.Range(0, powerUps.Length);

        Instantiate(powerUps[powerUpIndex], range, transform.rotation);
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
