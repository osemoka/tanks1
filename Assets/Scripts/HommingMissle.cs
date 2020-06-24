using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingMissle : MonoBehaviour {

    public float speed = 5;
    public float rotatingSpeed = 1000;
    public GameObject target;
    private BossScript bs;
    private Rigidbody2D rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        bs = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
        float value = Vector3.Cross(point2Target, transform.right).z;
        Debug.Log(value);
        rb.angularVelocity = rotatingSpeed * value;        
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.ToString() == "Player" || c.gameObject.tag.ToString() == "Bullet_Player" || c.CompareTag("Shield"))
            {
                Destroy(this.gameObject);
                bs.missle_fired = false; 
            }
        }
}
