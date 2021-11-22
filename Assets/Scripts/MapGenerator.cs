using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapVisualizer mapVisualizer;
    public int width, height;
    public int xOrg;
    public int yOrg;
    private MapGrid grid;
    private CandidateMap map;
    public float magnification;
    void Start()
    {
        grid = new MapGrid(width, height);
        map = new CandidateMap(grid.Width, grid.Length);
        mapVisualizer.CreateTileset();
        mapVisualizer.CreateTileGroups();
        int[,] gridTile = map.GenerateMap(xOrg, yOrg, magnification, mapVisualizer.getTileCount());

        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                grid.SetCell(x, y, gridTile[x, y]);
                mapVisualizer.CreateTile(gridTile[x, y], x, y);
            }
        }
    }

}
