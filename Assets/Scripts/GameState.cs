using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void GoToLevel(int whichLevel)
    {

        switch (whichLevel)
        {
            case 1:
                SceneManager.LoadScene("Level_1");
                break;
            case 2:
                SceneManager.LoadScene("Level_2");
                break;
            case 3:
                SceneManager.LoadScene("Level_3");
                break;
        }
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
