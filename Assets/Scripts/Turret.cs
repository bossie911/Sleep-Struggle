using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Tilemaps;

public class Turret : BaseTurret
{
    public Transform target;

    public List<GameObject> enemiesInRange = new List<GameObject>();

    private float priorityPortal = 10000;

    void Awake()
    {
        bulletBeginPoint = gameObject.GetComponentInChildren<Transform>();
        range = 5f;
        targetTag = "enemy";
        fireSpeed = 1f;
        fireCounter = 0f;
        BulletPrefab = Resources.Load("Prefabs/Bullet", typeof(GameObject)) as GameObject;
        resourceCost = 50f;
    }

    void Update()
    {
        FindTarget();
        UpdateFogOfWar();

        if (target != null)
        {
            if (fireCounter <= 0f)
            {
                Fire();
                fireCounter = 1f / fireSpeed;
            }
            fireCounter -= Time.deltaTime;
        }
    }

    //finds enemies closest to the base, and add them to a list. 
    public void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        float closestDistanceToBase = Mathf.Infinity;

        //Add all enemies in range to the list
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < range)
            {
                enemiesInRange.Add(enemy);
            }
        }

        //Mark enemies closest to the base as 'target' and clear the list. 
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy.gameObject.name == "Portal")
            {
                if(priorityPortal < closestDistanceToBase)
                {
                    closestDistanceToBase = priorityPortal;
                    target = enemy.transform;
                }
            }
            else if (AIMovement.distanceToBase < closestDistanceToBase)
            {
                closestDistanceToBase = AIMovement.distanceToBase;
                target = enemy.transform;
            }
        }
        enemiesInRange.Clear();
    }

    //Fire a bullet
    public void Fire()
    {
        GameObject bulletB = (GameObject)Instantiate(BulletPrefab, bulletBeginPoint.position, bulletBeginPoint.rotation);
        Bullet bullet = bulletB.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Find(target);
        }
    }


    public override void PayResourceCost(GameObject dreamfuel)
    {
        dreamfuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
    }

 
    public int vision = 1;
    void UpdateFogOfWar()
    {
        Vector3Int currentTowerTile = FogOfWar.WorldToCell(transform.position);

        //Clear the surrounding tiles
        for(int x=-vision; x<= vision - 1; x++)
        {
            for(int y = -vision - 1; y<= vision; y++)
            {
                FogOfWar.SetTile(currentTowerTile + new Vector3Int(x, y, 0), null);
            }    
        }    
    }
}
