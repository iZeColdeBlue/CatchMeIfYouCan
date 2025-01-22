using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeTracker : MonoBehaviour
{
    public int currentLifes;
    public GameObject[] hearts;

    public bool lostLife = false;

    public AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentLifes = hearts.Length;
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLifes == 0)
        {
            SceneManager.LoadScene("Game over");
            audioPlayer.playDeath();
        }
    }

    public void clearLife()
    {
        if (currentLifes > 0)
        {
            hearts[currentLifes - 1].SetActive(false);
            currentLifes--;
            audioPlayer.playMiss();

            lostLife = true;
        }
    }
}
