using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Mosquito : Enemy
{
    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the destination of the enemy
        GameObject destFound = findGameObject();
        if (destFound != null)
        {
            navMeshAgent.SetDestination(findGameObject().transform.position);
        }
        base.Update();
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
