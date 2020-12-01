﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Lennart");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}