using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    Grid worldGrid;

    Tilemap ground;
    Tilemap background;
    
    [SerializeField]
    int rows;
    [SerializeField]
    int columns;

    [SerializeField]
    int roomWidth;
    [SerializeField]
    int roomHeight;

    [SerializeField]
    GameObject roomPrefab;

    GameObject[,] generatedRooms;

    void Start()
    {
        ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        background = GameObject.Find("Background").GetComponent<Tilemap>();

        GenerateRoomObjects();
        GeneratePath();
        Generate();
    }

    void GenerateRoomObjects() {
        generatedRooms = new GameObject[columns, rows];
        for(int row = 0; row < rows; row++) {
            for(int column = 0; column < columns; column++) {
                GameObject generatedRoomGameObject = Instantiate (roomPrefab, new Vector3Int(column * roomWidth, row * roomHeight, 0), Quaternion.identity) as GameObject;
                generatedRoomGameObject.transform.SetParent(transform);
                GeneratedRoom room = generatedRoomGameObject.GetComponent<GeneratedRoom>();
                room.roomRow = row;
                room.roomColumn = column;
                room.roomHeight = roomHeight;
                room.roomWidth = roomWidth;
                room.LevelGenerator = this;
                generatedRooms[column, row] = generatedRoomGameObject;
            }
        }
    }

    void GeneratePath() {
        int pathBuilders = 4;
        for(int path = 0; path <= pathBuilders; path++) {

            int pathLength = Random.Range((rows + columns) / 4 * 3, (rows + columns));

            int currentX = (int)Mathf.Ceil(columns / 2f) - 1;
            int currentY = (int)Mathf.Ceil(rows / 2f) - 1;

            GetGeneratedRoom(currentX, currentY).roomType = 1;

            for(int pathCount = 0; pathCount < pathLength; pathCount++) {
                int direction = Random.Range(0, 6);
                if(direction == 0 || ((direction == 4 || direction == 5) && path == 0)) { //up
                    if(currentY < rows -1) {
                        GetGeneratedRoom(currentX, currentY).pathUp = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                        currentY = currentY + 1;
                        GetGeneratedRoom(currentX, currentY).pathDown = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                    }
                }
                else if(direction == 1 || ((direction == 4 || direction == 5) && path == 1)) {// down
                    if(currentY > 0) {
                        GetGeneratedRoom(currentX, currentY).pathDown = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                        currentY = currentY - 1;
                        GetGeneratedRoom(currentX, currentY).pathUp = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                    }
                }
                else if(direction == 2 || ((direction == 4 || direction == 5) && path == 2)) {// left
                    if(currentX > 0) {
                        GetGeneratedRoom(currentX, currentY).pathLeft = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                        currentX = currentX - 1;
                        GetGeneratedRoom(currentX, currentY).pathRight = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                    }
                }
                else if(direction == 3 || ((direction == 4 || direction == 5) && path == 3)) {// right
                    if(currentX < columns -1) {
                        GetGeneratedRoom(currentX, currentY).pathRight = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                        currentX = currentX + 1;
                        GetGeneratedRoom(currentX, currentY).pathLeft = true;
                        GetGeneratedRoom(currentX, currentY).roomActive = true;
                    }
                }
            }
        }
    }

    void Generate() {
        for(int row = 0; row < rows; row++) {
            for(int column = 0; column < columns; column++) {
                generatedRooms[column, row].GetComponent<GeneratedRoom>().Generate();
            }
        }
    }

    GeneratedRoom GetGeneratedRoom(int posX, int posY) {
        return generatedRooms[posX, posY].GetComponent<GeneratedRoom>();
    }

    public Tilemap GetGroundTileMap() {
        return ground;
    }

    public Tilemap GetBackgroundTileMap() {
        return background;
    }
}
