using UnityEngine;
using System.Threading.Tasks;

public static class Noise
{
    public static float[,] GenerateHeightMap(int width, int height, float scale, int octaves, float amplitude, Vector2 offset)
    {
        float[,] heightMap = new float[height, width];

        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; x++)
            {
                float value = 0;
                float currentAmplitude = amplitude;
                float currentScale = scale;

                for (int i = 0; i < octaves; i++)
                {
                    value += Simplex.CalcPixel2D(x + (int)offset.x + width / 2, y + (int)offset.y + height / 2, 1 / currentScale) * currentAmplitude;
                    currentAmplitude *= 0.5f;
                    currentScale *= 0.5f;
                }

                heightMap[x, y] = value;
            }
        });

        return heightMap;
    }
}
