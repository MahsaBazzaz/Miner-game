using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystemV2
{
    public class MapVisualizer : MonoBehaviour
    {
        private List<List<int>> noise_grid = new List<List<int>>();
        private Dictionary<int, GameObject> tile_groups;
        private Dictionary<int, GameObject> tileset;
        private GameObject[] prefabs;
        public void setPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }
        public void setPrefabs(GameObject[] prefabs)
        {
            this.prefabs = prefabs;
        }
        public void CreateTileset()
        {
            /** Collect and assign ID codes to the tile prefabs, for ease of access.
                Best ordered to match land elevation. **/

            tileset = new Dictionary<int, GameObject>();
            tileset.Add(0, prefabs[0]);
            tileset.Add(1, prefabs[1]);
            tileset.Add(2, prefabs[2]);
            tileset.Add(3, prefabs[3]);
        }
        public void CreateTileGroups()
        {
            /** Create empty gameobjects for grouping tiles of the same type, ie
                forest tiles **/

            tile_groups = new Dictionary<int, GameObject>();
            foreach (KeyValuePair<int, GameObject> prefab_pair in tileset)
            {
                GameObject tile_group = new GameObject(prefab_pair.Value.name);
                tile_group.transform.parent = gameObject.transform;
                tile_group.transform.localPosition = new Vector3(0, 0, 0);
                tile_groups.Add(prefab_pair.Key, tile_group);
            }
        }
        public void CreateTile(int x, int y, int tile_id)
        {
            /** Creates a new tile using the type id code, group it with common
                tiles, set it's position and store the gameobject. **/

            GameObject tile_prefab = tileset[tile_id];
            GameObject tile_group = tile_groups[tile_id];
            GameObject tile = Instantiate(tile_prefab, tile_group.transform);

            tile.name = string.Format("tile_x{0}_y{1}", x, y);
            tile.transform.localPosition = new Vector3(x, y, 0);
        }
        public void DestroyTile(Vector3 pos)
        {
            RaycastHit hit;

            if (Physics.Raycast(pos, Vector3.forward, out hit))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        public int getTileCount()
        {
            return tileset.Count;
        }
    }
}