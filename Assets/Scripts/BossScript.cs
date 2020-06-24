using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public Rigidbody2D rb2d;
    private Vector2 Player;
    private Manager m;

    //public Rigidbody2D HommingMissle;
    private Animator animator;


    public Rigidbody2D bullet_pref_boss;
    public int bullet_speed_boss = 2;
    private float bullet_cd = 1.0f;
    public float nextFire_bullet = 1f;
    public bool bullet_fired = false;
    public float nextUsage_bullet;
    public float delay_bullet = 0.5f;

    public Rigidbody2D missle_pref_boss;
    public bool missle_fired = false;
    public bool missle_right = true;

    public int hp = 3;

    public AudioClip[] audioClip;

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }



    public float dyst;

    public void boss_move()
    {

            dyst = Player.x - transform.position.x;
                 
            if (dyst > 0)
            {
                rb2d.velocity = Vector2.right + Vector2.right;
            }
            else if (dyst < 0)
            {
                rb2d.velocity = Vector2.left + Vector2.left;
            }               
    }

	// Use this for initialization
	void Start () 
    
    {
        animator = GetComponent<Animator>();
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
		Player = GameObject.Find("Player").transform.position;
	}

    void FixedUpdate()
    {
        Rigidbody2D bulletInstance = new Rigidbody2D();
        Rigidbody2D missleInstance = new Rigidbody2D();

        Vector3 bullet_pos = new Vector3(0, 0, 0);
        Vector3 missle_pos = new Vector3(0, 0, 0);


        if (!bullet_fired)
        {
            bulletInstance = Instantiate(bullet_pref_boss, transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
            bulletInstance.velocity = new Vector2(0, -bullet_speed_boss);
            bullet_pos = bulletInstance.transform.position;
            bullet_pos.y -= 1.2f;
            bulletInstance.transform.position = bullet_pos;
            bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));
        }
        bullet_fired = true;
        
        if (Time.time > nextUsage_bullet)
        {           
            nextUsage_bullet = Time.time + delay_bullet;
            boss_move();            
        }

        if (Time.time > nextFire_bullet)
        {
            nextFire_bullet = Time.time + bullet_cd;
            bullet_fired = false;
        }

        if (!missle_fired)
        {
            if (missle_right)
            {
                missleInstance = Instantiate(missle_pref_boss, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            }
            else
            {
                missleInstance = Instantiate(missle_pref_boss, transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
            }
            PlaySound(0);
            missleInstance.velocity = new Vector2(0, -1);
            missle_pos = missleInstance.transform.position;
            missle_pos.y -= 1.2f;
            missleInstance.transform.position = missle_pos;
            //missleInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            missle_fired = true;
            missle_right = !missle_right;
        }
    }


    IEnumerator Eksplozja()
    {
        
        yield return StartCoroutine(Wait(1.0f));
        Destroy(this.gameObject);
    }

    IEnumerator Wait(float seconds)
    {
        Debug.Log("wait");

        yield return new WaitForSeconds(seconds);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        //Debug.Log("CollEnter");
        if (c.gameObject.tag.ToString() == "Bullet_Player")
        {
            if (hp != 1)
            {
                hp--;
                PlaySound(1);
            }
            else
            {
                animator.SetBool("Dead", true);
                PlaySound(2);
                this.BroadcastMessage("Eksplozja");
                m.score += 50;
                PlayerPrefs.SetInt("Score", m.score);
                //tu bym chciał jakoś poczekać i wybuchnąć go
                //Application.LoadLevel(4);
                //Destroy(gameObject);
            }
        }

    }
}
