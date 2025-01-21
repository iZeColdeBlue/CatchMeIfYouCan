using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startmenubutton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("box_catching_game");
    }
}
