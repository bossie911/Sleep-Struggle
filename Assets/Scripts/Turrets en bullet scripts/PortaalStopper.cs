using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaalStopper : BaseTurret
{

    public bool turretIsBuffed = false;

    public Transform target;

    public List<GameObject> portalsInRange = new List<GameObject>();

    private float bulletDamage = 30;
    private float buffedBulletDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
        bulletBeginPoint = gameObject.GetComponentInChildren<Transform>();
        range = 5f;
        targetTag = "enemy";
        fireSpeed = 1f;
        fireCounter = 0f;
        BulletPrefab = Resources.Load("Prefabs/Bullet", typeof(GameObject)) as GameObject;
        resourceCost = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        FindPortal();
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

    //Deze functie markeerd de dichtsbijzijnde portal als target
    void FindPortal()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;
        GameObject closestPortal = null;

        //Dit stuk code zet alle portals die in range zijn in de list portalsInRange
        foreach (GameObject enemy in enemies)
        {
            if (enemy.name == "Portal"){
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < range)
                {
                    portalsInRange.Add(enemy);
                }
            }
        }

        //Onderstaande code zoekt welke portal het dichtsbij de portaalstopper is en markeerd deze als closest portal
        foreach (GameObject portal in portalsInRange)
        {
            float distanceToPortal = Vector3.Distance(transform.position, portal.transform.position);
            if (distanceToPortal < closestDistance)
            {
                closestDistance = distanceToPortal;
                closestPortal = portal;
            }
        }

        //Onderstaande code markeerd de closest portal als target
        if (closestPortal != null)
        {
            target = closestPortal.transform;
        }
        else
        {
            target = null;
        }
        portalsInRange.Clear();
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
