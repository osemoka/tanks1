using UnityEngine;
using System.Collections;

public class HardWall : MonoBehaviour
{

    public int wallhp = 3;
    public Sprite[] hardwallsprite;
    public PlayerScript ps;
    //public Bullet_Player bullet_player;



    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Setting up the reference.
        spriteRenderer = GetComponent<SpriteRenderer>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
       // bullet_player = GameObject.FindGameObjectWithTag("Bullet_Player").GetComponent<Bullet_Player>();


    }


    void OnCollisionEnter2D(Collision2D c)
    {
        /*  Debug.Log("jestem Mur i walnelo we mnie: " + c.gameObject.tag.ToString());
          if (c.gameObject.tag.ToString() == "Bullet_Player")
          {
              Destroy(gameObject);
          }
         * */
        if (ps.bulletPower == 2)
        {
            if (c.gameObject.tag.ToString() == "Bullet_Player")
            {
                if (wallhp == 3)
                {
                    spriteRenderer.sprite = hardwallsprite[0];
                    wallhp -= 1;
                }
                else if (wallhp == 2)
                {
                    spriteRenderer.sprite = hardwallsprite[1];
                    wallhp -= 1;
                }
                else if (wallhp == 1)
                {
                    Destroy(gameObject);
                }

            }
        }
    }
}