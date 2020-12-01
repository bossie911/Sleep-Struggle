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

    public bool IsResourceTile() {
        return isResourceTile;
    }

    public int GetResources() {
        return currentResources;
    }

    public void MakeResourceTile(bool resource) {
        isResourceTile = resource;
    }

    public void Mine(int amountMined) {
        currentResources -= amountMined;
    }

}
