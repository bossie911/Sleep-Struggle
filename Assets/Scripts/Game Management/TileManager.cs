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
    public TileBase waterTilePrefab;
    public TileBase GrassTilePrefab;

    public Tilemap tilemap;
    public Tilemap obstacles;

    Vector3Int middleTile;

    float[,] tileTypes;
    TileObject[,] tileObjects;

    public int width, height;

    public float scale;

    public GameObject navMesh;

    private Camera cam;

    Vector2Int middleTilePosition;

    public bool generateRandomMap;

    public float waterChance;


    // Start is called before the first frame update
    void Start()
    {
        tileTypes = new float[width, height];
        tileObjects = new TileObject[width, height];
        SetTiles();
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(GetTileFromPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)).CanPlaceTower());
        }
    }

    public float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float xOffset, float yOffset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float sampleX = (float)x / mapWidth * scale + xOffset * 10;
                float sampleY = (float)y / mapHeight * scale + yOffset * 10;
                float noise = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = noise;
            }
        }
        return noiseMap;
    }

    /// <summary>
    /// sets all tiles when starting up
    /// </summary>
    void SetTiles()
    {
        if (generateRandomMap)
        {
            tileTypes = GenerateNoiseMap(width, height, scale, Random.Range(-100, 100), Random.Range(-100, 100));
            tilemap.ClearAllTiles();
            obstacles.ClearAllTiles();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)//two dimentional for loop
                {
                    if (tileTypes[x, y] > waterChance) {
                        PlaceWalkable(new Vector2Int(x, y));
                    }
                    else
                    {
                        PlaceObstacle(x, y);
                    }
                }
            }
            PlaceResources();
            CheckMiddleTileLocation();
            SetMiddleTile();
        }
        else { tileObjects = GeneratePremadeObjectArray(); }
        UpdateNavMesh();
    }

    
    void CheckMiddleTileLocation()
    {
        bool onWater = !GetTileFromPosition(middleTilePoint.transform.position).CanPlaceTower();

        if (onWater)
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
                            PlaceWalkable(new Vector2Int(x, y));
                            break;
                        case 1:
                            PlaceWalkable(new Vector2Int(x, y));
                            break;
                        case 2:
                            PlaceObstacle(x, y);
                            break;
                    }
                }

            }
            CheckMiddleTileLocation();
        }
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
    public void PlaceWalkable(Vector2Int loc)
    {
        tileObjects[loc.x, loc.y] = new TileObject(loc.x, loc.y, true, false);
        tilemap.SetTile(new Vector3Int(loc.x - width / 2, loc.y - height / 2, 0), GrassTilePrefab);
    }

    /// <summary>
    /// can be used to place a non walkable tile
    /// </summary>
    /// <param name="x">The x in the tilemap</param>
    /// <param name="y">the Y in the tilemap</param>
    void PlaceObstacle(int x, int y)
    {
        tileObjects[x, y] = new TileObject( x, y, false, false);
        obstacles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), waterTilePrefab);
    }

    /// <summary>
    /// sets the home tile 
    /// </summary>
    void SetMiddleTile()
    {
        int x = (width / 2) - 1;
        int y = height / 2;
        middleTilePosition = new Vector2Int(x, y);

        tileObjects[x, y] = new TileObject(x, height / 2, false, false);
        tilemap.SetTile(tilemap.WorldToCell(middleTilePoint.position), middleTileSprite);
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
    /// Looks at the sprites already drawn in the editor and generates a tileobjects array from there
    /// </summary>
    /// <returns></returns>
    TileObject[,] GeneratePremadeObjectArray()
    {

        TileObject[,] objects = new TileObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int mid = tilemap.WorldToCell(middleTile);
                switch (tilemap.GetSprite(new Vector3Int(mid.x - width / 2 + x, mid.y - height / 2 + y, 0)).name)
                {
                    case "Grass":
                        objects[x, y] = new TileObject( x, y, true, false);
                        break;
                    case "Resource":
                        objects[x, y] = new TileObject( x, y, true, true);
                        break;
                    case "Water":
                        objects[x, y] = new TileObject(x, y, false, false);
                        tilemap.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), null);
                        obstacles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), waterTilePrefab);
                        break;
                }
            }
        }
        return objects;
    }


    /// <summary>
    /// Places the resource tiles 
    /// </summary>
    /// <param name="amount"> Determines the amount of resource tiles placed</param>
    void PlaceResources()
    {

        for (int i = 5; i < 15; i++)
        {
            PlaceResource(RandomTile(i));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance">The distance from the middle tile </param>
    /// <returns>Random tile with a specified distance from the middle</returns>
    Vector2Int RandomTile(int distance)
    {
        int xOffset = Random.Range(-distance, distance);
        int yOffset = distance - Math.Abs(xOffset);
        bool spawnabove = (Random.Range(0, 2) == 0);
        Debug.Log("spawn above" + spawnabove);
        if (spawnabove)
        {
            return new Vector2Int(xOffset, -yOffset) + middleTilePosition;
        }
        else
        {
            return new Vector2Int(xOffset, yOffset) + middleTilePosition;
        }

    }


    /// <summary>
    /// Places a resource tile and a tileObject at a specified location
    /// </summary>
    /// <param name="loc">The location where the resource tile will be placed</param>
    void PlaceResource(Vector2Int loc)
    {
        tileObjects[loc.x, loc.y] = new TileObject(loc.x, loc.y, true, true);
        tilemap.SetTile(new Vector3Int(loc.x - width / 2, loc.y - height / 2, 0), resourceTilePrefab);
    }
}

