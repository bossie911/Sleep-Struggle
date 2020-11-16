using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.WSA.Persistence;

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

    public bool TurretPlaced
    {
        get { return turretPlaced; }
        set { turretPlaced = value; }
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

    public bool CanPlaceTurret(){
        if (turretCanBePlaced && !turretPlaced) return true;
        return false;
    }
}
