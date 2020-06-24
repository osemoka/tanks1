using UnityEngine;
using System.Collections;

public class BulletPlayer : MonoBehaviour
{
    private PlayerScript ps;
    private float bpower;

    void OnEnable()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        bpower = ps.bulletPower;
    }

    void OnDisable()
    {
        CancelInvoke("Die");
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("MetalWalls"))
        {
            Destroy(this.gameObject);
        }
        if (bpower != 1 && (c.CompareTag("Enemy") || c.CompareTag("Bullet_Enemy") || c.CompareTag("Mur")))
        {
            bpower--;
        }
        else if (bpower == 1)
        Destroy(this.gameObject);
    }
}