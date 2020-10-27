using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform middle;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(middle.position);
    }

    // Update is called once per frame
    void Update()
    {
        //this is not efficient we should make it so that it only updates when the target changes
    }

    
}
