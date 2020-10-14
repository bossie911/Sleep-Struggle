using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager: MonoBehaviour
{
    public Transform middleTilePoint;
    public TileBase middleTileSprite;

    public Tilemap tilemap;

    Vector3Int middleTile;

    // Start is called before the first frame update
    void Start()
    {
        middleTile = tilemap.WorldToCell(middleTilePoint.position);
        tilemap.SetTile(middleTile, middleTileSprite);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
