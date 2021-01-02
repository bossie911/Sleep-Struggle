using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamFuel : MonoBehaviour
{
    public float minimumResourceValue = 0f;
    public float currentResourceValue = 50f;

    public float baseGeneration = 1f;
    public float generationDelay = 1f;
    protected float generationTimer;
    public float addedGeneration;

    public Text dreamFuelDisp;

    void Update()
    {
        ResourceGeneration();

        //voor nu displayt het baseGeneration, moet veranderen naar basegeneration + addedGeneration
        //dreamFuelDisp.text = "" + currentResourceValue + "/" + baseGeneration;
        dreamFuelDisp.text = currentResourceValue.ToString();
    }

    //ResourceGeneration manages the amount of DreamFuel that is added per x amount of time.
    //Have to test what the right starting amount is and what the right base generation amount is.
    public void ResourceGeneration()
    {
        generationTimer += Time.deltaTime;

        if(generationTimer >= generationDelay)
        {
            currentResourceValue += baseGeneration;
            Debug.Log(baseGeneration);
            generationTimer = 0f;
        }
    }
}
