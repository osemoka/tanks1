using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int power;

    void OnEnable()
    {
        //Send the bullet "forward"
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.ToString() == "Bullet_Player")
        {
            if (power != 1)
            {
                power--;
            }
            else
                Destroy(this.gameObject);
        }
        else
            Destroy(this.gameObject);

    }
}