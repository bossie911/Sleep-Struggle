using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryCost : MonoBehaviour
{
    public Text factoryCost;

    float cost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cost = PlaceTurret.factoryTotalCost + 5;

        factoryCost.text = cost.ToString();
    }
}
