using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Mosquito : Enemy
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

        GameObject[] allResources = GameObject.FindGameObjectsWithTag("resource");

        GameObject closestResource = null;

        foreach (GameObject resource in allResources)
        {
            float distanceToResource = Vector3.Distance(transform.position, resource.transform.position);
            if (distanceToResource < closestDistance)
            {
                closestDistance = distanceToResource;

                closestResource = resource;
            }
        }
        
        return closestResource;
    }
}
