using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandidateMap
{
    private int width;
    private int height;
    private int[,] tileGrid;
    public CandidateMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        tileGrid = new int[width, height];
    }

    public int[,] GenerateMap(int x_offset, int y_offset, float magnification, int CellObjectTypeCount)
    {
        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                int tile_id = GetIdUsingPerlin(x, y, x_offset, y_offset, magnification, CellObjectTypeCount);
                tileGrid[x, y] = tile_id;
            }
        }
        return tileGrid;
    }

    private int GetIdUsingPerlin(int x, int y, int x_offset, int y_offset, float magnification, int CellObjectTypeCount)
    {
        /** Using a grid coordinate input, generate a Perlin noise value to be
            converted into a tile ID code. Rescale the normalised Perlin value
            to the number of tiles available. **/

        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification,
            (y - y_offset) / magnification
        );
        float clamp_perlin = Mathf.Clamp01(raw_perlin); // Thanks: youtu.be/qNZ-0-7WuS8&lc=UgyoLWkYZxyp1nNc4f94AaABAg
        float scaled_perlin = clamp_perlin * CellObjectTypeCount;

        // Replaced 4 with tileset.Count to make adding tiles easier
        if (scaled_perlin == CellObjectTypeCount)
        {
            scaled_perlin = (CellObjectTypeCount - 1);
        }
        return Mathf.FloorToInt(scaled_perlin);
    }


}
