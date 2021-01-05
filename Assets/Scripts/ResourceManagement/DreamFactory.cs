using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DreamFactory : BaseTurret
{
    void Awake()
    {
        turretHP = 30f; 
    }

    public void increaseProduction(DreamFuel fuel)
    {
        fuel.currentResourceValue -= resourceCost;
        fuel.baseGeneration += 1f; //factoryAddedGeneration;
    }
}
