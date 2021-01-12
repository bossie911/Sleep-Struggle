using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Tilemaps;

public class Base : BaseTurret
{
    private Vector3Int currentTile;
    private int vision;

<<<<<<< HEAD
    [SerializeField] private Tilemap FogOfWar; 

    void Start()
    {
        turretHP = 100;
        vision = 3;
        FogOfWar = GameObject.Find("Towers").GetComponent<PlaceTurret>().fogOfWar;
        isBase = true; 
    }

    void Update()
    {
        AddFogOfWar();
    }
=======
    //Deze functie checked of de base collide met een enemy en de enemy doet dan damage
>>>>>>> Turret

    private void AddFogOfWar()
    {
        currentTile = FogOfWar.WorldToCell(transform.position); 

        //Clear the surrounding tiles
        for (int x = -vision; x <= vision - 1; x++)
        {
            for (int y = -vision - 1; y <= vision; y++)
            {
                FogOfWar.SetTile(currentTile + new Vector3Int(x, y, 0), null);
            }
        }
    }
}
