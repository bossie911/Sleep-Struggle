using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPBarBase : MonoBehaviour
{
    [SerializeField]
    private Image health;

    [SerializeField]
    private CanvasGroup healthGroup;

    public static float baseHP = 100f;

    void Start()
    {
        health.fillAmount = 1f; 
    }

    void Update()
    {
        healthGroup.alpha = health.fillAmount < 1f ? 1 : 0; 

        health.fillAmount = baseHP / 100;

        if (baseHP <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
