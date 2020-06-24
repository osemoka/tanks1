using UnityEngine;
using System.Collections;

public class Player2Script : MonoBehaviour {

    #region facing bools
    public bool FacingRight = false;
    public bool FacingUp = true;
    public bool FacingLeft = false;
    public bool FacingDown = false;
    #endregion
    public Rigidbody2D[] bulletPref;
    public int bulletLevel = 0;
    public int lives2 = 3;
    public float speed = 2f;
    public int bulletSpeed = 1;
    public int bulletPower = 1;
    private Animator animator;
    private Manager m;
    private GameObject rp;

    private Shield2 s2;

    public AudioClip[] audioClip;

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }

    enum lastDirection
    {
        Left,
        Right,
        Up,
        Down
    };

    lastDirection lastDir = lastDirection.Up;
    void Start()
    {
        animator = GetComponent<Animator>();
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        rp = GameObject.FindGameObjectWithTag("RespawnPoint2");
        s2 = GameObject.FindGameObjectWithTag("Shield2").GetComponent<Shield2>();
        this.BroadcastMessage("TemporaryShield");
        s2.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal2");
        float y = Input.GetAxisRaw("Vertical2");
        Vector2 direction = new Vector2(x, y);


        if (x > 0)
        {
            animator.SetBool("FacingRight", true);
            lastDir = lastDirection.Right;
        }
        else if (x < 0)
        {
            animator.SetBool("FacingLeft", true);
            lastDir = lastDirection.Left;
        }
        else if (y > 0 )
        {
            animator.SetBool("FacingUp", true);
            lastDir = lastDirection.Up;
        }
        else if (y < 0 )
        {
            animator.SetBool("FacingDown", true);
            lastDir = lastDirection.Down;
        }
        switch (lastDir)
        {
            case lastDirection.Up:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                ResetFacesCustom("FacingUp");
                break;
            case lastDirection.Down:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 180f));
                ResetFacesCustom("FacingDown");
                break;
            case lastDirection.Right:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
                ResetFacesCustom("FacingRight");
                break;
            case lastDirection.Left:
                transform.rotation = Quaternion.Euler(new Vector3(180f, 0, 90f));
                ResetFacesCustom("FacingLeft");

                break;
            default:
                break;
        }

        if (Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.y) > 0) Move(direction);
        else if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) > 0) Move(direction);
        else
        {
            animator.SetBool("Moving", false);
            ResetFaces();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1_2") && bulletLevel != 4)
        {
            PlaySound(0);
            Rigidbody2D bulletInstance = new Rigidbody2D();
            if (lastDir == lastDirection.Right)
            {
                bulletInstance = Instantiate(bulletPref[bulletLevel], transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(bulletSpeed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x += 0.7f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 90f));

            }
            else if (lastDir == lastDirection.Left)
            {
                bulletInstance = Instantiate(bulletPref[bulletLevel], transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-bulletSpeed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x -= 0.7f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));

            }
            else if (lastDir == lastDirection.Up)
            {
                bulletInstance = Instantiate(bulletPref[bulletLevel], transform.position, Quaternion.Euler(new Vector3(180f, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, bulletSpeed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y += 0.7f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            else if (lastDir == lastDirection.Down)
            {
                bulletInstance = Instantiate(bulletPref[bulletLevel], transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, -bulletSpeed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y -= 0.7f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));

            }
            Destroy(bulletInstance.gameObject, 5);
        }
    }

    void ResetFaces()
    {
        animator.SetBool("FacingDown", false);
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingLeft", false);
        animator.SetBool("FacingRight", false);
    }

    void ResetFacesCustom(string custom)
    {
        if (custom != "FacingDown") animator.SetBool("FacingDown", false);
        if (custom != "FacingUp") animator.SetBool("FacingUp", false);
        if (custom != "FacingLeft") animator.SetBool("FacingLeft", false);
        if (custom != "FacingRight") animator.SetBool("FacingRight", false);
    }

    public void Move(Vector2 direction)
    {
        switch (lastDir)
        {
            case lastDirection.Up:
                direction.x = 0;
                break;
            case lastDirection.Down:
                direction.x = 0;
                break;
            case lastDirection.Right:
                direction.y = 0;
                break;
            case lastDirection.Left:
                direction.y = 0;
                break;
            default:
                break;

        }
        animator.SetBool("Moving", true);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;
        transform.position = pos;
    }

    IEnumerator Eksplozja()
    {
        animator.SetBool("Dead", true);
        this.speed = 0;
        bulletLevel = 4;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        Application.LoadLevel(0);
    }

    IEnumerator LifeDown()
    {
        animator.SetBool("Dead", true);
        this.speed = 0;
        bulletLevel = 4;
        s2.GetComponent<ParticleSystem>().enableEmission = false;
        yield return new WaitForSeconds(1f);
        s2.GetComponent<AudioSource>().Play();
        s2.GetComponent<ParticleSystem>().enableEmission = true;
        animator.SetBool("Dead", false);
        bulletLevel = 0;
        bulletPower = 1;
        speed = 2;
        bulletSpeed = 3;
        this.speed = 2f;
        this.transform.position = rp.transform.position;
    }

    IEnumerator TemporaryShield()
    {
        s2.gameObject.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        s2.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.ToString() == "PowerUp")
        {
            PlaySound(1);
        }

        if (c.gameObject.tag.ToString() == "Bullet_Enemy" || c.gameObject.tag.ToString() == "FlameWave" || c.gameObject.tag.ToString() == "Bullet_Player")
        {
            lives2--;
            PlaySound(2);
            if (lives2 >= 1)
            {
                this.BroadcastMessage("LifeDown");
                this.BroadcastMessage("TemporaryShield");
            }
            if (lives2 == 0)
            {
                this.BroadcastMessage("Eksplozja");
            }
            if (Application.loadedLevel < 7)
            {
                if (PlayerPrefs.HasKey("Highscore"))
                {
                    if (PlayerPrefs.GetInt("Highscore") < m.score2)
                    {
                        PlayerPrefs.SetInt("Highscore", m.score2);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("Highscore", m.score2);
                }

                PlayerPrefs.SetInt("Score", m.score2);
            }
            else if (Application.loadedLevel > 8)
            {
                if (PlayerPrefs.HasKey("HighscoreSurvival"))
                {
                    if (PlayerPrefs.GetInt("HighscoreSurvival") < m.score2)
                    {
                        PlayerPrefs.SetInt("HighscoreSurvival", m.score2);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("HighscoreSurvival", m.score);
                }
            }
        }
    }
}

