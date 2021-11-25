using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    public MapGrid grid;
    List<List<int>> noise_grid = new List<List<int>>();
    Dictionary<int, GameObject> tile_groups;
    Dictionary<int, GameObject> tileset;
    public GameObject dirtPrefab;
    public GameObject greensPrefab;
    public GameObject stonePrefab;
    public GameObject waterPrefab;
    public void CreateTileset()
    {
        /** Collect and assign ID codes to the tile prefabs, for ease of access.
            Best ordered to match land elevation. **/

        tileset = new Dictionary<int, GameObject>();
        tileset.Add(0, dirtPrefab);
        tileset.Add(1, greensPrefab);
        tileset.Add(2, stonePrefab);
        tileset.Add(3, waterPrefab);
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
    public void CreateTile(int tile_id, int x, int y)
    {
        /** Creates a new tile using the type id code, group it with common
            tiles, set it's position and store the gameobject. **/

        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);

    }
    // public void SetTileTaken(Vector3 pos)
    // {
    //     RaycastHit hit;
    //     Ray ray = Camera.main.ScreenPointToRay(pos);

    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         Destroy(hit.transform.gameObject);
    //     }
    // }

    public void SetTileTaken(Vector3 pos)
    {
        RaycastHit hit;
        // Ray ray = Camera.main.ScreenPointToRay(pos);

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