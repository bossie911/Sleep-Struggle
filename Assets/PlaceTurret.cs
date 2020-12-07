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
    public GameObject turret, factory, DreamFuel;
    public Tilemap fogOfWar;

    TileManager manager;
    static DreamFactorySelector selector;
    int count;
    private int fow_layer; 

    private Rigidbody ghost; 
    private Rigidbody turretGhost, factoryGhost;

    //Place the turret slightly higher (looks better)
    private static Vector3 currentOffset; 
    static Vector3 turretOffset, factoryOffset;

    public float resourceCost;

    public float factoryAddedGeneration = 0.5f;
    float factoryBaseCost = 30;
    public float factoryExtraCost;
    public float factoryExtraCostPerFactory = 5;
    public static float factoryTotalCost = 30;

    void Start()
    {
        turretOffset = new Vector3(0, 0.4f);
        factoryOffset = new Vector3(0, 0.66f);

        turretGhost = GameObject.Find("TurretGhost").GetComponent<Rigidbody>(); 
        factoryGhost = GameObject.Find("FactoryGhost").GetComponent<Rigidbody>();
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
        selector = GameObject.Find("ResourcePanel").GetComponent<DreamFactorySelector>();

        fow_layer = 1 << 8; 
    }

    void Update()
    {
        switch (selector.CurrentPlaceable)
        {
            case DreamFactorySelector.Placeables.Turret:
                ghost = turretGhost;
                currentOffset = turretOffset; 
                break;

            case DreamFactorySelector.Placeables.Factory:
                ghost = factoryGhost;
                currentOffset = factoryOffset; 
                break;
        }

        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        TileObject currentTile = manager.GetTileFromPosition(point);

        //Draw ghost on the tile the mouse is currently hovering over
        if (!EventSystem.current.IsPointerOverGameObject())
            ghost.position = tilemap.CellToWorld(tilemap.WorldToCell(point)) + currentOffset;

        //Make sure that the mouse is not inside the FoW
        if (Input.GetMouseButtonDown(0) && currentTile != null && currentTile.CanPlaceTower()) 
        {
            if (!Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, fow_layer) &&
                DreamFuel.GetComponent<DreamFuel>().currentResourceValue > 0 + resourceCost &&
                !EventSystem.current.IsPointerOverGameObject())
            {
                switch (selector.)
                {
                    case DreamFactorySelector.Placeables.Turret:
                        Place(point, currentTile, turret);
                        break;

                    case DreamFactorySelector.Placeables.Factory:
                        Place(point, currentTile, factory);
                        break; 
                }
            }
        }
    }

    public void Place(Vector3 point, TileObject currentTile, GameObject towerToPlace)
    {
        Vector3 worldPosition = tilemap.CellToWorld(tilemap.WorldToCell(point));

        //Get all turrets currently on the field
        count = Resources.FindObjectsOfTypeAll<GameObject>().Count(obj => obj.name == "Turret");

        resourceCost = 0f;
        if (Equals(towerToPlace, turret))
        {
            resourceCost = 50f;
            GameObject newTurret = Instantiate(turret, worldPosition + turretOffset, Quaternion.identity);
            newTurret.name = "Turret";

            //Create all turrets as a child of this gameobj, so the hierarchy doesn't get cluttered
            newTurret.transform.SetParent(this.transform);

            //Set reference FoW of the newly created turret
            newTurret.GetComponent<Turret>().fogOfWar = fogOfWar;
            DreamFuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;

            /*AnalyticsEvent.Custom("TurretsBuild", new Dictionary<string, object>
            {
                {$"(Turret number = {count}", count},
                {"Time_elapsed", Time.timeSinceLevelLoad}
            }); */

        }
        else
        {
            //Code voor factories die steeds meer kosten
            factoryExtraCost += factoryExtraCostPerFactory;
            factoryTotalCost = factoryBaseCost + factoryExtraCost;

            resourceCost = factoryTotalCost;
            Debug.Log(resourceCost);

            GameObject newFactory = Instantiate(factory, worldPosition + factoryOffset, Quaternion.identity);

            newFactory.transform.SetParent(this.transform);

            newFactory.GetComponent<DreamFactory>().fogOfWar = fogOfWar;

            DreamFuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
            DreamFuel.GetComponent<DreamFuel>().baseGeneration += factoryAddedGeneration;

            //TODO
            //Analytics.CustomEvent("FactoriesBuilt", new Dictionary<string, object>
            //{
            //    {$"Turret number = {this.transform.childCount}", this.transform.childCount}
            //});
        }

        currentTile.TurretPlaced = true;
    }
}
