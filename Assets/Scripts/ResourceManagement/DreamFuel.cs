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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ResourceGeneration();

        //voor nu displayt het baseGeneration, moet veranderen naar basegeneration + addedGeneration
        dreamFuelDisp.text = "" + currentResourceValue + "/" + baseGeneration;
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

    }

    public void TurretPlaced()
    {
        //This script gets called when the player places a turret of said type. 
        //The dreamfuel cost has to be subtracted from the currentResourceValue
        //currentResourceValue has to then be updated
    }

    public void DreamFactoryPlaced()
    {
        //This script is different than the TurretPlaced script in the fact that it 
        //also adds an x amount to baseGeneration within the ResourceGeneration Function
        //The dreamfuel cost has to be subtracted from the currentResourceValue
        //currentResourceValue has to then be updated
    }
}
