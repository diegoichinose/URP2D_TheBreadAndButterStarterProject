using UnityEngine;

public class MyPerlinNoiseLibrary
{
    // NOISE SCALE = Noise image zoom. Lower values means a more zoomed-in noise map which means more isolated islands of props
    public static float[,] GenerateNoiseMap(int seed, int sizeX, int sizeY, float noiseScale)
    {
        var noiseMap = new float[sizeX, sizeY];

        for(int y = 0; y < sizeY; y++)
        {
            for(int x = 0; x < sizeX; x++)
            {
                float noiseValue = Mathf.PerlinNoise((x + seed) * noiseScale, (y + seed) * noiseScale);
                noiseMap[x, y] = noiseValue;
            }
        }

        return noiseMap;
    }

    public static float[,] GenerateNoiseMap(int seed, int size, float noiseScale)
    {
        return GenerateNoiseMap(seed, size, size, noiseScale);
    }
}