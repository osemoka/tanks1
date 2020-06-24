using UnityEngine;
using System.Collections;

public class PowerUp4 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player") || c.CompareTag("Player2"))
        {
            Destroy(gameObject);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.BroadcastMessage("Slow");
            }
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
            foreach (GameObject spawner in spawners)
            {
                spawner.BroadcastMessage("Delay");
            }
        }
    }
}
