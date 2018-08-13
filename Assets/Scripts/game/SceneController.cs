using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadIntro()
    {
        SceneManager.LoadScene("Intro");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
