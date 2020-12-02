using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileObject
{
    bool turretCanBePlaced, visible, turretPlaced;

    TileBase tileBase;

    public int xLocation, yLocation;

    bool isResourceTile, isBeingMined;

    int startingResources, currentResources;

    /// <summary>
    /// An object with different values all linked to a specific tile in the tilemap
    /// </summary>
    /// <param name="tile">The tile that this place in the tilemap will start with</param>
    public TileObject(TileBase tile, int x, int y, bool canPlace, bool isResource)
    {
        tileBase = tile;
        xLocation = x;
        yLocation = y;
        turretCanBePlaced = canPlace;
        isResourceTile = isResource;
        if (isResourceTile) {
            startingResources = 100;
            currentResources = startingResources;
        }

    }

    /// <summary>
    /// Wether the tile contains a turret
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The location of this tile in the tileObjects array</returns>
    public Vector2Int GetLocation()
    {
        return new Vector2Int(xLocation, yLocation);
    }

    /// <summary>
    /// Returns if a tower can be placed on this tile
    /// </summary>
    /// <returns></returns>
    public bool CanPlaceTower(){
        if (turretCanBePlaced && !turretPlaced) return true;
        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns>Wether the tile is a resource tile</returns>
    public bool IsResourceTile() {
        return isResourceTile;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The current amount of resources left</returns>
    public int GetResources() {
        return currentResources;
    }


    /// <summary>
    /// changes the current amount of resources left
    /// </summary>
    /// <param name="amountMined">The amount changed</param>
    public void Mine(int amountMined) {
        currentResources -= amountMined;
    }

}
