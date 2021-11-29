using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV2
{
    public class MapGenerator : MonoBehaviour
    {
        public MapVisualizer mapVisualizer;

        [SerializeField]
        public int width;
        public int height;
        public int xOrg;
        public int yOrg;
        public float magnification;
        public GameObject dirtPrefab;
        public GameObject greensPrefab;
        public GameObject stonePrefab;
        public GameObject waterPrefab;
        private GridSystem grid;
        private int[,] tileGrid;
        void Awake()
        {
            grid = new GridSystem(height, width);
            GameObject[] prefabs = { dirtPrefab, greensPrefab, stonePrefab, waterPrefab };
            mapVisualizer.setPrefabs(prefabs);

        }
        void Start()
        {
            grid.createGrid();
            mapVisualizer.CreateTileset();
            mapVisualizer.CreateTileGroups();
            generateMap(xOrg, yOrg, magnification, mapVisualizer.getTileCount());
        }
        public void generateMap(int x_offset, int y_offset, float magnification, int CellObjectTypeCount)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int tile_id = getIdUsingPerlin(x, y, x_offset, y_offset, magnification, CellObjectTypeCount);
                    grid.setTile(x, y, tile_id);
                    mapVisualizer.CreateTile(x, y, tile_id);
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
    }
}