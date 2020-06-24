using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;
    private Manager m;
    public AudioClip[] audioClip;
    private Shield s;
    private Shield2 s2;

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();

    }

    public void OnHover()
    {
        PlaySound(0);
        //source.PlayOneShot(audioClip[0]);
    }
    public void OnClick()
    {
        PlaySound(1);
        //source.PlayOneShot(audioClip[1]);
    }
   

    private bool paused = false;

    void Start()
    {
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        //if(s.enabled)
        s = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        s2 = GameObject.FindGameObjectWithTag("Shield2").GetComponent<Shield2>();

        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!paused)
            {
                s.GetComponent<AudioSource>().Pause();
                s2.GetComponent<AudioSource>().Pause();
            }
            else if (paused)
            {
                s.GetComponent<AudioSource>().Play();
                s2.GetComponent<AudioSource>().Play();
            }
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if(!paused)

        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    //public void TouchPause(bool x)
    //{
    //    if (x)
    //    {
    //        Debug.Log("DAJE JEDEN DO PAUSY");
    //        paused = true;
    //        Time.timeScale = 0;
    //    }
    //}

    public void Resume()
    {

        paused = false;
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainMenu()
    {
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

        SaveScore();
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

   
    void SaveScore()
    {
        PlayerPrefs.SetInt("Score", m.score);
    }

}

