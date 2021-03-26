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
    int cellWidth;
    [SerializeField]
    int cellHeight;

    int levelHeight;
    int levelWidth;

    Tilemap ground;
    Tilemap background;


    // Start is called before the first frame update
    void Start()
    {
        levelHeight = cellRows * cellHeight + 1;
        levelWidth = cellColumns * cellWidth + 1;
        ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        background = GameObject.Find("Background").GetComponent<Tilemap>();
        BuildLevel();
    }

    void BuildLevel() {
        //build standard grid
        for(int row = 0; row < levelHeight; row++) {
            //int columnProbability = Random.Range(20, 40);
            for(int column = 0; column < levelWidth; column++) {
                if(row == 0 || row == levelHeight -1 || column == 0 || column == levelWidth -1 || row % cellHeight == 0 || column % cellWidth == 0) {
                    ground.SetTile(new Vector3Int(column, row, 0), groundTiles[0]);
                    continue;
                }

                /*if(GridCorner(row, column)) {
                    ground.SetTile(new Vector3Int(column, row, 0), groundTiles[0]);
                    continue;
                }*/

                /*bool hasLeft = (ground.GetTile(new Vector3Int(column - 1 , row, 0)) != null);
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
                }*/
            }
        }

        //create random paths
        int pathBuilders = 4;
        for(int path = 0; path <= pathBuilders; path++) {

            int pathLength = Random.Range((cellRows + cellColumns) / 4 * 3, (cellRows + cellColumns));

            int halfCellWidth = (int)Mathf.Ceil(cellWidth / 2f);
            int halfCellHeight = (int)Mathf.Ceil(cellHeight / 2f);
            int currentX = ((int)Mathf.Ceil(cellColumns / 2f) * cellWidth) - halfCellWidth;
            int currentY = ((int)Mathf.Ceil(cellRows / 2f) * cellHeight) - halfCellHeight;

            
            ground.SetTile(new Vector3Int(currentX, currentY, 0), groundTiles[0]);
            FillRoom(currentX, currentY);

            for(int pathCount = 0; pathCount < pathLength; pathCount++) {
                int direction = Random.Range(0, 6);
                if(direction == 0 || ((direction == 4 || direction == 5) && path == 0)) { //up
                    if(currentY + halfCellHeight != levelHeight -1) {// on boundry
                        ground.SetTile(new Vector3Int(currentX, currentY + halfCellHeight, 0), null);
                        background.SetTile(new Vector3Int(currentX, currentY + halfCellHeight, 0), backgroundTiles[0]);
                        currentY = currentY + cellHeight;
                        FillRoom(currentX, currentY);
                    }
                }
                else if(direction == 1 || ((direction == 4 || direction == 5) && path == 1)) {// down
                    if(currentY - halfCellHeight != 0) {// on boundry
                        ground.SetTile(new Vector3Int(currentX, currentY - halfCellHeight, 0), null);
                        background.SetTile(new Vector3Int(currentX, currentY - halfCellHeight, 0), backgroundTiles[0]);
                        currentY = currentY - cellHeight;
                        FillRoom(currentX, currentY);
                    }
                }
                else if(direction == 2 || ((direction == 4 || direction == 5) && path == 2)) {// left
                    if(currentX - halfCellWidth != 0) {// on boundry
                        ground.SetTile(new Vector3Int(currentX - halfCellWidth, currentY, 0), null);
                        background.SetTile(new Vector3Int(currentX - halfCellWidth, currentY, 0), backgroundTiles[0]);
                        currentX = currentX - cellWidth;
                        FillRoom(currentX, currentY);
                    }
                }
                else if(direction == 3 || ((direction == 4 || direction == 5) && path == 3)) {// right
                    if(currentX + halfCellWidth != levelWidth -1) {// on boundry
                        ground.SetTile(new Vector3Int(currentX + halfCellWidth, currentY, 0), null);
                        background.SetTile(new Vector3Int(currentX + halfCellWidth, currentY, 0), backgroundTiles[0]);
                        currentX = currentX + cellWidth;
                        FillRoom(currentX, currentY);
                    }
                }
            }
        }
    }

    bool GridCorner(int row, int column) {
        int rowMod = row % cellWidth;
        int columnMod = column % cellHeight;

        if(rowMod == 0 && (columnMod == 0 || columnMod == 1 || columnMod == 4 || columnMod == 5)) {
            return true;
        }
        if(rowMod == 1 && (columnMod == 0 || columnMod == 5)) {
            return true;
        }
        if(rowMod == 4 && (columnMod == 0 || columnMod == 5)) {
            return true;
        }
        /*if(rowMod == 7 && (columnMod == 0 || columnMod == 9)) {
            return true;
        }
        if(rowMod == 8 && (columnMod == 0 || columnMod == 9)) {
            return true;
        }
        if(rowMod == 9 && (columnMod == 0 || columnMod == 1 || columnMod == 2 || columnMod == 7 || columnMod == 8 || columnMod == 9)) {
            return true;
        }*/
        return false;
    }

    void FillRoom(int currentX, int currentY) {
        int startX = currentX - (int)Mathf.Floor(cellWidth / 2f) + 1;
        int startY = currentY - (int)Mathf.Floor(cellHeight / 2f) + 1;

        for(int row = 0; row <= cellHeight - 2; row++) {
            for(int column = 0; column <= cellWidth - 2; column++) {
                background.SetTile(new Vector3Int(startX + column, startY + row, 0), backgroundTiles[0]);
            }
        }
    }
}
