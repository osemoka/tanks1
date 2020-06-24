using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    //public AudioSource source;
    public GameObject PauseUI;
    private Manager m;
    public AudioClip[] audioClip;
    private Fade1 f;

    public GameObject mode;
    public GameObject mode2;

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

    void Start()
    {
        f = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade1>();
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        //mode = GameObject.FindGameObjectWithTag("Mode");
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        //pojawia sie 2 panel
        mode.SetActive(true);
        PauseUI.gameObject.SetActive(false);
    }
    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("Highscore");
        m.highScore = 0;
        PauseUI.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(true);
    }
    public void ResetSurvivalHighscore()
    {
        PlayerPrefs.DeleteKey("HighscoreSurvival");
        m.highScoreSurvival = 0;
        PauseUI.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(true);
    }

    public void PlayArcade()
    {
        f.fadeOn();
    }

    public void PlayVS()
    {
        Application.LoadLevel(8);
    }

    public void ChooseSurvival()
    {
        mode2.SetActive(true);
    }

    public void S1()
    {
        Application.LoadLevel(9);
    }

    public void S2()
    {
        Application.LoadLevel(10);
    }

    public void S3()
    {
        Application.LoadLevel(11);
    }

}

