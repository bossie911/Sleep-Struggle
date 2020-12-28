using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Tilemaps;

public class Turret : BaseTurret
{
<<<<<<< HEAD
    public Transform target;

    List<GameObject> enemiesInRange = new List<GameObject>();

    public float turretRange = 5f;
    public string targetTag = "enemy";

    public float fireSpeed = 1f;
    private float fireCounter = 0f;

    public GameObject bulletPrefab;
    public Transform bulletBeginPoint;

    //De naam van de Tilemap
    public Tilemap fogOfWar;

    // Start is called before the first frame update
    void Start()
=======
    void Awake()
>>>>>>> 06f7f6e19bc34b77c474304dbc53711827080762
    {
        bulletBeginPoint = gameObject.GetComponentInChildren<Transform>();
        range = 5f;
        targetTag = "enemy";
        fireSpeed = 1f;
        fireCounter = 0f;
        BulletPreFab = Resources.Load("Prefabs/Bullet", typeof(GameObject)) as GameObject;
        resourceCost = 50f;
    }

    void FixedUpdate()
    {
        base.FindTarget();
        UpdateFogOfWar();

        if (target != null)
        {
            if (fireCounter <= 0f)
            {
                base.Fire();
                fireCounter = 1f / fireSpeed;
            }
            fireCounter -= Time.deltaTime;
        }
    }

    public override void PayResourceCost(GameObject dreamfuel)
    {
        dreamfuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
    }

    //Truncated
    void FindClosestTarget()
    {    
        //array voor enemy gameobjects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        //Onderstaande code zorgt ervoor dat als de dichtbijzijnste enemy in range is van de turret deze enemy als target wordt gemarkeerd
        if (closestEnemy != null && closestDistance <= range)
        {
            target = closestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

<<<<<<< HEAD
    //functie dat de enemy zoekt die het dicht bij de base is en markt deze als target
    void FindTargetClosestToBase()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        float closestDistanceToBase = Mathf.Infinity;

        //Zet alle enemies die in range zijn in een list
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < turretRange)
            {
                enemiesInRange.Add(enemy);
            }
        }

        //enemy die het dichtsbijzijnde de base is marken als target en daarna de list clearen
        foreach (GameObject enemy in enemiesInRange)
        {
            if (AIMovement.distanceToBase < closestDistanceToBase)
            {
                closestDistanceToBase = AIMovement.distanceToBase;
                target = enemy.transform;
            }

        }
        enemiesInRange.Clear();
    }

    //Range drawen
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }

    //Deze variabele past aan hoeveel vision de shooting turret weghaalt
=======
>>>>>>> 06f7f6e19bc34b77c474304dbc53711827080762
    public int vision = 1;
    
    void UpdateFogOfWar()
    {
<<<<<<< HEAD
        //Een 3d vector op integer punten
        Vector3Int currentTowerTile = fogOfWar.WorldToCell(transform.position);
        //WorldToCell converteert de wereldpositie naar cellpositie 
=======
        Vector3Int currentTowerTile = FogOfWar.WorldToCell(transform.position);
>>>>>>> 06f7f6e19bc34b77c474304dbc53711827080762

        //Deze forloop haalt tiles weg die eromheen staan
        for (int x=-vision; x<= vision - 1; x++)
        {
            for(int y = -vision - 1; y<= vision; y++)
            {
                FogOfWar.SetTile(currentTowerTile + new Vector3Int(x, y, 0), null);
            }    
        }    
    }
}
