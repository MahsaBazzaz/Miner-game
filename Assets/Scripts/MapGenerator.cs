using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapGrid grid;
    public MapVisualizer mapVisualizer;
    public int xOrg;
    public int yOrg;
    private CandidateMap map;
    public float magnification;
    void Start()
    {
        map = new CandidateMap(grid.width, grid.height);
        mapVisualizer.CreateTileset();
        mapVisualizer.CreateTileGroups();
        int[,] gridTile = map.GenerateMap(xOrg, yOrg, magnification, mapVisualizer.getTileCount());

        for (int x = 0; x < grid.width; x++)
        {

            for (int y = 0; y < grid.height; y++)
            {
                grid.SetCell(x, y, gridTile[x, y]);
                mapVisualizer.CreateTile(gridTile[x, y], x, y);
            }
        }
    }
}
