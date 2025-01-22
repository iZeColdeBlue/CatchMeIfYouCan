using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeTracker : MonoBehaviour
{
    public int currentLifes;
    public static int finalScore = 0;

    public GameObject[] hearts;

    public bool lostLife = false;

    public AudioPlayer audioPlayer;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        currentLifes = hearts.Length;
        audioPlayer = FindObjectOfType<AudioPlayer>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLifes == 0)
        {
            SceneManager.LoadScene("Game over");
            audioPlayer.playDeath();
            finalScore = playerController.counter;
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
