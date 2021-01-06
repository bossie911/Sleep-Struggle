using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BaseTurret : MonoBehaviour
{
    protected float range;
    protected string targetTag;

    protected float fireSpeed;
    protected float fireCounter;
    protected float resourceCost;

    protected float turretHP;
    protected float enemyDamageOnTurret = 10f;

    protected Transform bulletBeginPoint;
    protected bool isBase;

    public DreamFuel dreamFuel;

    private GameObject bulletPrefab;
    private Tilemap fogOfWar;

    TileObject myTile;
    public Tilemap FogOfWar
    {
        get { return fogOfWar; }
        set { fogOfWar = value; }
    }

    public float TurretHP()
    {
        return turretHP; 
    }

    public void Setup(TileObject _myTile, DreamFuel _fuel)
    {
        myTile = _myTile;
        dreamFuel = _fuel;
    }

    public GameObject BulletPrefab
    {
        get { return bulletPrefab; }
        protected set { bulletPrefab = value; }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("enemy"))
        {
            turretHP -= (enemyDamageOnTurret * Time.deltaTime);
        }
        if (turretHP <= 0)
        {
            //The base is a tile, so it can't have a turret on it 
            if (!isBase)
                myTile.TurretPlaced = false;

            if (isBase)
            {
                SceneManager.LoadScene("GameOverScreen");
                turretHP = 100f;
            }

            Destroy(this.gameObject);
        }
    }

    //Pay the resource cost of a particular turret
    public virtual void PayResourceCost()
    {
        var currFuel = dreamFuel.GetComponent<DreamFuel>().currentResourceValue;

        if (currFuel >= resourceCost)
            dreamFuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
    }

    //Overloaded method for the DreamFactory
    public virtual void PayResourceCost(float cost)
    {
        var currFuel = dreamFuel.GetComponent<DreamFuel>().currentResourceValue;

        if (currFuel >= resourceCost)
            dreamFuel.GetComponent<DreamFuel>().currentResourceValue -= cost;
    }

    //Drawn range
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
