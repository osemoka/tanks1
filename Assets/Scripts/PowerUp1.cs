using UnityEngine;
using System.Collections;

public class PowerUp1 : MonoBehaviour {

    private PlayerScript upgrade;
    private Player2Script upgrade2;

    void Start()
    {
        upgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (Application.loadedLevel == 8 || Application.loadedLevel > 9)
            upgrade2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
    }

    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.CompareTag("Player"))
        {
            Destroy(gameObject);
            upgrade.speed += 0.5f;
        }

        if (c.CompareTag("Player2"))
        {
            Destroy(gameObject);
            upgrade2.speed += 0.5f;
        }
    }

}
