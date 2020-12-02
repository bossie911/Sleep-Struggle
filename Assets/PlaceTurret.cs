using System;
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
    static DreamFactorySelector script;
    int count;
    public int fow_layer; 

    private Rigidbody ghost; 
    private Rigidbody turretGhost, factoryGhost;

    //Place the turret slightly higher (looks better)
    static Vector3 offset; 
    static Vector3 factoryOffset;

    public float resourceCost;

    public float factoryAddedGeneration = 0.5f;
    float factoryBaseCost = 30;
    public float factoryExtraCost;
    public float factoryExtraCostPerFactory = 5;
    public static float factoryTotalCost = 30;

    void Start()
    {
        offset = new Vector3(0, 0.4f);
        factoryOffset = new Vector3(0, 0.66f);

        turretGhost = GameObject.Find("TurretGhost").GetComponent<Rigidbody>(); 
        factoryGhost = GameObject.Find("FactoryGhost").GetComponent<Rigidbody>();
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
        script = GameObject.Find("ResourcePanel").GetComponent<DreamFactorySelector>();

        fow_layer = 1 << 8; 
    }

    void Update()
    {
        ghost = script.PlaceableType ? factoryGhost : turretGhost;

        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        //Draw ghost on the tile the mouse is currently hovering over
        if (!EventSystem.current.IsPointerOverGameObject())
            ghost.position = tilemap.CellToWorld(tilemap.WorldToCell(point)) + (script.PlaceableType ? factoryOffset : offset);
        
        TileObject currentTile = manager.GetTileFromPosition(point);

        if (!Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, fow_layer)) 
        {
            if (Input.GetMouseButtonDown(0) && currentTile != null && currentTile.CanPlaceTower() &&
                DreamFuel.GetComponent<DreamFuel>().currentResourceValue > 0 + resourceCost && !EventSystem.current.IsPointerOverGameObject())

                if (script.PlaceableType) Place(point, currentTile, factory);

                else Place(point, currentTile, turret);
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
            GameObject newTurret = Instantiate(turret, worldPosition + offset, Quaternion.identity);
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
