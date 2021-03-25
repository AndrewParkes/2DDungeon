using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateRoom : MonoBehaviour
{

    [SerializeField]
    TileBase[] groundTiles;
    [SerializeField]
    TileBase[] backgroundTiles;
    

    [SerializeField]
    Grid worldGrid;

    [SerializeField]
    int cellRows;
    [SerializeField]
    int cellColumns;
    [SerializeField]
    int cellSize;

    int levelHeight;
    int levelWidth;

    Tilemap ground;



    // Start is called before the first frame update
    void Start()
    {
        levelHeight = cellRows * cellSize;
        levelWidth = cellColumns * cellSize;
        ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        for(int row = 0; row < levelHeight; row++) {
            int columnProbability = Random.Range(20, 40);
            for(int column = 0; column < levelWidth; column++) {
                if(row == 0 || row == levelHeight -1 || column == 0 || column == levelWidth -1) {
                    ground.SetTile(new Vector3Int(column, row, 0), groundTiles[0]);
                    continue;
                }

                if(GridCorner(row, column)) {
                    ground.SetTile(new Vector3Int(column, row, 0), groundTiles[0]);
                    continue;
                }

                bool hasLeft = (ground.GetTile(new Vector3Int(column - 1 , row, 0)) != null);
                bool hasBellow = (ground.GetTile(new Vector3Int(column , row -1, 0)) != null);
                bool hasRight = (ground.GetTile(new Vector3Int(column + 1 , row, 0)) != null);
                bool hasAbove = (ground.GetTile(new Vector3Int(column , row + 1, 0)) != null);
                if(hasLeft || hasRight) {
                    columnProbability += 3;
                }
                if(hasBellow || hasAbove) {
                    columnProbability -= 3;
                }
                if( Random.Range(0, 100) <= columnProbability) {
                    ground.SetTile(new Vector3Int(column, row, 0), groundTiles[0]);
                    continue;
                }
            }
        }
    }

    bool GridCorner(int row, int column) {
        int rowMod = row % 10;
        int columnMod = column % 10;

        if(rowMod == 0 && (columnMod == 0 || columnMod == 1 || columnMod == 2 || columnMod == 7 || columnMod == 8 || columnMod == 9)) {
            return true;
        }
        if(rowMod == 1 && (columnMod == 0 || columnMod == 9)) {
            return true;
        }
        if(rowMod == 2 && (columnMod == 0 || columnMod == 9)) {
            return true;
        }
        if(rowMod == 7 && (columnMod == 0 || columnMod == 9)) {
            return true;
        }
        if(rowMod == 8 && (columnMod == 0 || columnMod == 9)) {
            return true;
        }
        if(rowMod == 9 && (columnMod == 0 || columnMod == 1 || columnMod == 2 || columnMod == 7 || columnMod == 8 || columnMod == 9)) {
            return true;
        }
        return false;
    }
}
