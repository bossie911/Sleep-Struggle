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

    private Base middleTower;
    private float baseHP; 

    void Start()
    {
        health.fillAmount = 1f;
        middleTower = GameObject.Find("MiddleTileLocation").GetComponent<Base>();         
    }

    void Update()
    {
        baseHP = middleTower.TurretHP(); 
        healthGroup.alpha = health.fillAmount < 1f ? 1 : 0;                   
        health.fillAmount = baseHP / 100;
    }
}