using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceCache : MonoBehaviour
{
    private Dictionary <string, Object[]> resourceCache = new Dictionary<string, Object[]>();

    private const string Unknown = "Tiles/Unknown";

    public TileBase GetTileFromFolder(string type) {
        
        if(resourceCache.ContainsKey(type)) {
            return (TileBase)resourceCache[type][Random.Range(0, resourceCache[type].Length)];
        }

        Object[] tileBases = Resources.LoadAll(type, typeof(TileBase));
        if(tileBases.Length == 0) {
            return GetTileFromFolder(Unknown);
        }
        resourceCache.Add(type, tileBases);
        return (TileBase)resourceCache[type][Random.Range(0, resourceCache[type].Length)];
    }
}
