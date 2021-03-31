using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratedRoom : MonoBehaviour
{

    [SerializeField] private GameObject resourceCache;

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

    private int[,] roomLayout;

    public LevelGenerator levelGenerator { get; set; }

    private PolygonCollider2D polyCollider;

    public void Generate()
    {
        if (!roomActive)
        {
            return;
        }
        
        roomLayout = new int[roomWidth, roomHeight];
        for(var row = 0; row < roomHeight; row++) {
            for(var column = 0; column < roomWidth; column++) {
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

    private void UpdateGroundTiles() {
        var positionX = roomColumn * roomWidth;
        var positionY = roomRow * roomHeight;
        
        for(var row = 0; row < roomHeight; row++) {
            for(var column = 0; column < roomWidth; column++) {
                var adjustedX = positionX + column;
                var adjustedY = positionY + row;
                if(roomLayout[column, row] == 1){
                    levelGenerator.GetGroundTileMap().SetTile(new Vector3Int(adjustedX, adjustedY, 0), GetTileFromFolder("Tiles/Ground/" + GetCoveringGround(column, row)));
                }
            }
        }
    }

    private void UpdateBackGroundTiles() {
        var positionX = roomColumn * roomWidth;
        var positionY = roomRow * roomHeight;
        
        for(var row = 0; row < roomHeight; row++) {
            for(var column = 0; column < roomWidth; column++) {
                var adjustedX = positionX + column;
                var adjustedY = positionY + row;
                if(roomLayout[column, row] == 0){
                    levelGenerator.GetBackgroundTileMap().SetTile(new Vector3Int(adjustedX, adjustedY, 0), GetTileFromFolder("Tiles/Background/" + GetCoveringBackGround(column, row)));
                }
            }
        }
    }
    
    TileBase GetTileFromFolder(string type) {
        return resourceCache.GetComponent<ResourceCache>().GetTileFromFolder(type);
    }

    private string GetCoveringGround(int x, int y) {
        var covering = "";
        if(RoomLayoutCheckUp(x, y, 0)) {
            covering += "T";
        }
        if(RoomLayoutCheckDown(x, y, 0)) {
            covering += "B";
        }
        if(RoomLayoutCheckLeft(x, y, 0)) {
            covering += "L";
        }
        if(RoomLayoutCheckRight(x, y, 0)){
            covering += "R";
        }

        return covering== "" ? "Center" : covering;
    }

    private string GetCoveringBackGround(int x, int y) {
        var covering = "";
        if(RoomLayoutCheckUp(x, y, 1)) {
            covering += "T";
        }
        if(RoomLayoutCheckDown(x, y, 1)) {
            covering += "B";
        }
        if(RoomLayoutCheckLeft(x, y, 1)) {
            covering += "L";
        }
        if(RoomLayoutCheckRight(x, y, 1)){
            covering += "R";
        }

        if(covering == "" || covering == "LR" || covering == "TB" ) {
            return "Center";
        }
        return covering;

    }

    private bool RoomLayoutCheckUp(int x, int y, int tileType) {
        return (y + 1 < roomHeight && roomLayout[x, y + 1] == tileType);
    }

    private bool RoomLayoutCheckDown(int x, int y, int tileType) {
        return (y - 1 >= 0 && roomLayout[x, y - 1] == tileType);
    }

    private bool RoomLayoutCheckLeft(int x, int y, int tileType) {
        return (x - 1 >= 0 && roomLayout[x - 1, y] == tileType);
    }

    private bool RoomLayoutCheckRight(int x, int y, int tileType) {
        return (x + 1 < roomWidth && roomLayout[x + 1, y] == tileType);
    }

    private bool IsBorder(int column, int row) {
        return (row == 0 || row == roomHeight -1 || column == 0 || column == roomWidth -1);
    }

    private bool IsLevelTransitionGap(int column, int row) {
        if (!IsBorder(column, row)) return false;
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
        return false;
    }

    bool Center(int column, int row) {
        return (column == Mathf.Ceil(roomWidth / 2) && row == Mathf.Ceil(roomHeight / 2));
    }

    void UpdatePolygonCollider2D() {
        //Only does a single room. Does not merge multiple rooms
        //polyCollider = transform.Find("CM vcam").GetComponent<PolygonCollider2D>();
        polyCollider = GetComponent<PolygonCollider2D>();

        const float positionX = 0.5f;
        const float positionY = 0.5f;

        Vector2 point1 = new Vector2(positionX, positionY);
        Vector2 point2 = new Vector2(positionX, positionY + roomHeight);
        Vector2 point3 = new Vector2(positionX + roomWidth, positionY + roomHeight);
        Vector2 point4 = new Vector2(positionX + roomWidth, positionY);

        polyCollider.points = new[]{point1,point2,point3,point4};
    }
}
