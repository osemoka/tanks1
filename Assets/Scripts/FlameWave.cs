using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWave : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("MetalWalls") || c.CompareTag("FlameWave") || c.CompareTag("Shield") || c.CompareTag("Shield2"))
        {
            Destroy(gameObject);
        }
    }

}
