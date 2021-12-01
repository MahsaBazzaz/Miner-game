using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV2
{

    [CreateAssetMenu(fileName = "Chamber", menuName = "Chamber/New Chamber", order = 1)]
    public class Chamber : ScriptableObject
    {
        public List<ChamberCell> shape;
        public int pivotIndex;
        public GameObject prefab;

        [Serializable]
        public class ChamberCell
        {
            public int row;
            public int col;
            public bool val;
        }

    }
}
