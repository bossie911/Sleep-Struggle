using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totempaal : BaseTurret
{
    public List<GameObject> turretsInRange = new List<GameObject>();

    void Awake()
    {
        range = 5f;
        resourceCost = 100f;
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
            if (Turret.name == "Turret(Clone)")
            {
                Turret.GetComponent<Turret>().turretIsBuffed = true;
            }
            else if (Turret.name == "Portaal stopper" || Turret.name == "Portaal stopper(Clone)")
            {
                Turret.GetComponent<PortaalStopper>().turretIsBuffed = true;
            }
        }
    }

    void ClearList()
    {
        turretsInRange.Clear();
    }
}
