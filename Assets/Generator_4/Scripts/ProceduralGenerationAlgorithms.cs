using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLenght)
    {
        HashSet<Vector2Int> path = new();

        path.Add(startPosition);
        var previousPostion = startPosition;

        for (int i = 0; i < walkLenght; i++)
        {
            var newPosition = previousPostion + Direction2D.GetCardinalDirecton();

            path.Add(newPosition);
            previousPostion = newPosition;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new();

        var direction = Direction2D.GetCardinalDirecton();

        var currentPostion = startPosition;
        corridor.Add(currentPostion);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPostion += direction;
            corridor.Add(currentPostion);
        }

        return corridor;
    }
}

