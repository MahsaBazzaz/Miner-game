using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV2
{
    public class MapGenerator : MonoBehaviour
    {
        public GridSystem grid;
        public MapVisualizer mapVisualizer;

        [SerializeField]
        public int width;
        public int height;
        public int xOrg;
        public int yOrg;
        public int tileSize;
        public int layer;
        public float magnification;
        public GameObject dirtPrefab;
        public GameObject greensPrefab;
        public GameObject stonePrefab;
        public GameObject waterPrefab;
        [SerializeField]
        public Cell[] cells;
        void Awake()
        {
            cells = new Cell[height * width];
            grid.setGridSystemParams(height, width, tileSize, layer);
            GameObject[] prefabs = { dirtPrefab, greensPrefab, stonePrefab, waterPrefab };
            mapVisualizer.setPrefabs(prefabs);
        }
        void Start()
        {
            Tile[,] tileGrid = grid.createGrid();
            mapVisualizer.CreateTileset();
            mapVisualizer.CreateTileGroups();
            // mapVisualizer.setPosition(-width / 2 + tileSize / 2, 0, height / 2 - ((byte)tileSize) / 2);
            generateMap(xOrg, yOrg, magnification, mapVisualizer.getTileCount());
        }
        public void generateMap(int x_offset, int y_offset, float magnification, int CellObjectTypeCount)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int tile_id = getIdUsingPerlin(i, j, x_offset, y_offset, magnification, CellObjectTypeCount);
                    Cell.CellType t = (Cell.CellType)tile_id;
                    cells[i + j * width] = new Cell { type = t, position = grid.calculateCoordinatesFromIndex(i + j * width) };
                    mapVisualizer.CreateTile(i, j, tile_id);
                }
            }
        }

        private int getIdUsingPerlin(int x, int y, int x_offset, int y_offset, float magnification, int CellObjectTypeCount)
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
        [Serializable]
        public class Cell
        {
            [Serializable]
            public enum CellType
            {
                Empty = -1,
                Dirt = 0,
                Greens = 1,
                Stone = 2,
                Water = 3
            }
            [SerializeField]
            public CellType type;
            public Vector3 position;
        }
    }

}