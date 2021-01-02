﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;

public class PlaceTurret : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject turretPrefab, factoryPrefab, DreamFuel, mine;
    public Tilemap fogOfWar;
   
    TileManager manager;
    
    static DreamFactorySelector selector;
    private int fow_layer; 

    private Rigidbody ghost; 
    private Rigidbody turretGhost, factoryGhost, mineGhost;

    //Place the turret slightly higher (looks better)
    private static Vector3 currentOffset; 
    static Vector3 turretOffset, factoryOffset, mineOffset;

    public float resourceCost;
    public float factoryCost;

    public float factoryAddedGeneration = 0.5f;
    float factoryBaseCost = 30;
    public float factoryExtraCost;
    public float factoryExtraCostPerFactory = 5;
    public static float factoryTotalCost = 30;
    public int mineCost; 

    void Start()
    {
        turretOffset = new Vector3(0, 0.4f);
        factoryOffset = new Vector3(0, 0.66f);
        mineOffset = new Vector3(0,0);

        turretGhost = GameObject.Find("TurretGhost").GetComponent<Rigidbody>(); 
        factoryGhost = GameObject.Find("FactoryGhost").GetComponent<Rigidbody>();
        mineGhost = GameObject.Find("MineGhost").GetComponent<Rigidbody>(); 
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
        selector = GameObject.Find("DreamFuelPanel").GetComponent<DreamFactorySelector>();

        fow_layer = 1 << 8; 
    }

    void Update()
    {
        switch (selector.SelectedPlaceable)
        {
            case DreamFactorySelector.Placeables.Turret: 
                ghost = turretGhost;
                currentOffset = turretOffset; 
                break;

            case DreamFactorySelector.Placeables.Factory:
                ghost = factoryGhost;
                currentOffset = factoryOffset; 
                break;
            case DreamFactorySelector.Placeables.Mine:
                ghost = mineGhost;
                currentOffset = mineOffset; 
                break; 
        }

        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        TileObject currentTile = manager.GetTileFromPosition(point);

        //Draw ghost on the tile the mouse is currently hovering over
        DrawGhost(point);

        //Make sure that the mouse is not inside the FoW
        if (Input.GetMouseButtonDown(0) && currentTile != null && currentTile.CanPlaceTower()) 
        {
            if (IfCanPlace(ray))
            {
                switch (selector.SelectedPlaceable)
                {
                    case DreamFactorySelector.Placeables.Turret:
                        Place(point, currentTile, turretPrefab, currentOffset);
                        break;

                    case DreamFactorySelector.Placeables.Factory:
                        Place(point, currentTile, factoryPrefab, currentOffset);
                        break;

                    case DreamFactorySelector.Placeables.Mine:
                        Place(point, currentTile, mine, currentOffset);
                        break; 
                }
            }
        }
    }

    private void DrawGhost(Vector3 point)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            ghost.position = tilemap.CellToWorld(tilemap.WorldToCell(point)) + currentOffset;
    }

    private bool IfCanPlace(Ray ray)
    {
        if (!Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, fow_layer) && 
            DreamFuel.GetComponent<DreamFuel>().currentResourceValue > 0 + resourceCost && 
            !EventSystem.current.IsPointerOverGameObject())
            return true;

        return false; 
    }
    
    private void Place(Vector3 point, TileObject currentTile, GameObject towerToPlace, Vector3 offset)
    {
        Vector3 worldPosition = tilemap.CellToWorld(tilemap.WorldToCell(point));

        if (Equals(towerToPlace, turretPrefab))
        {
            GameObject newTurret = Instantiate(turretPrefab, worldPosition + offset, Quaternion.identity);

            //Create all turrets as a child of this gameobj, so the hierarchy doesn't get cluttered
            newTurret.transform.SetParent(this.transform);
            Turret turret = newTurret.GetComponent<Turret>(); 

            //Set reference FoW of the newly created turret
            turret.FogOfWar = fogOfWar;
            turret.PayResourceCost(DreamFuel);
        }

        else if(Equals(towerToPlace, factoryPrefab))
        {
            //Code voor factories die steeds meer kosten
            factoryExtraCost += factoryExtraCostPerFactory;
            factoryTotalCost = factoryBaseCost + factoryExtraCost;

            resourceCost = factoryTotalCost;
            Debug.Log(resourceCost);

            GameObject newFactory = Instantiate(factoryPrefab, worldPosition + offset, Quaternion.identity);

            newFactory.transform.SetParent(this.transform);

            newFactory.GetComponent<DreamFactory>().fogOfWar = fogOfWar;

            DreamFuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
            DreamFuel.GetComponent<DreamFuel>().baseGeneration += 1f;//factoryAddedGeneration;
        }

        else if(Equals(towerToPlace, mine)) {
            resourceCost = mineCost;
            GameObject newMine = Instantiate(mine, worldPosition + offset, Quaternion.identity);
            newMine.transform.SetParent(this.transform);
            DreamFuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
            //DreamFuel.GetComponent<DreamFuel>().baseGeneration += factoryAddedGeneration;
            newMine.GetComponent<Mine>().construct(manager, DreamFuel.GetComponent<DreamFuel>());

        }

        currentTile.TurretPlaced = true;
    }
}