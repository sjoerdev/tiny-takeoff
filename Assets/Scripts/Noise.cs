using UnityEngine;

public static class Noise
{
    public static float[,] GenerateHeightMap(int width, int height, float scale, int octaves, float amplitude, Vector2 offset)
    {
        float[,] heightMap = new float[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = Simplex.CalcPixel2D(x + (int)offset.x + width / 2, y + (int)offset.y + height / 2, 1 / scale);
                heightMap[x, y] = value * amplitude;
            }
        }

        return heightMap;
    }
}
