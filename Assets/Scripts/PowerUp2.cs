using UnityEngine;
using System.Collections;

public class PowerUp2 : MonoBehaviour
{

    private PlayerScript upgrade;
    private Player2Script upgrade2;

    void Start()
    {
        upgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (Application.loadedLevel == 8 || Application.loadedLevel >9)
        upgrade2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
    }

    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.CompareTag("Player"))
        {
            upgrade.bulletSpeed += 1;
            Destroy(gameObject);
        }

        if (c.CompareTag("Player2"))
        {
            upgrade2.bulletSpeed += 1;
            Destroy(gameObject);
        }
    }

}
