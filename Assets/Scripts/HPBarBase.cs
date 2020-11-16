﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPBarBase : MonoBehaviour
{
    private Slider slider;
    public Image fillimage;

    public static float baseHP = 100;

    BaseStats Castle;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 100;
        Castle = FindObjectOfType<BaseStats>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = baseHP;

        baseHP = Castle.Health;
    }
}
