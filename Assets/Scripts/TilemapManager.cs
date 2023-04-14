using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TilemapManager : InstancedBehaviour<TilemapManager>
{
    public NodeGraph groundNodes => new NodeGraph(groundTilemap);

    [Header("Tilemaps")] 
    public Tilemap groundTilemap;

    public List<Vector2Int> ObstaclePositions;

    public TilemapNode GetNodeAtPosition(Vector3 pos)
    {
        var targetPos = (Vector2Int)groundTilemap.WorldToCell(pos);

        return groundNodes.GetNode(targetPos);
    }

    public void SetObstacleAtPosition(Vector3 pos)
    {
        ObstaclePositions.Add((Vector2Int)groundTilemap.WorldToCell(pos));
    }

    public void RemoveObstacleAtPosition(Vector3 pos)
    {
        var targetPos = (Vector2Int)groundTilemap.WorldToCell(pos);

        if (IsObjectAtPosition(targetPos))
        {
            ObstaclePositions.RemoveAll(x => x == targetPos);
        }
    }

    public bool IsObjectAtPosition(Vector2Int pos) => ObstaclePositions.Any(x => x == pos);
}
