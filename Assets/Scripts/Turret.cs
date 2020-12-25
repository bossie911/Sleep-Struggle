using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Tilemaps;

public class Turret : BaseTurret
{
    void Awake()
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
