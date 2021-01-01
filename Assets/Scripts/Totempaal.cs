using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totempaal : BaseTurret
{

    public List<GameObject> turretsInRange = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        range = 5f;
        resourceCost = 100;
        targetTag = "turret";
    }

    // Update is called once per frame
    void Update()
    {
        BuffTurrets();

        ClearList();
    }

    void BuffTurrets()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(targetTag);

        //Add all turrets in range to the list
        foreach (GameObject Turret in turrets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, Turret.transform.position);
            if (distanceToEnemy < range)
            {
                turretsInRange.Add(Turret);
            }
        }
        //Buff turrets in range
        foreach (GameObject Turret in turretsInRange)
        {
            Turret.GetComponent<Turret>().turretIsBuffed = true;
        }
    }


    void ClearList()
    {
        turretsInRange.Clear();
    }
}
