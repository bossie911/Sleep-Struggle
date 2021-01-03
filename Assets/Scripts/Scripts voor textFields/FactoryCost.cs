using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryCost : MonoBehaviour
{
    public Text factoryCost;

    float cost;

    // Update is called once per frame
    void Update()
    {
        cost = PlaceTurret.factoryTotalCost + 5;

        factoryCost.text = cost.ToString();
    }
}
