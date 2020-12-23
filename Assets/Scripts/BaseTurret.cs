using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseTurret : MonoBehaviour
{
    protected Transform target;

    protected List<GameObject> enemiesInRange = new List<GameObject>();

    protected float range;
    protected string targetTag;

    protected float fireSpeed;
    protected float fireCounter; 

    protected Transform bulletBeginPoint;

    private GameObject bulletPrefab;
    private Tilemap fogOfWar;

    public Tilemap FogOfWar
    {
        get { return fogOfWar; }
        set { fogOfWar = value; }
    }

    public GameObject BulletPreFab
    {
        get { return bulletPrefab; }
        protected set { bulletPrefab = value; }
    }

    void Start()
    {
        bulletBeginPoint = gameObject.GetComponentInChildren<Transform>(); 
        range = 5f;
        targetTag = "enemy";
        fireSpeed = 1f;
        fireCounter = 0f;
        BulletPreFab = Resources.Load("Prefabs/Bullet", typeof(GameObject)) as GameObject;
    }

    //finds enemies closest to the base, and add them to a list. 
    protected virtual void FindTarget()
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
            if (AIMovement.distanceToBase < closestDistanceToBase)
            {
                closestDistanceToBase = AIMovement.distanceToBase;
                target = enemy.transform;
            }
        }
        enemiesInRange.Clear();
    }

    //Fire a bullet
    protected virtual void Fire()
    {
        GameObject bulletB = (GameObject)Instantiate(bulletPrefab, bulletBeginPoint.position, bulletBeginPoint.rotation);
        Bullet bullet = bulletB.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Find(target);
        }
    }

    //Drawn range
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
