using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialTextData
{
    public float timeVisible;
    public GameObject tutorial;
    Text text;

    public void Setup()
    {
        text = tutorial.GetComponent<Text>();
    }

    public void EnableText(bool enable)
    {
        if (text == null) return; 

        if (enable)
            text.enabled = true;
        
        else
            text.enabled = false;
        
    }
}
