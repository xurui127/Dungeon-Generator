using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]private Tilemap floorTilemap;


    [SerializeField]private TileBase floorTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {

        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        // Convert tile from local position to world position
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);

        // Set tile to the scene
        tilemap.SetTile(tilePosition, tile); 
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
    }
}
