using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV2
{
    [Serializable]
    public class Tile
    {
        private int x, z;
        private bool isTaken;
        private TileType objectType;

        public int X { get => x; }
        public int Z { get => z; }
        public TileType type { get => objectType; set => objectType = value; }

        public Tile(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.objectType = TileType.Empty;
        }
    }

    public enum TileType
    {
        Empty = -1,
        Dirt = 0,
        Greens = 1,
        Stone = 2,
        Water = 3
    }
}