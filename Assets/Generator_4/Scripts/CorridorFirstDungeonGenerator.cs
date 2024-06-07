using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{

    [SerializeField]private int corridorLength = 14;
    [SerializeField]private int corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1)] private float roomPercent = 0.8f;

   
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new(); 
        
        HashSet<Vector2Int> potentialRoomPositions = new();
        
        CreateCorridors(floorPositions, potentialRoomPositions);
        
        HashSet<Vector2Int> roomPostions = CreateRooms(potentialRoomPositions);
        
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        
        CreateRoomsAtDeadEnd(deadEnds, roomPostions);
        
        floorPositions.UnionWith(roomPostions);
        
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
       foreach (Vector2Int position in deadEnds) 
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new();
        foreach (var position in floorPositions)
        {
            int neighborsCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighborsCount++;
               
                }
            }
            // found end position
            if (neighborsCount == 1)
            {
                deadEnds.Add(position);
            }
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        // OrderBy: Sorts the elements of a sequence in ascending order.
        // Take: Returns a specified number of contiguous elements from the start of a sequence.
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x=>Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count -1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}
