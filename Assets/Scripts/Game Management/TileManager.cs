using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;



public class TileManager : MonoBehaviour
{
    public Transform middleTilePoint;
    public TileBase middleTileSprite;
    public TileBase[] differentTiles;

    public Tilemap tilemap;
    public Tilemap obstacles;

    Vector3Int middleTile;

    int[,] tileTypes;

    public int width, height;

    public float scale;

    public GameObject navMesh;

    // Start is called before the first frame update
    void Start()
    {
        tileTypes = new int[width, height];
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
                        tilemap.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), differentTiles[tileTypes[x, y]]);
                        break;
                    case 1:
                        tilemap.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), differentTiles[tileTypes[x, y]]);
                        break;
                    case 2:
                        obstacles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), differentTiles[tileTypes[x, y]]);
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

    void SetMiddleTile(){
        middleTile = tilemap.WorldToCell(middleTilePoint.position);
        tilemap.SetTile(middleTile, middleTileSprite);

    }
}

