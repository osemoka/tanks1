using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    public int wallhp = 3;
    public Sprite[] wallsprite;


    private SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.gameObject.tag.ToString() == "Bullet_Player" || c.gameObject.tag.ToString() == "Bullet_Enemy")
        {
            if (wallhp == 3)
            {
                spriteRenderer.sprite = wallsprite[0];
                wallhp -= 1;
            }
            else if (wallhp == 2)
            {
                spriteRenderer.sprite = wallsprite[1];
                wallhp -= 1;
            }
            else if (wallhp == 1)
            {
                Destroy(gameObject);
            }
           
        }        
    }
}

