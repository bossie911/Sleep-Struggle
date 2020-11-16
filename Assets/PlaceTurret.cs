using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlaceTurret : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject turret;
    public Tilemap fogOfWar;
    
    TileManager manager; 

    private Rigidbody ghost;

    //Place the turret slightly higher (looks better)
    private Vector3 offset; 

    void Start()
    {
        offset = new Vector3(0, 0.4f);
        ghost = GetComponentInChildren<Rigidbody>();
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
    }

    void Update()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Draw ghost on the tile the mouse is currently hovering over
        ghost.position = tilemap.CellToWorld(tilemap.WorldToCell(point)) + offset;

        TileObject currentTile = manager.GetTileFromPosition(point);

        if (Input.GetMouseButtonDown(0) && currentTile != null && currentTile.CanPlaceTurret())
            Place(point, currentTile);
    }

    void Place(Vector3 point, TileObject currentTile)
    {
        Vector3 worldPosition = tilemap.CellToWorld(tilemap.WorldToCell(point));

        GameObject newTurret = Instantiate(turret, worldPosition + offset, Quaternion.identity);

        //Create all turrets as a child of this gameobj, so the hierarchy doesn't get cluttered
        newTurret.transform.SetParent(this.transform);

        //Set reference FoW of the newly created turret
        newTurret.GetComponent<Turret>().fogOfWar = fogOfWar;

        currentTile.TurretPlaced = true;
    }
}
