using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratedRoom : MonoBehaviour
{

    public int roomWidth{ get; set; }
    public int roomHeight{ get; set; }

    public int roomRow { get; set; }
    public int roomColumn { get; set; }

    public bool roomActive { get; set; } = false;

    public bool pathUp { get; set; } = false;
    public bool pathDown { get; set; } = false;
    public bool pathLeft { get; set; } = false;
    public bool pathRight { get; set; } = false;

    public int roomType{ get; set; } // start, boss, item, lever
    
    int[,] roomLayout;

    public LevelGenerator LevelGenerator { get; set; }

    PolygonCollider2D polyCollider;

    void Start()
    {
    }

    public void Generate() {
        if(roomActive) {
            roomLayout = new int[roomWidth, roomHeight];
            for(int row = 0; row < roomHeight; row++) {
                for(int column = 0; column < roomWidth; column++) {
                    if(IsBorder(column, row) && !IsLevelTransitionGap(column, row)) {
                        roomLayout[column, row] = 1;
                    }
                    else if(roomType == 1 && Center(column, row)) {
                        roomLayout[column, row] = 1;
                    }
                    else {
                        roomLayout[column, row] = 0;
                    }
                }
            }
            UpdateTiles();
            UpdatePolygonCollider2D();
        }
    }

    void UpdateTiles() {
        int positionX = roomColumn * roomWidth;
        int positionY = roomRow * roomHeight;
        
        for(int row = 0; row < roomHeight; row++) {
            for(int column = 0; column < roomWidth; column++) {
                int adjustedX = positionX + column;
                int adjustedY = positionY + row;
                if(roomLayout[column, row] == 1){
                    LevelGenerator.GetGroundTileMap().SetTile(new Vector3Int(adjustedX, adjustedY, 0), getTileFromFolder(column, row, "Ground"));
                }
            }
        }
    }
    
    TileBase getTileFromFolder(int x, int y, string type) {
        
        string covering = GetCovering(x, y);
        Object[] tileBases = Resources.LoadAll("Tiles/" + type + "/" + covering, typeof(TileBase));
        if(tileBases.Length == 0) {
            return (TileBase)Resources.LoadAll("Tiles/Unknown", typeof(TileBase))[0];
        }
        return (TileBase)tileBases[Random.Range(0, tileBases.Length)];
    }

    string GetCovering(int x, int y) {
        string covering = "";
        if(y + 1 < roomHeight -1 && roomLayout[x, y + 1] == 0) {
            covering = covering + "T";
        }
        if(y - 1 >= 0 && roomLayout[x, y - 1] == 0) {
            covering = covering + "B";
        }
        if(x - 1 >= 0 && roomLayout[x - 1, y] == 0) {
            covering = covering + "L";
        }
        if(x + 1 < roomWidth -1 && roomLayout[x + 1, y] == 0){
            covering = covering + "R";
        }

        if(covering== "") {
            return "Center";
        }
        return covering;

    }

    bool IsBorder(int column, int row) {
        return (row == 0 || row == roomHeight -1 || column == 0 || column == roomWidth -1);
    }

    bool IsLevelTransitionGap(int column, int row) {
        if(IsBorder(column, row)) {
        
            if(pathUp && row == roomHeight - 1 && column == Mathf.Ceil(roomWidth / 2) - 1) {
                return true;
            }
            if(pathDown && row == 0 && column == Mathf.Ceil(roomWidth / 2) - 1) {
                return true;
            }
            if(pathLeft && row == Mathf.Ceil(roomHeight / 2) - 1 && column == 0) {
                return true;
            }
            if(pathRight && row == Mathf.Ceil(roomHeight / 2) - 1 && column == roomWidth - 1 ) {
                return true;
            }
        }
        return false;
    }

    bool Center(int column, int row) {
        return (column == Mathf.Ceil(roomWidth / 2) && row == Mathf.Ceil(roomHeight / 2));
    }

    void UpdatePolygonCollider2D() {
        //Only does a single room. Does not merge multiple rooms
        polyCollider = GetComponent<PolygonCollider2D>();

        float positionX = 0.5f;
        float positionY = 0.5f;

        Vector2 point1 = new Vector2(positionX, positionY);
        Vector2 point2 = new Vector2(positionX, positionY + roomHeight);
        Vector2 point3 = new Vector2(positionX + roomWidth, positionY + roomHeight);
        Vector2 point4 = new Vector2(positionX + roomWidth, positionY);

        polyCollider.points = new[]{point1,point2,point3,point4};
    }
}
