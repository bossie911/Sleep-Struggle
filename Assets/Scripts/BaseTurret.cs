using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseTurret : MonoBehaviour
{
    protected float range;
    protected string targetTag;

    protected float fireSpeed;
    protected float fireCounter; 
    protected float resourceCost; 

    protected Transform bulletBeginPoint;

    private GameObject bulletPrefab;
    private Tilemap fogOfWar;

    public Tilemap FogOfWar
    {
        get { return fogOfWar; }
        set { fogOfWar = value; }
    }

    public GameObject BulletPrefab
    {
        get { return bulletPrefab; }
        protected set { bulletPrefab = value; }
    }

    //Pay the resource cost of a particular turret
    public virtual void PayResourceCost(GameObject dreamfuel)
    {
        var currFuel = dreamfuel.GetComponent<DreamFuel>().currentResourceValue; 

        if (currFuel >= resourceCost)
            dreamfuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
    }

    public float ResourceCost()
    {
        return resourceCost; 
    }

    //Drawn range
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
