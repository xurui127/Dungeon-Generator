using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions,TilemapVisualizer tilemapVisualizer)
    {
        // Paint basic walls for cardinal direction (Up,Right,Down,Left)
        var basicWallPosition = FindWallsInDirection(floorPositions, Direction2D.cardinalDirectionList);
        // Paint coner walls for diagonal direction ( Up- Right, Right - Down,Down - Left,Left - Up)
        var cornerWallPosition = FindWallsInDirection(floorPositions, Direction2D.diagonalDirectionList);
        CreateBasicWall(tilemapVisualizer, basicWallPosition,floorPositions);
        CreateCornerWall(tilemapVisualizer, cornerWallPosition, floorPositions);
    }

    private static void CreateCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPosition)
        {
            string neighborsBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighborPosition = position + direction;
                if (floorPositions.Contains(neighborPosition))
                {
                    neighborsBinaryType += "1";
                }
                else
                {
                    neighborsBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighborsBinaryType);
        }

    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPosition)
        {
            string neighborsBinaryType = "";

            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                var neighbotPosition = position + direction;
                // Check if floor position contains neighbor position
                if (floorPositions.Contains(neighbotPosition))
                {
                    // And string 1 , means there is a floor
                    neighborsBinaryType += "1";
                }
                else
                {
                    // if not, means there is no floors
                    neighborsBinaryType += "0";
                }
            }
            // Start paint cardinaDirection walls 
            tilemapVisualizer.PaintSingleBasicWall(position,neighborsBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                
                var neighborPosition = position + direction;
                //if neighbor is not contained means it is wall position
                if (floorPositions.Contains(neighborPosition) == false)
                {
                    wallPositions.Add(neighborPosition);
                }
            }
        }

        return wallPositions;
    }
}
