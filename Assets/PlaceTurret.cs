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
    public GameObject turretPrefab, factoryPrefab, Mine, Totem, Candle;
    public Tilemap fogOfWar;
    public AudioManager audioManager; 
    
    TileManager manager;
    DreamFuel dreamFuel; 

    static DreamFactorySelector selector;

    private Rigidbody ghost, turretGhost, factoryGhost, mineGhost, totemGhost, candleGhost;

    //Place the turret slightly higher (looks better)
    private static Vector3 currentOffset; 
    static Vector3 turretOffset, factoryOffset, mineOffset;

    //Default selected turret is the dreamcatcher, which cost 50
    public float resourceCost = 50f;
    private int fow_layer;

    public float factoryAddedGeneration = 0.5f;
    float factoryBaseCost = 30;
    public float factoryExtraCost;
    public float factoryExtraCostPerFactory = 5;
    public static float factoryTotalCost = 30;
    public int mineCost; 

    void Start()
    {
        turretOffset = new Vector3(0, 0.4f);

        turretGhost = GameObject.Find("TurretGhost").GetComponent<Rigidbody>(); 
        factoryGhost = GameObject.Find("FactoryGhost").GetComponent<Rigidbody>();
        mineGhost = GameObject.Find("MineGhost").GetComponent<Rigidbody>();
        candleGhost = GameObject.Find("CandleGhost").GetComponent<Rigidbody>();
        totemGhost = GameObject.Find("TotemGhost").GetComponent<Rigidbody>(); 

        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
        selector = GameObject.Find("DreamFuelPanel").GetComponent<DreamFactorySelector>();
        dreamFuel = GameObject.Find("DreamFuelPanel").GetComponent<DreamFuel>(); 

        fow_layer = 1 << 8; 
    }

    void Update()
    {
        switch (selector.SelectedPlaceable)
        {
            case DreamFactorySelector.Placeables.Turret: 
                ghost = turretGhost;
                currentOffset = turretOffset;
                resourceCost = 50f;
                break;

            case DreamFactorySelector.Placeables.Factory:
                ghost = factoryGhost;
                currentOffset = factoryOffset;
                break;

            case DreamFactorySelector.Placeables.Mine:
                ghost = mineGhost;
                break;

            case DreamFactorySelector.Placeables.Totem:
                ghost = totemGhost;
                resourceCost = 100f; 
                break;

            case DreamFactorySelector.Placeables.Candle:
                ghost = candleGhost;
                resourceCost = 20f;
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
                        Place(point, currentTile, Mine, currentOffset);
                        break;

                    case DreamFactorySelector.Placeables.Totem:
                        Place(point, currentTile, Totem, currentOffset);
                        break;

                    case DreamFactorySelector.Placeables.Candle:
                        Place(point, currentTile, Candle, currentOffset);
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
            dreamFuel.currentResourceValue > 0 + resourceCost && 
            !EventSystem.current.IsPointerOverGameObject())
            return true;

        return false; 
    }

    private void Place(Vector3 point, TileObject currentTile, GameObject towerToPlace, Vector3 offset)
    {
        Vector3 worldPosition = tilemap.CellToWorld(tilemap.WorldToCell(point));

        if (towerToPlace == turretPrefab)
        {
            GameObject newTurret = Instantiate(turretPrefab, worldPosition + offset, Quaternion.identity);

            //Create all turrets as a child of this gameobj, so the hierarchy doesn't get cluttered
            newTurret.transform.SetParent(this.transform);

            Turret turret = newTurret.GetComponent<Turret>();
          
            turret.Setup(currentTile, dreamFuel);
            turret.PayResourceCost();
        }

        else if(towerToPlace == factoryPrefab)
        {
            factoryExtraCost += factoryExtraCostPerFactory;
            factoryTotalCost = factoryBaseCost + factoryExtraCost;
            resourceCost = factoryTotalCost;

            GameObject newFactory = Instantiate(factoryPrefab, worldPosition + offset, Quaternion.identity);

            newFactory.transform.SetParent(this.transform);

            DreamFactory factory = newFactory.GetComponent<DreamFactory>();

            factory.Setup(currentTile, dreamFuel);
            factory.PayResourceCost(resourceCost);

        }

        else if (towerToPlace == Mine)
        {
            resourceCost = mineCost;
            GameObject newMine = Instantiate(Mine, worldPosition + offset, Quaternion.identity);
            newMine.transform.SetParent(this.transform);
            newMine.GetComponent<BaseTurret>().Setup(currentTile, dreamFuel);
            dreamFuel.currentResourceValue -= resourceCost;
            //DreamFuel.GetComponent<DreamFuel>().baseGeneration += factoryAddedGeneration;
            newMine.GetComponent<Mine>().construct(manager, dreamFuel);

        }

        else if (towerToPlace == Totem)
        {
            GameObject newTotem = Instantiate(Totem, worldPosition + offset, Quaternion.identity);

            newTotem.transform.SetParent(this.transform);
            
            Totempaal totem = newTotem.GetComponent<Totempaal>();

            totem.Setup(currentTile, dreamFuel);
            totem.PayResourceCost();
        }

        else if (towerToPlace == Candle)
        {
            GameObject newCandle = Instantiate(Candle, worldPosition + offset, Quaternion.identity);

            newCandle.transform.SetParent(this.transform);
           
            Candle candle = newCandle.GetComponent<Candle>();

            candle.FogOfWar = fogOfWar; 
            candle.Setup(currentTile, dreamFuel);
            candle.PayResourceCost();
        }

        audioManager.Play("thud");
        currentTile.TurretPlaced = true;
    }
}
