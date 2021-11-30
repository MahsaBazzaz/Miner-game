using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GridSystemV2
{
    public class GridSystem : MonoBehaviour
    {
        private Tile[,] cellGrid;
        public int width;
        public int height;
        public int tileSize;
        public int layer;
        public Color gizmosColor;
        void Awake()
        {
            cellGrid = new Tile[height, width];
            for (int row = 0; row < height; row += tileSize)
            {
                for (int col = 0; col < width; col += tileSize)
                {
                    cellGrid[row, col] = new Tile(row, col, layer);
                    // GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    // go.transform.localScale = new Vector3(tileSize, tileSize, 1);
                    // go.transform.position = new Vector3(col, row, layer);
                    // go.GetComponent<Renderer>().material.color = gizmosColor;
                }
            }
        }
        public Tile[,] getGrid()
        {
            return cellGrid;
        }
        public Vector3 calculateCoordinatesFromIndex(int index)
        {
            int row = index / width;
            int col = index % width;
            return new Vector3(row * tileSize, col * tileSize, layer);
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

        public Vector3 GetNearestPointOnGrid(Vector3 position)
        {
            position -= transform.position;

            int xCount = Mathf.RoundToInt(position.x / tileSize);
            int yCount = Mathf.RoundToInt(position.y / tileSize);
            int zCount = Mathf.RoundToInt(position.z / tileSize);

            Vector3 result = new Vector3(
                (float)xCount * tileSize,
                (float)yCount * tileSize,
                (float)zCount * tileSize);

            result += transform.position;

            return result;
        }
    }
}