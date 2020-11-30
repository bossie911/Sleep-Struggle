using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class TileManager : MonoBehaviour
{
    public Transform middleTilePoint;
    public TileBase middleTileSprite;
    public TileBase[] differentTiles;
    public TileBase resourceTilePrefab;

    public Tilemap tilemap;
    public Tilemap obstacles;

    Vector3Int middleTile;

    int[,] tileTypes;
    TileObject[,] tileObjects;

    public int width, height;

    public float scale;

    public GameObject navMesh;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        tileTypes = new int[width, height];
        tileObjects = new TileObject[width, height];
        SetTiles();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            Debug.Log("surrounded? " + IsSurroundedByWater(new Vector2Int( GetTileFromPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)).xLocation, 
                GetTileFromPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)).yLocation)));
        }
    }

    public int[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float xOffset, float yOffset)
    {
        int[,] noiseMap = new int[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float sampleX = (float)x / mapWidth * scale + xOffset * 10;
                float sampleY = (float)y / mapHeight * scale + yOffset * 10;
                float noise = Mathf.PerlinNoise(sampleX, sampleY) * differentTiles.Length;
                noiseMap[x, y] = (int)noise;
            }
        }
        return noiseMap;
    }

    /// <summary>
    /// sets all tiles at the beginning
    /// </summary>
    void SetTiles()
    {
        tileTypes = GenerateNoiseMap(width, height, scale, Random.Range(-100, 100), Random.Range(-100, 100));
        tilemap.ClearAllTiles();
        obstacles.ClearAllTiles();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (tileTypes[x, y])
                {
                    case 0:
                        PlaceWalkable(x, y);
                        break;
                    case 1:
                        PlaceWalkable(x, y);
                        break;
                    case 2:
                        PlaceObstacle(x, y);
                        break;

                }
            }
        }
        SetMiddleTile();
        PlaceResources();
        UpdateNavMesh();
    }

    //can be called whenever to update the navmesh
    void UpdateNavMesh()
    {
        navMesh.GetComponent<NavMeshSurface2d>().BuildNavMesh();
    }

    /// <summary>
    /// can be used to place a walkable tile
    /// </summary>
    /// <param name="x">The x in the tilemap</param>
    /// <param name="y">the Y in the tilemap</param>
    void PlaceWalkable(int x, int y)
    {
        tileObjects[x, y] = new TileObject(differentTiles[tileTypes[x, y]], x, y, true, false);
        tilemap.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tileObjects[x, y].GetTile());

    }

    /// <summary>
    /// can be used to place a non walkable tile
    /// </summary>
    /// <param name="x">The x in the tilemap</param>
    /// <param name="y">the Y in the tilemap</param>
    void PlaceObstacle(int x, int y)
    {
        tileObjects[x, y] = new TileObject(differentTiles[tileTypes[x, y]], x, y, false, false);
        obstacles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tileObjects[x, y].GetTile());
    }

    /// <summary>
    /// sets the home tile 
    /// </summary>
    void SetMiddleTile()
    {
        int x = (width / 2) - 1;

        tileObjects[x, height / 2] = new TileObject(middleTileSprite, x, height / 2, false, false);
        tilemap.SetTile(tilemap.WorldToCell(middleTilePoint.position), tileObjects[x, height / 2].GetTile());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="positionRequest"></param>
    /// <returns></returns>
    public TileObject GetTileFromPosition(Vector3 positionRequest)
    {
        Vector3Int pos = tilemap.WorldToCell(positionRequest);

        //Check whether the given world position is on the tilemap
        try
        {
            var tryGetIndex = tileObjects[pos.x + width / 2, pos.y + height / 2];
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

        return tileObjects[pos.x + width / 2, pos.y + height / 2];
    }


    /// <summary>
    /// Places the resource tiles 
    /// </summary>
    /// <param name="amount"> Determines the amount of resource tiles placed</param>
    void PlaceResources() {
        for (int i = 5; i < 15; i++) {
            
            Vector2Int whereToPlace = RandomTile(i);
            while (IsSurroundedByWater( whereToPlace)) {
                whereToPlace = RandomTile(i);
            }

            PlaceResource(whereToPlace);
            
        }
    }

    Vector2Int RandomTile(int i) {
        //return new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        int xOffset = Random.Range(-i, i);
        bool aboveMiddle = (Random.value > 0.5f);
        if (aboveMiddle)
        {
            Debug.Log("location(above)" + new Vector2Int(xOffset + width / 2, i - xOffset + height / 2));
            return new Vector2Int(xOffset + width / 2, i - xOffset + height / 2);
        }
        else {
            Debug.Log("Location(below) =" + new Vector2Int(-xOffset + width / 2, -i + xOffset + height / 2));
            return new Vector2Int(-xOffset + width / 2, -i + xOffset + height / 2);

        }
    }

    void PlaceResource(Vector2Int loc) {
        tileObjects[loc.x, loc.y] = new TileObject(resourceTilePrefab, loc.x, loc.y, true, true);
        tilemap.SetTile(new Vector3Int(loc.x - width / 2, loc.y - height / 2, 0), tileObjects[loc.x, loc.y].GetTile());
    }

    /// <summary>
    /// Returns if a coordinate of the tilemap is surrounded by water tiles
    /// </summary>
    /// <param name="loc"></param>
    /// <returns></returns>
    public bool IsSurroundedByWater(Vector2Int loc)
    {
        if (tileObjects[loc.x - 1, loc.y - 1] != null)
        {
            if (tileObjects[loc.x - 1, loc.y - 1].CanPlaceTurret())
            {
                return false;
            }
        }
        if (tileObjects[loc.x, loc.y - 1] != null)
        {
            if (tileObjects[loc.x, loc.y - 1].CanPlaceTurret())
            {
                return false;
            }
        }
        if (tileObjects[loc.x - 1, loc.y - 1] != null)
        {
            if (tileObjects[loc.x - 1, loc.y - 1].CanPlaceTurret())
            {
                return false;
            }
        }
        if (tileObjects[loc.x + 1, loc.y] != null)
        {
            if (tileObjects[loc.x + 1, loc.y].CanPlaceTurret())
            {
                return false;
            }
        }
        if (tileObjects[loc.x - 1, loc.y + 1] != null)
        {
            if (tileObjects[loc.x - 1, loc.y + 1].CanPlaceTurret())
            {
                return false;
            }
        }
        if (tileObjects[loc.x, loc.y + 1] != null)
        {
            if (tileObjects[loc.x, loc.y + 1].CanPlaceTurret())
            {
                return false;
            }
        }
        if (tileObjects[loc.x + 1, loc.y + 1] != null)
        {
            if (tileObjects[loc.x + 1, loc.y + 1].CanPlaceTurret())
            {
                return false;
            }
        }
        return true;
    }
}

