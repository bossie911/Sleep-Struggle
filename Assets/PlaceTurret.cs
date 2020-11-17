using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class PlaceTurret : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject turret, factory, DreamFuel;
    public Tilemap fogOfWar;

    TileManager manager;
    static DreamFactorySelector script;

    private Rigidbody ghost; 
    private Rigidbody turretGhost, factoryGhost;

    //Place the turret slightly higher (looks better)
    static Vector3 offset; 
    static Vector3 factoryOffset;

    public float resourceCost;
    public float factoryAddedGeneration = 0.5f;

    void Start()
    {
        offset = new Vector3(0, 0.4f);
        factoryOffset = new Vector3(0, 0.66f);

        turretGhost = GetComponentInChildren<Rigidbody>();
        factoryGhost = GameObject.Find("FactoryGhost").GetComponent<Rigidbody>();
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
        script = GameObject.Find("ResourcePanel").GetComponent<DreamFactorySelector>();
    }

    void Update()
    {
        ghost = script.PlaceableType ? factoryGhost : turretGhost;

        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Draw ghost on the tile the mouse is currently hovering over
        if (!EventSystem.current.IsPointerOverGameObject())
            ghost.position = tilemap.CellToWorld(tilemap.WorldToCell(point)) + (script.PlaceableType ? factoryOffset : offset);
        
        TileObject currentTile = manager.GetTileFromPosition(point);

        if (Input.GetMouseButtonDown(0) && currentTile != null && currentTile.CanPlaceTurret() && 
            DreamFuel.GetComponent<DreamFuel>().currentResourceValue > 0 + resourceCost && !EventSystem.current.IsPointerOverGameObject())

            if (script.PlaceableType) Place(point, currentTile, factory);
            
            else Place(point, currentTile, turret);
    }

    public void Place(Vector3 point, TileObject currentTile, GameObject towerToPlace)
    {
        Vector3 worldPosition = tilemap.CellToWorld(tilemap.WorldToCell(point));

        resourceCost = 0f;
        if (Equals(towerToPlace, turret))
        {
            resourceCost = 50f;
            GameObject newTurret = Instantiate(turret, worldPosition + offset, Quaternion.identity);

            //Create all turrets as a child of this gameobj, so the hierarchy doesn't get cluttered
            newTurret.transform.SetParent(this.transform);

            //Set reference FoW of the newly created turret
            newTurret.GetComponent<Turret>().fogOfWar = fogOfWar;

            DreamFuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
        }
        else
        {
            resourceCost = 25f;
            GameObject newFactory = Instantiate(factory, worldPosition + factoryOffset, Quaternion.identity);

            newFactory.transform.SetParent(this.transform);

            newFactory.GetComponent<DreamFactory>().fogOfWar = fogOfWar;

            DreamFuel.GetComponent<DreamFuel>().currentResourceValue -= resourceCost;
            DreamFuel.GetComponent<DreamFuel>().baseGeneration += factoryAddedGeneration;
        }

        currentTile.TurretPlaced = true;
    }
}
