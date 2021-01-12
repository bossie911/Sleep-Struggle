using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamFuel : MonoBehaviour
{
    // This is a comment! 
    public float minimumResourceValue = 0f;
    public float currentResourceValue = 50f;

    public float baseGeneration = 1f;
    public float generationDelay = 1f;
    private float generationTimer;
    public float addedGeneration;

    public Text dreamFuelDisplay;

    void Update()
    {
        ResourceGeneration();
    }

    //ResourceGeneration manages the amount of DreamFuel that is added per x amount of time.
    //Have to test what the right starting amount is and what the right base generation amount is.
    public void ResourceGeneration()
    {
        generationTimer += Time.deltaTime;

        if(generationTimer >= generationDelay)
        {
            currentResourceValue += baseGeneration;
            generationTimer = 0f;
        }
        
        dreamFuelDisplay.text = currentResourceValue.ToString();
    }
}
