using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private Scene[] scenes;
    private int index;


    private void Start()
    {
        setIndex();
    }

    public void loadNextScene()
    {
        index++;
        SceneManager.LoadSceneAsync(index);
    }

    public void loadMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void restart()
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void pause()
    {
        if (Time.timeScale > 0) Time.timeScale = 0;

        else Time.timeScale = 1;
    }

    private void setIndex()
    {
        index = SceneManager.GetActiveScene().buildIndex;
    }
}
