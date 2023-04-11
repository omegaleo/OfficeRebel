using System.Collections.Generic;
using UnityEngine;


public class TilemapNode
{
    public Vector2Int position;
    public TilemapNode cameFrom;
    public int gScore;
    public int hScore;
    public int fScore;
    public List<TilemapNode> neighbors;

    public TilemapNode(Vector2Int position)
    {
        this.position = position;
        this.neighbors = new List<TilemapNode>();
    }
}