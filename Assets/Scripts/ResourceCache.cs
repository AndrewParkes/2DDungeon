using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceCache : MonoBehaviour
{
    Dictionary <string, Object[]> resourceCache = new Dictionary<string, Object[]>();

    string unknown = "Tiles/Unknown";

    public TileBase GetTileFromFolder(string type) {
        
        if(resourceCache.ContainsKey(type)) {
            return (TileBase)resourceCache[type][Random.Range(0, resourceCache[type].Length)];
        }

        Object[] tileBases = Resources.LoadAll(type, typeof(TileBase));
        if(tileBases.Length == 0) {
            return GetTileFromFolder(unknown);
        }
        resourceCache.Add(type, tileBases);
        return (TileBase)resourceCache[type][Random.Range(0, resourceCache[type].Length)];
    }
}
