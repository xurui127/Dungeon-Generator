using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]private int minRoomWidth = 4;
    [SerializeField]private int minRoomHeight = 4;

    [SerializeField]private int dungeonWidth = 20;
    [SerializeField] private int dungeonHeight = 20;

    [SerializeField][Range(0,10)] private int offset = 1;
    [SerializeField] bool randomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        // To split the room with random dungeon width and height
        var roomList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
                                                                              new Vector3Int(dungeonWidth, dungeonHeight, 0)),
                                                                              minRoomWidth, minRoomHeight);

        // Add each ground to the hashset
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomList);
        
        // Find each room center point
        List<Vector2Int> roomCenters = new();
        foreach (var room in roomList)
        {
            // Add to room centers list
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));    
        }
        // connect each room using room center positions
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);
        // end Find center point


        // Paint ground tiles
        tilemapVisualizer.PaintFloorTiles(floor);
        // generate wall postion and paint it
        WallGenerator.CreateWalls(floor,tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new();

        // randomly pick one room 
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        // Remove the first picked room 
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            // Find the closest room 
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            // Remove Closest room center
            roomCenters.Remove(closest);

            // Create a corridor from current position to the closest room position
            HashSet<Vector2Int> newCorrior = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorrior);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int distination)
    {
        HashSet<Vector2Int> corridor = new();
        var position = currentRoomCenter;
        corridor.Add(position);
        // if positions not reach the distination y , means not reach the closest room center position
        // and distination is from up or down side of the current room 
        while (position.y != distination.y)
        {
            //Check distination y axis is bigger than current position 
            if (distination.y > position.y)
            {
                position += Vector2Int.up;
            }
            //Check distination y axis is smaller than current position 
            else if (distination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        // if positions not reach the distination x , means not reach the closest room center position
        // and distination is from left or right side of the current room  
        while (position.x != distination.x)
        {
            //Check distination x axis is bigger than current position 
            if (position.x < distination.x)
            {
                position += Vector2Int.right;
            }
            //Check distination y axis is smaller than current position 
            else if (position.x > distination.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float currentLength = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < currentLength)
            {
                currentLength = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new();
        foreach (var room in roomList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;   
    }
}
