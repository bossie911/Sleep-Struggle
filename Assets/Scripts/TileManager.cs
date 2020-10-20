using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager: MonoBehaviour
{
    public Transform middleTilePoint;
    public TileBase middleTileSprite;
    public TileBase[] differentTiles;

    public Tilemap tilemap;

    Vector3Int middleTile;

    int[,] tileTypes;

    public int width, height;

    // Start is called before the first frame update
    void Start()
    {
        middleTile = tilemap.WorldToCell(middleTilePoint.position);

        tilemap.SetTile(middleTile, middleTileSprite);
        
        tileTypes = new int[width,height];

        for(int x = middleTile.x - width/2; x < middleTile.x + width/2; x++ ){
            for(int y = middleTile.y - height/2; y < middleTile.y + height/2; y++){
                //tilemap.SetTile(new Vector3Int(x,y,0), middleTileSprite);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
