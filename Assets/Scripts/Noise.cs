using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height, float scale, int octaves, float persistance, float lacunatity, Vector2 offset)
    {
        float[,] noiseMap = new float[height, width];

        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) octaveOffsets[i] = new Vector2(offset.x + 2000, offset.y + 3500);

        if (scale <= 0) scale = 0.0001f;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) * frequency + octaveOffsets[i].y;

                    //float value = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    float value = Simplex.CalcPixel2D((int)sampleX, (int)sampleY, 1 / scale);
                    noiseHeight += value * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunatity;
                }

                if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;

                noiseMap[x, y] = noiseHeight;

            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        
        return noiseMap;
    }
}
