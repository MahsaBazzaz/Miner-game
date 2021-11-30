using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV2
{
    [Serializable]
    public class Tile
    {
        private int x, y, z;
        public int X { get => x; }
        public int Y { get => y; }
        public int Z { get => z; }

        public Tile(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}