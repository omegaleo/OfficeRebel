using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeGraph
{
    private Dictionary<Vector2Int, TilemapNode> nodes = new Dictionary<Vector2Int, TilemapNode>();

    public NodeGraph(Tilemap tilemap)
    {
        nodes.Clear();

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3 worldPosition = tilemap.CellToWorld(position);
            Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));

            if (tilemap.HasTile(position))
            {
                TilemapNode node = new TilemapNode(gridPosition);
                AddNode(gridPosition, node);
            }
        }

        foreach (Vector2Int position in nodes.Keys)
        {
            TilemapNode node = nodes[position];
            List<TilemapNode> neighbors = new List<TilemapNode>();

            foreach (Vector2Int direction in new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left })
            {
                Vector2Int neighborPosition = position + direction;
                TilemapNode neighbor = GetNode(neighborPosition);

                if (neighbor != null)
                {
                    neighbors.Add(neighbor);
                }
            }

            node.neighbors = neighbors;
        }
    }

    public void AddNode(Vector2Int position, TilemapNode node)
    {
        nodes.Add(position, node);
    }

    public TilemapNode GetNode(Vector2Int position)
    {
        TilemapNode node;
        nodes.TryGetValue(position, out node);

        if (node == null)
        {
            return GetClosestNodeToPosition(position);
        }
        
        return node;
    }

    private TilemapNode GetClosestNodeToPosition(Vector2Int position)
    {
        var node = nodes.First();

        var previousDistance = Vector2Int.Distance(position, node.Key);
        
        foreach (var n in nodes)
        {
            var distance = Vector2Int.Distance(position, n.Key);

            if (distance < previousDistance)
            {
                node = n;
                previousDistance = distance;
            }
        }

        return node.Value;
    }
    
    public int GetNodeCount() => nodes.Count;

    public List<TilemapNode> GetNeighbors(TilemapNode node)
    {
        return node?.neighbors ?? new List<TilemapNode>();
    }

    public int GetCost(Vector2Int fromPosition, Vector2Int toPosition)
    {
        TilemapNode fromNode = GetNode(fromPosition);
        TilemapNode toNode = GetNode(toPosition);

        if (fromNode != null && toNode != null)
        {
            int dx = Mathf.Abs(fromNode.position.x - toNode.position.x);
            int dy = Mathf.Abs(fromNode.position.y - toNode.position.y);

            if (dx > dy)
            {
                return 14 * dy + 10 * (dx - dy);
            }
            else
            {
                return 14 * dx + 10 * (dy - dx);
            }
        }

        return int.MaxValue;
    }
}