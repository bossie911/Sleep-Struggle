using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Nightmare : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Sets the destination of the enemy
        GetComponent<NavMeshAgent>().SetDestination(findGameObject().transform.position);
    }

    GameObject findGameObject()
    {
        float closestDistance = Mathf.Infinity;

        GameObject[] allTurrets = GameObject.FindGameObjectsWithTag("turret");

        GameObject closestTurret = null;

        foreach (GameObject resource in allTurrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, resource.transform.position);
            if (distanceToTurret < closestDistance)
            {
                closestDistance = distanceToTurret;

                closestTurret = resource;
            }
        }

        return closestTurret;
    }
}