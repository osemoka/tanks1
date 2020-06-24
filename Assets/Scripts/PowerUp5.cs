using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp5 : MonoBehaviour {

    private PlayerScript upgrade;
    private Player2Script upgrade2;

    void Start()
    {
        upgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (Application.loadedLevel == 8 || Application.loadedLevel > 9)
        upgrade2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
    }
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.CompareTag("Player"))
        {
            upgrade.lives += 1;
            Destroy(gameObject);
        }

        if (c.CompareTag("Player2"))
        {
            upgrade2.lives2 += 1;
            Destroy(gameObject);
        }
    }
}
