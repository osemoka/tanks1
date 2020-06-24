using UnityEngine;
using System.Collections;

public class Eagle : MonoBehaviour
{

    private Animator animator;
    private Manager m;
    private AudioSource a;

    void Start()
    {
        animator = GetComponent<Animator>();
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        a = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    IEnumerator Eksplozja()
    {
        animator.SetBool("Dead", true);
        yield return StartCoroutine(Wait(1f));
        Debug.Log("die");
        Destroy(this.gameObject);
        Application.LoadLevel(0);
    }

    IEnumerator Wait(float seconds)
    {
        Debug.Log("wait");

        yield return new WaitForSeconds(seconds);
    }

    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.gameObject.tag.ToString() == "Bullet_Player" || c.gameObject.tag.ToString() == "Bullet_Enemy" || c.gameObject.tag.ToString() == "Enemy")
        {
            a.Play();
            this.BroadcastMessage("Eksplozja");


            if (PlayerPrefs.HasKey("Highscore"))
            {
                if (PlayerPrefs.GetInt("Highscore") < m.score)
                {
                    PlayerPrefs.SetInt("Highscore", m.score);
                }
            }
            else
            {
                PlayerPrefs.SetInt("Highscore", m.score);
            }

            PlayerPrefs.SetInt("Score", m.score);
        } 
    }
}
