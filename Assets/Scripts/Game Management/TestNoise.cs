using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNoise : MonoBehaviour
{

    Renderer renderer;

    int width, height;
    // Start is called before the first frame update
    void Start()
    {
        width = height = 256;
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GetTexture();
    }

    Texture2D GetTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; x < height; y++)
            {
                texture.SetPixel(x, y, GetColor(x, y));
            }

        }
        texture.Apply();
        return texture;
    }

    Color GetColor(int x, int y)
    {
        float xSample = (float)x / width;
        float ySample = (float)y / height;

        float shade = Mathf.PerlinNoise(xSample, ySample);
        return new Color(shade, shade, shade);

    }

}
