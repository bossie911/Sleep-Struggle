using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("level_1");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void PlaySettings()
    {
        Debug.Log("Settings bestaan nog niet");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
