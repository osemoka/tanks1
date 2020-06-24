using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenScenes : MonoBehaviour {

    private Fade1 f;
    private Manager m;
    public GameObject PauseUI;
    public AudioClip[] audioClip;

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
        Debug.Log("Start Between Scenes  " + f.i +" "+ m.score);

        if (m.score == 10)
        {//tu może być też ładniejszy obrazek...
            m.LevelText.text = "Congratulations! Level 1 Complete\n\t\t\t Start Next Level?";
        }
        else if (m.score == 39)
        {
            m.LevelText.text = "Congratulations! Level 2 Complete\n\t\t\t Start Next Level?";
        }
        else if (m.score == 84)
        {
            m.LevelText.text = "Congratulations! Level 3 Complete\n\t\t\t Start Next Level?";
        }
        else if (m.score == 188)
        {
            m.LevelText.text = "Congratulations! Level 4 Complete\n\t\t\t Start Next Level?";
        }
        else if (m.score == 262)
        {
            m.LevelText.text = "\t\t\t\t\t\tTHE END\n\t\t\t  CONGRATULATIONS!";
        }

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

    public void Go()
    {
        //to jest z dupy 
        //Application.LoadLevel(3);
        f.fadeOn();
        Debug.Log("jestem za waitnload");
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

    void SaveScore()
    {
        PlayerPrefs.SetInt("Score", m.score);
    }

}
