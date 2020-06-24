using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{

    private Manager m;

    private Vector3 Player;
    private Vector3 Player2;
    private Vector3 Eagle;

    public DirectionRaycasting2DCollider os;
    private Rigidbody2D rb2d;
    private Animator animator;

    public bool slowed = false;

    public float nextUsage;
    public Rigidbody2D bulletPrefEnemy;
    public int bulletSpeedEnemy = 3;
    public float delayMove = 0.5f;
    public float nextFire = 1f;
    private float bulletCd = 1.0f;
    private bool bulletFired = true;

    public Vector2 dyst;
    public Vector2 dystE;
    public Vector2 dyst2;

    public int obstacle = 4;

    public float zasieg = 0.25f;
    public bool perfectAimTime = true;
    public float nextAim;
    public float delayPerfectAim = 2f;

    public Vector2 tankSpeed = new Vector2(2, 2);

    public bool[] tabColl = new bool[] { false, false, false, false };

    public AudioClip[] audioClip;

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }

    public enum lastDirection
    {
        Left,
        Right,
        Up,
        Down
    };

    public lastDirection dir = lastDirection.Down;

    void Start()
    {

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        os = GetComponent<DirectionRaycasting2DCollider>();
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        if (Application.loadedLevel == 8 || Application.loadedLevel > 9)
            Player2 = GameObject.Find("Player2").transform.position;
        Player = GameObject.Find("Player").transform.position;
        Eagle = GameObject.Find("Eagle").transform.position;
        tabColl[0] = os.collisionUp;
        tabColl[1] = os.collisionRight;
        tabColl[2] = os.collisionDown;
        tabColl[3] = os.collisionLeft;
    }

    public void EnemyMove()
    {
        dyst = new Vector2(Player.x - transform.position.x, Player.y - transform.position.y);
        dystE = new Vector2(Eagle.x - transform.position.x, Eagle.y - transform.position.y);
        dyst2 = new Vector2(Player2.x - transform.position.x, Player2.y - transform.position.y);

        if (Application.loadedLevel == 8 || Application.loadedLevel > 9)
        {
            if ((Mathf.Abs(dyst2.x) + Mathf.Abs(dyst2.y)) < (Mathf.Abs(dyst.x) + Mathf.Abs(dyst.y)))
            {
                dyst = dyst2;
            }
        }
        else
        {
            if ((Mathf.Abs(dystE.x) + Mathf.Abs(dystE.y)) / 1.5f < (Mathf.Abs(dyst.x) + Mathf.Abs(dyst.y)))
            {
                dyst = dystE;
            }
        }

        for (int i = 0; i < tabColl.Length; i++) //wchodzimy tylko jak napotka juz przeszkode
        {
            if (tabColl[i] && obstacle == 4) obstacle = i;
            else if (!tabColl[0] && !tabColl[1] && !tabColl[2] && !tabColl[3] && obstacle != 4)
                obstacle = 4;
        }

        if (obstacle != 4) // && obstacle2 == 4
        {
            switch (obstacle)
            {
                case 0: //gora
                case 2: //dol
                    if (dyst.x > 0)
                    {
                        dir = lastDirection.Right;
                        rb2d.velocity = Vector2.Scale(Vector2.right, tankSpeed);
                        Debug.Log("Velocity  " + rb2d.velocity);
                        animator.SetBool("FacingRight", true);
                        ResetFacesCustom("FacingRight");
                    }
                    else if (dyst.x < 0)
                    {
                        dir = lastDirection.Left;
                        rb2d.velocity = Vector2.Scale(Vector2.left, tankSpeed);
                        animator.SetBool("FacingLeft", true);
                        ResetFacesCustom("FacingLeft");
                    }
                    break;
                case 1: //prawo
                case 3: //lewo
                    if (dyst.y > 0)
                    {
                        dir = lastDirection.Up;
                        rb2d.velocity = Vector2.Scale(Vector2.up, tankSpeed);
                        animator.SetBool("FacingUp", true);
                        ResetFacesCustom("FacingUp");
                    }
                    else if (dyst.y < 0)
                    {
                        dir = lastDirection.Down;
                        rb2d.velocity = Vector2.Scale(Vector2.down, tankSpeed);
                        animator.SetBool("FacingDown", true);
                        ResetFacesCustom("FacingDown");
                    }
                    break;
            }
            return;
        }
        if (obstacle == 4)
        {
            if (dyst.x < 0 && dyst.y < 0) // tu dodamy && zeby tylko woda //czy obie ujemne
            {
                if (dyst.x < dyst.y) //ruch w lewo
                {
                    dir = lastDirection.Left;
                    rb2d.velocity = Vector2.Scale(Vector2.left, tankSpeed);
                    animator.SetBool("FacingLeft", true);
                    ResetFacesCustom("FacingLeft");
                }
                else //ruch w dol
                {
                    dir = lastDirection.Down;
                    rb2d.velocity = Vector2.Scale(Vector2.down, tankSpeed);
                    animator.SetBool("FacingDown", true);
                    ResetFacesCustom("FacingDown");
                }
            }
            else // ruch w prawo i w górę
            {
                if (Mathf.Abs(dyst.x) > Mathf.Abs(dyst.y))//determinuje nam czy mozemy ruszyc sie w prawo
                {
                    if (dyst.x > 0)
                    {
                        dir = lastDirection.Right;
                        rb2d.velocity = Vector2.Scale(Vector2.right, tankSpeed);
                        animator.SetBool("FacingRight", true);
                        ResetFacesCustom("FacingRight");
                    }
                    else if (dyst.x < 0)
                    {
                        dir = lastDirection.Left;
                        rb2d.velocity = Vector2.Scale(Vector2.left, tankSpeed);
                        animator.SetBool("FacingLeft", true);
                        ResetFacesCustom("FacingLeft");
                    }
                }
                else//determinuje nam czy mozemy ruszyc sie w górę
                {
                    if (dyst.y > 0)
                    {
                        dir = lastDirection.Up;
                        rb2d.velocity = Vector2.Scale(Vector2.up, tankSpeed);
                        animator.SetBool("FacingUp", true);
                        ResetFacesCustom("FacingUp");
                    }
                    else if (dyst.y < 0)
                    {
                        dir = lastDirection.Down;
                        rb2d.velocity = Vector2.Scale(Vector2.down, tankSpeed);
                        animator.SetBool("FacingDown", true);
                        ResetFacesCustom("FacingDown");
                    }
                }
            }
        }
    }


    public IEnumerator Slow()
    {
        slowed = true;
        this.tankSpeed = new Vector2(0, 0);
        this.bulletFired = true;
        //Debug.Log("Stopping Enemy Fire");
        yield return StartCoroutine(Wait(8.0f));
        this.bulletFired = false;
        this.tankSpeed = new Vector2(2, 2);
        slowed = false;
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


    void ResetFacesCustom(string custom)
    {
        if (custom != "FacingDown") animator.SetBool("FacingDown", false);
        if (custom != "FacingUp") animator.SetBool("FacingUp", false);
        if (custom != "FacingLeft") animator.SetBool("FacingLeft", false);
        if (custom != "FacingRight") animator.SetBool("FacingRight", false);
    }

    void FixedUpdate()
    {

        if (perfectAimTime)
        {
            Debug.Log("perfectAim");
            if ((tabColl[1] || tabColl[3]) && ((transform.position.y >= (Player.y - zasieg)) && (transform.position.y < (Player.y + zasieg))))
            {
                Debug.Log(tabColl + " TP X " + transform.position.x + "  TP Y " + transform.position.y + " Player X  " + Player.x + " Player Y  " + Player.y);
                if (transform.position.x < Player.x)
                {
                    dir = lastDirection.Right;
                    rb2d.velocity = Vector2.Scale(Vector2.zero, tankSpeed);
                    animator.SetBool("FacingRight", true);
                    ResetFacesCustom("FacingRight");
                }
                else if (transform.position.x > Player.x)
                {
                    dir = lastDirection.Left;
                    rb2d.velocity = Vector2.Scale(Vector2.zero, tankSpeed);
                    animator.SetBool("FacingLeft", true);
                    ResetFacesCustom("FacingLeft");
                }
            }
            else if ((tabColl[0] || tabColl[2]) && ((transform.position.x >= (Player.x - zasieg)) && (transform.position.x < (Player.x + zasieg))))
            {
                Debug.Log(tabColl + " TP X " + transform.position.x + "  TP Y " + transform.position.y + " Player X  " + Player.x + " Player Y  " + Player.y);
                if (transform.position.y < Player.y)
                {
                    dir = lastDirection.Up;
                    rb2d.velocity = Vector2.Scale(Vector2.zero, tankSpeed);
                    animator.SetBool("FacingUp", true);
                    ResetFacesCustom("FacingUp");
                }
                else if (transform.position.y > Player.y)
                {
                    dir = lastDirection.Up;
                    rb2d.velocity = Vector2.Scale(Vector2.zero, tankSpeed);
                    animator.SetBool("FacingDown", true);
                    ResetFacesCustom("FacingDown");
                }
            }
            perfectAimTime = false;
            return;
        }

            Rigidbody2D bulletInstance = new Rigidbody2D();
            Vector3 bullet_pos = new Vector3(0, 0, 0);
            if (!bulletFired)
            {
                switch (dir)
                {
                    case lastDirection.Right:
                        bulletInstance = Instantiate(bulletPrefEnemy, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(bulletSpeedEnemy, 0);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.x += 0.6f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 90f));
                        break;
                    case lastDirection.Left:
                        bulletInstance = Instantiate(bulletPrefEnemy, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(-bulletSpeedEnemy, 0);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.x -= 0.6f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                        break;
                    case lastDirection.Up:
                        bulletInstance = Instantiate(bulletPrefEnemy, transform.position, Quaternion.Euler(new Vector3(180f, 0, 0))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(0, bulletSpeedEnemy);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.y += 0.6f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        break;
                    case lastDirection.Down:
                        bulletInstance = Instantiate(bulletPrefEnemy, transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(0, -bulletSpeedEnemy);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.y -= 0.6f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                        break;
                }

                Destroy(bulletInstance.gameObject, 5);
                bulletFired = true;
        }


        if (Time.time > nextUsage)
        {
            nextUsage = Time.time + delayMove;
            EnemyMove();

        }

        if (Time.time > nextFire && !slowed)
        {
            Debug.Log("TU");
            nextFire = Time.time + bulletCd;
            bulletFired = false;
        }

        if (Time.time > nextAim)
        {
            nextAim = Time.time + delayPerfectAim;
            perfectAimTime = true;
        }

    
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.ToString() == "Bullet_Player")
        {
            AudioSource.PlayClipAtPoint(audioClip[1], transform.position, 1.5F);

            m.score += 2;
            PlayerPrefs.SetInt("Score", m.score);
            Destroy(gameObject);
        }
        if (Application.loadedLevel == 10 && c.gameObject.tag.ToString() == "Bullet_Player2")
        {
            AudioSource.PlayClipAtPoint(audioClip[1], transform.position, 1.5F);

            m.score += 2;
            PlayerPrefs.SetInt("Score", m.score);
            Destroy(gameObject);
        }
        else if (c.gameObject.tag.ToString() == "Bullet_Player2")
        {
            AudioSource.PlayClipAtPoint(audioClip[1], transform.position, 1.5F);

            m.score2 += 2;
            PlayerPrefs.SetInt("Score2", m.score2);
            Destroy(gameObject);
        }

    }
}