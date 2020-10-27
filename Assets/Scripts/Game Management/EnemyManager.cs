using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject regularEnemy;
    public float timeBetweenEnemies;
    float enemiesTimer;
    public Transform[] spawnPoint;
    public Transform parent;
    public Transform middle;
    int whereToSpawn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        enemiesTimer += Time.deltaTime;
        if (enemiesTimer >= timeBetweenEnemies)
        {
            whereToSpawn = Random.Range(0,2);
            enemiesTimer = 0;
            GameObject newGuy = Instantiate(regularEnemy, spawnPoint[whereToSpawn].position, Quaternion.identity);//Creates a new enemy
            newGuy.transform.SetParent(parent);//orders the enemy to avoid cluttering
            newGuy.GetComponent<NavMeshAgent>().SetDestination(middle.position);//sets the destination of the enemy
        }
    }


}
