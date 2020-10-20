using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTurret : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject turret;
    
    private Rigidbody ghost;

    //Place the turret slightly higher (looks better)
    private float offset = 0.4f; 

    void Start()
    {
        ghost = GetComponentInChildren<Rigidbody>(); 
    }

    void Update()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Draw ghost on the tile the mouse is currently hovering over
        ghost.position = tilemap.CellToWorld(tilemap.WorldToCell(point)) + new Vector3(0, offset); 

        if (Input.GetMouseButtonDown(0))
        {   
            Vector3 worldPosition = tilemap.CellToWorld(tilemap.WorldToCell(point));

            GameObject newTurret = Instantiate(turret, new Vector3(worldPosition.x, worldPosition.y + offset), Quaternion.identity);

            //Create all turrets as a child of this gameobj, so the hierarchy doesn't get cluttered
            newTurret.transform.SetParent(this.transform);
        }
    }
}
