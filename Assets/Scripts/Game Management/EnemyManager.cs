using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject regularEnemy, mosquito, nightmare;
    public WaveObject[] waves;
    int currentWave;

    float timeBetweenRegularEnemies, nextRegularEnemyTime;
    float timeBetweenMosquito, nextMosquitoTime;
    float timeBetweenNightmare, nextNightmareTime;

    float waveTimer;
    bool cooldownActive;

    public Transform[] spawnPoints;
    public Transform parent;
    public Transform target;

    public Text waveDisplay;

    float totalWaveTime, gameEndTimer;
    public Text victoryText;

    public AudioManager audioManager;
    public bool endless;
    bool gameIsOver;


    // Start is called before the first frame update
    void Start()
    {
        startWave();
        AnalyticsResult result = Analytics.CustomEvent("Startlevel", new Dictionary<string, object>
                {{"StartLevel", 1}
        });
        Debug.Log("start:" + result);

        for (int i = 0; i < waves.Length; i++)
        {
            totalWaveTime += waves[i].waveDurationSeconds;
            totalWaveTime += waves[i].cooldownAfterWave;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //checks if the portals are all destroyed
        gameIsOver = true;
        foreach (Transform spawn in spawnPoints)
        {
            if (spawn != null)
            {
                gameIsOver = false;
            }
        }
        if (gameIsOver)
        {
            SceneManager.LoadScene("LevelSelection");
            Debug.Log("game is over");
        }
        
        //checks if the portals are all destroyed
        waveTimer += Time.deltaTime;


        if (waveTimer >= waves[currentWave].waveDurationSeconds)
        {
            cooldownActive = true;
        }

        if (waveTimer >= waves[currentWave].cooldownAfterWave + waves[currentWave].waveDurationSeconds && currentWave < waves.Length)
        {//If the wave is over go to the next one
            if (!endless && currentWave > waves.Length - 1)
            {
                AnalyticsResult result = Analytics.CustomEvent("CompleteLevel", new Dictionary<string, object>
                {{"CompleteLevel", 1}
                });

                Debug.Log("leveldone : " + result);
            }


            currentWave++;
            
            if (endless && currentWave > waves.Length - 1)
            {
                currentWave = waves.Length - 1;
            }
            startWave();
        }


        gameEndTimer += Time.deltaTime;
        if (gameEndTimer > totalWaveTime)
        {
            //victoryText.enabled = true;
        }

        waveDisplay.text = (currentWave + 1).ToString();

        if (!cooldownActive && waveTimer >= nextRegularEnemyTime)
        {
            spawnEnemy(regularEnemy);

            nextRegularEnemyTime += timeBetweenRegularEnemies;
        }

        if (!cooldownActive && waveTimer >= nextMosquitoTime)
        {
            spawnEnemy(mosquito);

            nextMosquitoTime += timeBetweenMosquito;
        }

        if (!cooldownActive && waveTimer >= nextNightmareTime)
        {
            spawnEnemy(nightmare);

            nextNightmareTime += timeBetweenNightmare;
        }
    }

    /// starts a new wave and calculates the values needed
    void startWave()
    {

        waveTimer = 0;
        timeBetweenRegularEnemies = waves[currentWave].waveDurationSeconds / waves[currentWave].amountOfNormalEnemies;
        //calculates how often enemies are spawned
        nextRegularEnemyTime = timeBetweenRegularEnemies;

        timeBetweenMosquito = waves[currentWave].waveDurationSeconds / waves[currentWave].amountOfMosquitos;
        //calculates how often mosquitos are spawned
        nextMosquitoTime = timeBetweenMosquito;

        timeBetweenNightmare = waves[currentWave].waveDurationSeconds / waves[currentWave].amountOfNightmares;
        //calculates how often nightmares are spawned
        nextNightmareTime = timeBetweenNightmare;

        cooldownActive = false;
    }

    /// <summary>
    /// selects a random spawnpoint from the spawnpoints array and spawns an enemy there
    /// </summary>
    /// <param name="enemy">Prefab of the enemy spawned</param>
    void spawnEnemy(GameObject enemy)
    {
        int whereToSpawn = Random.Range(0, spawnPoints.Length);//finds a spawnpoint to spawn an enemy
        GameObject newGuy = Instantiate(enemy, spawnPoints[whereToSpawn].position, Quaternion.identity);//Creates a new enemy
        newGuy.GetComponent<Enemy>().Setup(audioManager);
        newGuy.transform.SetParent(parent);//orders the enemy to avoid cluttering
        newGuy.GetComponent<NavMeshAgent>().SetDestination(target.position);//sets the destination of the enemy
    }
}
