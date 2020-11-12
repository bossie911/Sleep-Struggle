using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileObject
{
    bool turretCanBePlaced, visible, turretPlaced;

    TileBase tileBase;

    int xLocation, yLocation;

    /// <summary>
    /// An object with different values all linked to a specific tile in the tilemap
    /// </summary>
    /// <param name="tile">The tile that this place in the tilemap will start with</param>
    public TileObject(TileBase tile, int x, int y, bool canPlace)
    {
        tileBase = tile;
        xLocation = x;
        yLocation = y;
        turretCanBePlaced = canPlace;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The tilebase for this tile</returns>
    public TileBase GetTile()
    {
        return tileBase;
    }

    public Vector2Int GetLocation()
    {
        return new Vector2Int(xLocation, yLocation);
    }

    public void PlaceTurret()
    {
        turretPlaced = true;
    }

    public bool CanPlaceTurret(){
        if(turretCanBePlaced && !turretPlaced){return true;}
        else{return false;}
    }
}
