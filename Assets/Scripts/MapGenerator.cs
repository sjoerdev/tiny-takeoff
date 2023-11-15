using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public float scale;

    public float meshHeightMultiplier;

    public int octaves;

    [Range(0, 1)]
    public float persistance;
    public float lacunatity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate = true;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistance, lacunatity, offset);
        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier));
    }

    void OnValidate()
    {
        if (width < 1) width = 1;
        if (height < 1) height = 1;
        if (lacunatity < 1) lacunatity = 1;
        if (octaves < 0) octaves = 0;
    }
}
