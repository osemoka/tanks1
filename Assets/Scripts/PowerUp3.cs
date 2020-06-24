using UnityEngine;
using System.Collections;

public class PowerUp3 : MonoBehaviour
{

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
            if (upgrade.bulletLevel < 4)
            {
                upgrade.bulletPower++;
                upgrade.bulletLevel++;
            }
            else
                upgrade.bulletPower++;
            Destroy(gameObject);
        }

        if (c.CompareTag("Player2"))
        {
            if (upgrade2.bulletLevel < 4)
            {
                upgrade2.bulletPower++;
                upgrade2.bulletLevel++;
            }
            else
                upgrade2.bulletPower++;
            Destroy(gameObject);
        }
    }

}
