using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DreamFactory : MonoBehaviour
{

    public float resourceCost = 25f;

    public float addedGeneration = 0.5f;

    public Tilemap fogOfWar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFogOfWar();
    }

    public int vision = 1;
    void UpdateFogOfWar()
    {
        Vector3Int currentTowerTile = fogOfWar.WorldToCell(transform.position);

        //Clear the surrounding tiles
        for (int x = -vision; x <= vision - 1; x++)
        {
            for (int y = -vision - 1; y <= vision; y++)
            {
                fogOfWar.SetTile(currentTowerTile + new Vector3Int(x, y, 0), null);
            }
        }
    }
}
