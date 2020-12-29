using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Analytics;

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

    public Text waveDisplay;

    float totalWaveTime, gameEndTimer;
    public Text victoryText;


    // Start is called before the first frame update
    void Start()
    {
        startWave();
        AnalyticsResult result = Analytics.CustomEvent("Startlevel", new Dictionary<string, object>
                {{"StartLevel", 1}
        });
        Debug.Log("start:" + result);

        for (int i = 0; i < waves.Length; i++) {
            totalWaveTime += waves[i].waveDurationSeconds;
            totalWaveTime += waves[i].cooldownAfterWave;
        }
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
            if (currentWave > waves.Length - 1) {
                AnalyticsResult result = Analytics.CustomEvent("CompleteLevel", new Dictionary<string, object> 
                {{"CompleteLevel", 1}
                });

                Debug.Log("leveldone : " + result);
            }
            currentWave++;
            startWave();
        }

        gameEndTimer += Time.deltaTime;
        if (gameEndTimer > totalWaveTime) {
            //victoryText.enabled = true;
        }

        waveDisplay.text = (currentWave + 1).ToString();

        if (!cooldownActive && waveTimer >= nextEnemyTime)
        {
            spawnRegularEnemy();
            nextEnemyTime += timeBetweenEnemies;
        }
    }

    /// starts a new wave and calculates the values needed
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
