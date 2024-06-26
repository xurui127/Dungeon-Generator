using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public string id;
    public string templateID;
    public GameObject prefab;
    public RoomNodeTypeSO roomNodeType;
    public Vector2Int lowerBounds;
    public Vector2Int upperBounds;
    public Vector2Int templateLowerBounds;
    public Vector2Int templateUpperBounds;
    public Vector2Int[] spawnPositionArray;
    public List<string> childRoomIDList;
    public string parentRoomID;
    public List<Doorway> doorWayList;
    public InstantiatedRoom instantiatedRoom;
    public bool isPositioned = false;
    public bool isLit = false;
    public bool isClearedOfEnemies = false;
    public bool isPreviouslyVistied = false;

    public Room()
    {
        childRoomIDList = new();
        doorWayList = new();
    }

}
