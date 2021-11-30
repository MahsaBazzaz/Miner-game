using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GridSystemV2
{
    public class GridSystem : MonoBehaviour
    {
        private Tile[,] cellGrid;
        private int width;
        private int height;
        private int tileSize;
        private int layer;
        public void setGridSystemParams(int height, int width, int tileSize, int layer)
        {
            this.height = height;
            this.width = width;
            this.tileSize = tileSize;
            this.layer = layer;
        }
        public Tile[,] createGrid()
        {
            cellGrid = new Tile[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    cellGrid[row, col] = new Tile(col * tileSize, row * -tileSize, layer);
                }
            }
            return cellGrid;
        }
        public int calculateIndexFromCoordinates(int x, int z)
        {
            int row = x / tileSize;
            int col = z / tileSize;
            return row + col * width;
        }
        public Vector3 calculateCoordinatesFromIndex(int index)
        {
            int row = index % width;
            int col = index / width;
            return new Vector3(row * tileSize, 0, col * tileSize);
        }
        public bool isTileValid(float x, float z)
        {
            if (x >= width || x < 0 || z >= height || z < 0)
            {
                return false;
            }
            return true;
        }
        public Tile getTile(int row, int col)
        {
            if (isTileValid(row, col) == false)
            {
                return null;
            }
            return cellGrid[col, row];
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