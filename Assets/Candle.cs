using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : BaseTurret
{
    public int vision; 

    void Awake()
    {
        vision = 2;
        resourceCost = 20f; 
    }

    void Update()
    {
        UpdateFogOfWar();
    }

   private void UpdateFogOfWar()
    {
        Vector3Int currentTile = FogOfWar.WorldToCell(transform.position);

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
