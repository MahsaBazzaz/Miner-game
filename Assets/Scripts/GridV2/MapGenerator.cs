using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV2
{
    public class MapGenerator : MonoBehaviour
    {
        public GridSystem grid;
        public MapVisualizer mapVisualizer;
        public int xOrg;
        public int yOrg;
        public float magnification;
        public int width;
        public int height;
        public int tileSize;
        [SerializeField] public Cell[] cells;
        void Awake()
        {
            this.width = grid.width;
            this.height = grid.height;
            this.tileSize = grid.tileSize;
            cells = new Cell[height * width];
        }
        void Start()
        {
            // mapVisualizer.setPosition(-width / 2 + tileSize / 2, 0, height / 2 - ((byte)tileSize) / 2);
            generateMap(xOrg, yOrg, magnification, mapVisualizer.getTileCount());
        }
        public void generateMap(int x_offset, int y_offset, float magnification, int CellObjectTypeCount)
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int tile_id = getIdUsingPerlin(row, col, x_offset, y_offset, magnification, CellObjectTypeCount);
                    Cell.CellType t = (Cell.CellType)tile_id;
                    Vector3 pos = grid.calculateCoordinatesFromIndex(row + col * width);
                    cells[row * width + col] = new Cell { type = t, position = pos };
                    mapVisualizer.CreateTile(row, col, tile_id, pos);
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