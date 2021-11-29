using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GridSystemV2
{
    public class GridSystem
    {
        private Tile[,] cellGrid;
        private int width;
        private int height;

        public GridSystem(int height, int width)
        {
            this.height = height;
            this.width = width;
        }
        public void createGrid()
        {
            cellGrid = new Tile[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    cellGrid[row, col] = new Tile(col, row);
                    cellGrid[row, col].type = TileType.Empty;
                }
            }
        }
        public void setTile(int x, int z, int tileTypeIndex, bool isTaken = false)
        {
            TileType ctype = (TileType)tileTypeIndex;
            cellGrid[z, x].type = ctype;
        }
        public void setTile(int x, int z, TileType objectType, bool isTaken = false)
        {
            cellGrid[z, x].type = objectType;
        }
        public int calculateIndexFromCoordinates(int x, int z)
        {
            return x + z * width;
        }
        public Vector3 calculateCoordinatesFromIndex(int randomIndex)
        {
            int x = randomIndex % width;
            int z = randomIndex / width;
            return new Vector3(x, 0, z);
        }
        public bool isTileValid(float x, float z)
        {
            if (x >= width || x < 0 || z >= height || z < 0)
            {
                return false;
            }
            return true;
        }
        public Tile getTile(int x, int z)
        {
            if (isTileValid(x, z) == false)
            {
                return null;
            }
            return cellGrid[z, x];
        }
        public int calculateIndexFromCoordinates(float x, float z)
        {
            return (int)x + (int)z * width;
        }

        public void checkCoordinates()
        {
            for (int i = 0; i < cellGrid.GetLength(0); i++)
            {
                StringBuilder b = new StringBuilder();
                for (int j = 0; j < cellGrid.GetLength(1); j++)
                {
                    b.Append(j + "," + i + " ");
                }
                Debug.Log(b.ToString());
            }
        }
    }
}