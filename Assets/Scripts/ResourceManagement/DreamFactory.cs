using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DreamFactory : BaseTurret
{
    float fuelTimer;
    public int resourcesPerSecond;

    void Awake()
    {
        turretHP = 30f; 
    }

    public void increaseProduction(DreamFuel fuel)
    {
        fuel.currentResourceValue -= resourceCost;
        //fuel.baseGeneration += 1f; //factoryAddedGeneration;
    }

    private void Update()
    {
        fuelTimer += Time.deltaTime;
        if (fuelTimer > 1) {
            fuel.currentResourceValue += resourcesPerSecond;
            fuelTimer = 0;
        }
    }
}
