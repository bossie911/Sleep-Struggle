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

    public Tilemap tilemap;
    public Tilemap obstacles;

    Vector3Int middleTile;

    int[,] tileTypes;
    TileObject[,] tileObjects;

    public int width, height;

    public float scale;

    public GameObject navMesh;

    // Start is called before the first frame update
    void Start()
    {
        tileTypes = new int[width, height];
        tileObjects = new TileObject[width, height];
        SetTiles();
    }

    // Update is called once per frame
    void Update()
    {
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
        UpdateNavMesh();
    }

    void UpdateNavMesh()
    {
        navMesh.GetComponent<NavMeshSurface2d>().BuildNavMesh();
    }

    void PlaceWalkable(int x, int y)
    {
        tileObjects[x, y] = new TileObject(differentTiles[tileTypes[x, y]], x, y, true);
        tilemap.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tileObjects[x, y].GetTile());
        Debug.Log("grass");
    }

    void PlaceObstacle(int x, int y)
    {
        tileObjects[x, y] = new TileObject(differentTiles[tileTypes[x, y]], x, y, false);
        obstacles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tileObjects[x, y].GetTile());
    }

    void SetMiddleTile()
    {
        int x = (width / 2) - 1;  
        
        tileObjects[x ,height/2] = new TileObject(middleTileSprite, x, height/2, false);
        tilemap.SetTile(tilemap.WorldToCell(middleTilePoint.position), tileObjects[x, height/2].GetTile());
    }

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
}

