using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Candle : BaseTurret
{
    private int vision;
    
    [SerializeField]
    private TileBase fogTile; 
    private Vector3Int currentTile; 

    void Awake()
    {
        vision = 2;
        resourceCost = 20f;
        turretHP = 30f; 
    }

    void Update()
    {
        UpdateFogOfWar();

        if(turretHP < 1)
            AddFogOfWar();
    }

   private void UpdateFogOfWar()
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

   private void AddFogOfWar()
   {
       //Clear the surrounding tiles
       for (int x = -vision; x <= vision - 1; x++)
       {
           for (int y = -vision - 1; y <= vision; y++)
           {
               FogOfWar.SetTile(currentTile + new Vector3Int(x, y, 0), fogTile);
           }
       }
   }
}
