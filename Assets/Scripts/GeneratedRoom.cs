using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratedRoom : MonoBehaviour
{

    [SerializeField]
    GameObject resourceCache;

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
            UpdateGroundTiles();
            UpdateBackGroundTiles();
            UpdatePolygonCollider2D();
        }
    }

    private void UpdateGroundTiles() {
        int positionX = roomColumn * roomWidth;
        int positionY = roomRow * roomHeight;
        
        for(int row = 0; row < roomHeight; row++) {
            for(int column = 0; column < roomWidth; column++) {
                int adjustedX = positionX + column;
                int adjustedY = positionY + row;
                if(roomLayout[column, row] == 1){
                    LevelGenerator.GetGroundTileMap().SetTile(new Vector3Int(adjustedX, adjustedY, 0), GetTileFromFolder("Tiles/Ground/" + GetCoveringGround(column, row)));
                }
            }
        }
    }

    private void UpdateBackGroundTiles() {
        int positionX = roomColumn * roomWidth;
        int positionY = roomRow * roomHeight;
        
        for(int row = 0; row < roomHeight; row++) {
            for(int column = 0; column < roomWidth; column++) {
                int adjustedX = positionX + column;
                int adjustedY = positionY + row;
                if(roomLayout[column, row] == 0){
                    LevelGenerator.GetBackgroundTileMap().SetTile(new Vector3Int(adjustedX, adjustedY, 0), GetTileFromFolder("Tiles/Background/" + GetCoveringBackGround(column, row)));
                }
            }
        }
    }
    
    TileBase GetTileFromFolder(string type) {
        return resourceCache.GetComponent<ResourceCache>().GetTileFromFolder(type);
    }

    string GetCoveringGround(int x, int y) {
        string covering = "";
        if(roomLayoutCheckUp(x, y, 0)) {
            covering = covering + "T";
        }
        if(roomLayoutCheckDown(x, y, 0)) {
            covering = covering + "B";
        }
        if(roomLayoutCheckLeft(x, y, 0)) {
            covering = covering + "L";
        }
        if(roomLayoutCheckRight(x, y, 0)){
            covering = covering + "R";
        }

        if(covering== "") {
            return "Center";
        }
        return covering;

    }

    string GetCoveringBackGround(int x, int y) {
        string covering = "";
        if(roomLayoutCheckUp(x, y, 1)) {
            covering = covering + "T";
        }
        if(roomLayoutCheckDown(x, y, 1)) {
            covering = covering + "B";
        }
        if(roomLayoutCheckLeft(x, y, 1)) {
            covering = covering + "L";
        }
        if(roomLayoutCheckRight(x, y, 1)){
            covering = covering + "R";
        }

        if(covering == "" || covering == "LR" || covering == "TB" ) {
            return "Center";
        }
        return covering;

    }

    bool roomLayoutCheckUp(int x, int y, int roomType) {
        return (y + 1 < roomHeight && roomLayout[x, y + 1] == roomType);
    }
    
    bool roomLayoutCheckDown(int x, int y, int roomType) {
        return (y - 1 >= 0 && roomLayout[x, y - 1] == roomType);
    }
    
    bool roomLayoutCheckLeft(int x, int y, int roomType) {
        return (x - 1 >= 0 && roomLayout[x - 1, y] == roomType);
    }
    
    bool roomLayoutCheckRight(int x, int y, int roomType) {
        return (x + 1 < roomWidth && roomLayout[x + 1, y] == roomType);
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
        //polyCollider = transform.Find("CM vcam").GetComponent<PolygonCollider2D>();
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
