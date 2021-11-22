using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MapGrid
{
    private int width, length;
    private Cell[,] cellGrid;

    public int Width { get => width; }
    public int Length { get => length; }

    public MapGrid(int width, int length)
    {
        this.width = width;
        this.length = length;
        CreateGrid();
    }

    private void CreateGrid()
    {
        cellGrid = new Cell[length, width];
        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < width; col++)
            {
                cellGrid[row, col] = new Cell(col, row);
                cellGrid[row, col].IsTaken = false;
                cellGrid[row, col].ObjectType = CellObjectType.Empty;
            }
        }
    }
    public void SetCell(int x, int z, int objectTypeIndex, bool isTaken = false)
    {
        CellObjectType ctype = (CellObjectType)objectTypeIndex;
        cellGrid[z, x].ObjectType = ctype;
        cellGrid[z, x].IsTaken = isTaken;
    }
    public void SetCell(int x, int z, CellObjectType objectType, bool isTaken = false)
    {
        cellGrid[z, x].ObjectType = objectType;
        cellGrid[z, x].IsTaken = isTaken;
    }
    public bool IsCellTaken(int x, int z)
    {
        return cellGrid[z, x].IsTaken;
    }
    public int CalculateIndexFromCoordinates(int x, int z)
    {
        return x + z * width;
    }

    public Vector3 CalculateCoordinatesFromIndex(int randomIndex)
    {
        int x = randomIndex % width;
        int z = randomIndex / width;
        return new Vector3(x, 0, z);
    }

    public bool IsCellValid(float x, float z)
    {
        if (x >= width || x < 0 || z >= length || z < 0)
        {
            return false;
        }
        return true;
    }

    public Cell GetCell(int x, int z)
    {
        if (IsCellValid(x, z) == false)
        {
            return null;
        }
        return cellGrid[z, x];
    }

    public Cell GetCell(float x, float z)
    {
        return GetCell((int)x, (int)z);
    }
    public int CalculateIndexFromCoordinates(float x, float z)
    {
        return (int)x + (int)z * width;
    }

    public void CheckCoordinates()
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
