using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV1
{
    [Serializable]
    public class Cell
    {
        private int x, z;
        private bool isTaken;
        private CellObjectType objectType;

        public int X { get => x; }
        public int Z { get => z; }
        public bool IsTaken { get => isTaken; set => isTaken = value; }
        public CellObjectType ObjectType { get => objectType; set => objectType = value; }

        public Cell(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.objectType = CellObjectType.Empty;
            isTaken = false;
        }
    }

    public enum CellObjectType
    {
        Empty = -1,
        Dirt = 0,
        Greens = 1,
        Stone = 2,
        Water = 3
    }
}