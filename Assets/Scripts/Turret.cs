using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;


    public float turretRange = 5f;
    public string targetTag = "enemy";

    public float fireSpeed = 1f;
    private float fireCounter = 0f;

    public GameObject bulletPrefab;
    public Transform bulletBeginPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindTarget();  
        
        if (fireCounter <= 0f)
        {
            Fire();
            fireCounter = 1f / fireSpeed;
        }

        fireCounter -= Time.deltaTime;

    }



    void Fire()
    {
        Debug.Log("FIRE");
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, bulletBeginPoint.position, bulletBeginPoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Find(target);    
        }
    }



    //Functie dat de dichtbijzijnste enemy zoekt en markt als target
    void FindTarget()
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



    //Range drawen
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }

}
