using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileManager : MonoBehaviour
{
    public Transform middleTilePoint;
    public TileBase middleTileSprite;
    public TileBase[] differentTiles;

    public Tilemap tilemap;

    Vector3Int middleTile;

    int[,] tileTypes;

    public int width, height;

    public float scale;
    Random random;

    // Start is called before the first frame update
    void Start()
    {
        middleTile = tilemap.WorldToCell(middleTilePoint.position);

        tilemap.SetTile(middleTile, middleTileSprite);

        tileTypes = new int[width, height];

        for (int x = middleTile.x - width / 2; x < middleTile.x + width / 2; x++)
        {
            for (int y = middleTile.y - height / 2; y < middleTile.y + height / 2; y++)
            {
               // tilemap.SetTile(new Vector3Int(x,y,0), middleTileSprite);
            }
        }
        random = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetTiles(); // if f is pressed, the generation will occur
        }

    }

    public int[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float xOffset, float yOffset)
    {
        int[,] noiseMap = new int[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float sampleX = (float)x / mapWidth * scale;
                float sampleY = (float)y / mapHeight * scale;
                float noise = Mathf.PerlinNoise(sampleX, sampleY) * differentTiles.Length;
                noiseMap[x, y] = (int)noise;
            }
        }
        return noiseMap;
    }

    void SetTiles()
    {
        tileTypes = GenerateNoiseMap(width, height, scale, 1, 1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tilemap.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), differentTiles[tileTypes[x, y]]);
            }

        }
    }
}

