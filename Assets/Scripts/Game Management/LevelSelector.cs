using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = new SceneManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToLevel(int whichLevel)
    {

        switch (whichLevel)
        {
            case 1:
            SceneManager.LoadScene("Level_1");
                break;

        }


    }
}
