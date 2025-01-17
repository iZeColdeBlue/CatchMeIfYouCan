using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameoverscript : MonoBehaviour
{
    public void Gameover()
    {
        SceneManager.LoadScene("startmenu");
    }
}