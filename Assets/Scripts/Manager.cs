using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public int score;
    public int highScore = 0;
    public int score2;
    public int highScoreSurvival = 0;

    public Text pointsText;
    public Text HighScoreText;
    public Text LevelText;
    public Text LivesText;

    public Text pointsText2;
    public Text LivesText2;
    public Text HighScoreSurvival;

    private Fade1 f;
    private bool switchS = true;
    public bool switchF = false;
    private PlayerScript ps;
    private Player2Script ps2;

    void Start()
    {
        if (Application.loadedLevel != 0 && Application.loadedLevel != 2)
        {
            ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            if (Application.loadedLevel > 7 && Application.loadedLevel != 9)
            ps2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
        }


        if(Application.loadedLevel != 2)
        {
            switchS = false;
        }

        f = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade1>();

       
        if (PlayerPrefs.HasKey("HighscoreSurvival"))
        {
            highScoreSurvival = PlayerPrefs.GetInt("HighscoreSurvival");
        }


        if (PlayerPrefs.HasKey("Score"))
        {
            if (Application.loadedLevel == 0)
            {
                PlayerPrefs.DeleteKey("Score");
                score = 0;
            }
            else 
            {
                score = PlayerPrefs.GetInt("Score");
            }
        }

        if (PlayerPrefs.HasKey("Score2"))
        {
            if (Application.loadedLevel == 0)
            {
                PlayerPrefs.DeleteKey("Score2");
                score2 = 0;
            }
            else
            {
                score2 = PlayerPrefs.GetInt("Score2");
            }
        }

        if(PlayerPrefs.HasKey("Highscore"))
        {
            highScore = PlayerPrefs.GetInt("Highscore");
        }
    }

    void Update()
    {
        if (Application.loadedLevel != 0 && Application.loadedLevel != 2)
        LivesText.text = ("Lives: " +  ps.lives);
        //Debug.Log("wtf" + switchS + switchF + Application.loadedLevel);
        if (Application.loadedLevel > 7 || Application.loadedLevel==0)
            HighScoreSurvival.text = ("Survival HS: " + highScoreSurvival);
        if (Application.loadedLevel < 8 || Application.loadedLevel==0)
            HighScoreText.text = ("Highscore: " + highScore);

        pointsText.text = ("Points: " + score);
        if (Application.loadedLevel > 7 && Application.loadedLevel != 9)
        {
            LivesText2.text = ("Lives: " + ps2.lives2);
            pointsText2.text = ("Points2 " + score2);
        }
        //tu dodamy ze jezeli nie bedzie oby dwóch playerów to app.load level 0;

        if (score == 10 && !switchS && !switchF && Application.loadedLevel == 1)
        {
            switchF = true;
            f.fadeOn();
        }
        else if (score == 39 && !switchS && !switchF && Application.loadedLevel == 3)
        {
            switchF = false;
            f.fadeOn();
        }
        else if (score == 84 && !switchS && !switchF && Application.loadedLevel == 4)
        {
            switchF = false;
            f.fadeOn();
        }
        else if (score == 188 && !switchS && !switchF && Application.loadedLevel == 5)
        {
            switchF = false;
            f.fadeOn();
        }
        else if (score == 262 && !switchS && !switchF && Application.loadedLevel == 6)
        {
            switchF = false;
            f.fadeOn();
        } 
    }

}