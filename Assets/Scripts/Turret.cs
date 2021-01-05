using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Tilemaps;

public class Turret : BaseTurret
{
    public bool turretIsBuffed = false;
    bool turretWasBuffed;

    public Transform target;

    public List<GameObject> enemiesInRange = new List<GameObject>();

    private float priorityPortal = 10000;

    public float bulletDamage = 10;
    public float buffedBulletDamage = 15;

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
        BuffedCheck();

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

        if(!turretWasBuffed && turretIsBuffed){
            GetComponent<ParticleSystem>().Play();
        }
        turretWasBuffed = turretIsBuffed;
    }

    //Fire a bullet
    public void Fire()
    {
        GameObject _bullet = (GameObject)Instantiate(BulletPrefab, bulletBeginPoint.position, bulletBeginPoint.rotation);
        Bullet bullet = _bullet.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.BulletDamage(bulletDamage, turretIsBuffed);
            bullet.Find(target);
        }
    }

    public void BuffedCheck()
    {
        if (turretIsBuffed == true)
        {
            bulletDamage = buffedBulletDamage;

        }
    }
}
