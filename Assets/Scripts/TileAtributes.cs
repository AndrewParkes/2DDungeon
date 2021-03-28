using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAtributes : MonoBehaviour
{

    [SerializeField]
    TileBase tile;

    [SerializeField]
    bool coveringT;
    [SerializeField]
    bool coveringB;
    [SerializeField]
    bool coveringL;
    [SerializeField]
    bool coveringR;
}
