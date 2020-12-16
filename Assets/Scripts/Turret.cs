using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Tilemaps;

public class Turret : MonoBehaviour
{
    public Transform target;

    List<GameObject> enemiesInRange = new List<GameObject>();

    public float turretRange = 5f;
    public string targetTag = "enemy";

    public float fireSpeed = 1f;
    private float fireCounter = 0f;

    public GameObject bulletPrefab;
    public Transform bulletBeginPoint;

    public Tilemap fogOfWar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //FindClosestTarget();
        FindTargetClosestToBase();
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


    //Vuur functie die een bullet maakt en een target er aan mee geeft
    void Fire()
    {
        GameObject bulletB = (GameObject)Instantiate(bulletPrefab, bulletBeginPoint.position, bulletBeginPoint.rotation);
        Bullet bullet = bulletB.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Find(target);    
        }
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
        if (closestEnemy != null && closestDistance <= turretRange)
        {
            target = closestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

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

    public int vision = 1;
    void UpdateFogOfWar()
    {
        Vector3Int currentTowerTile = fogOfWar.WorldToCell(transform.position);

        //Clear the surrounding tiles
        for(int x=-vision; x<= vision - 1; x++)
        {
            for(int y = -vision - 1; y<= vision; y++)
            {
                fogOfWar.SetTile(currentTowerTile + new Vector3Int(x, y, 0), null);
            }    
        }    
    }
}
