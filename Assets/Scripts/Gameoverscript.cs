using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameoverscript : MonoBehaviour
{

    public Text finalScoreTxt;

    public int finalScore;

    void Update()
    {
        setHighscore();
    }


    public void Gameover()
    {
        SceneManager.LoadScene("startmenu");
    }

    public void setHighscore()
    {
        finalScore = LifeTracker.finalScore;

        finalScoreTxt.text = "Your Score: " + finalScore;
    }
}