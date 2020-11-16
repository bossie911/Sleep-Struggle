using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    public GameObject regularEnemy;
    public WaveObject[] waves;
    int currentWave;
    float timeBetweenEnemies, nextEnemyTime;
    float waveTimer;
    bool cooldownActive;

    public Transform[] spawnPoints;
    public Transform parent;
    public Transform middle;

    // Start is called before the first frame update
    void Start()
    {
        startWave();
    }

    // Update is called once per frame
    void Update()
    {
        waveTimer += Time.deltaTime;

        if (waveTimer >= waves[currentWave].waveDurationSeconds)
        {
            cooldownActive = true;
        }

        if (waveTimer >= waves[currentWave].cooldownAfterWave + waves[currentWave].waveDurationSeconds && currentWave < waves.Length -1)
        {
            currentWave++;
            startWave();
        }


        if (!cooldownActive && waveTimer >= nextEnemyTime)
        {
            spawnRegularEnemy();
            nextEnemyTime += timeBetweenEnemies;
        }
    }

    // starts a new wave and calculates the values needed
    void startWave()
    {
        waveTimer = 0;
        timeBetweenEnemies = waves[currentWave].waveDurationSeconds / waves[currentWave].amountOfNormalEnemies;
        nextEnemyTime = timeBetweenEnemies;
        cooldownActive = false;
    }

    void spawnRegularEnemy()
    {
        int whereToSpawn = Random.Range(0, spawnPoints.Length);
        GameObject newGuy = Instantiate(regularEnemy, spawnPoints[whereToSpawn].position, Quaternion.identity);//Creates a new enemy
        newGuy.transform.SetParent(parent);//orders the enemy to avoid cluttering
        newGuy.GetComponent<NavMeshAgent>().SetDestination(middle.position);//sets the destination of the enemy
    }


}
