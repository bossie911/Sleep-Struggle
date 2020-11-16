﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlaceTurret : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject turret, factory;
    public Tilemap fogOfWar;
    
    TileManager manager;
    public DreamFactorySelector script;

    private Rigidbody ghost;

    //Place the turret slightly higher (looks better)
    private Vector3 offset;
    private Vector3 factoryOffset;

    void Start()
    {
        offset = new Vector3(0, 0.4f);
        factoryOffset = new Vector3(0, 0.6f);
        ghost = GetComponentInChildren<Rigidbody>();
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
        script = GameObject.Find("DreamFactorySelector").GetComponent<DreamFactorySelector>();
    }

    void Update()
    {
        //Dit moeten we herschrijven
        if(script.PlaceableType)
        {
            ghost = GameObject.Find("FactoryGhost").GetComponent<Rigidbody>();
        }
        else ghost = GetComponentInChildren<Rigidbody>();
        //tot hier

        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Draw ghost on the tile the mouse is currently hovering over
        ghost.position = tilemap.CellToWorld(tilemap.WorldToCell(point)) + offset;

        TileObject currentTile = manager.GetTileFromPosition(point);

        //Dit moet herschreven worden
        if (Input.GetMouseButtonDown(0) && currentTile != null && currentTile.CanPlaceTurret())
            if (script.PlaceableType == false)
            {
                Place(point, currentTile, turret);
            }
            else
            {
                Place(point, currentTile, factory);
            }    
        //tot hier
    }

    void Place(Vector3 point, TileObject currentTile, GameObject towerToPlace)
    {
        Vector3 worldPosition = tilemap.CellToWorld(tilemap.WorldToCell(point));

        //Dit moeten we herschrijven
        if (towerToPlace == turret)
        {
            GameObject newTurret = Instantiate(turret, worldPosition + offset, Quaternion.identity);

            //Create all turrets as a child of this gameobj, so the hierarchy doesn't get cluttered
            newTurret.transform.SetParent(this.transform);

            //Set reference FoW of the newly created turret
            newTurret.GetComponent<Turret>().fogOfWar = fogOfWar;
        }
        else
        {
            GameObject newFactory = Instantiate(factory, worldPosition + factoryOffset, Quaternion.identity);

            newFactory.transform.SetParent(this.transform);

            newFactory.GetComponent<DreamFactory>().fogOfWar = fogOfWar;
        }
        //tot hier

        currentTile.TurretPlaced = true;
    }
}
