using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    public static List<TilemapNode> FindPath(NodeGraph graph, Vector2Int start, Vector2Int end)
    {
        // Create the open and closed sets
        List<TilemapNode> openSet = new List<TilemapNode>();
        HashSet<TilemapNode> closedSet = new HashSet<TilemapNode>();

        if (graph == null)
        {
            return new List<TilemapNode>();
        }
        
        // Add the start node to the open set
        TilemapNode startNode = graph.GetNode(start);
        openSet.Add(startNode);

        // Loop until we have found the end node or there are no more nodes to check
        while (openSet.Count > 0)
        {
            // Get the node in the open set with the lowest f score
            TilemapNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fScore < currentNode.fScore || (openSet[i].fScore == currentNode.fScore && openSet[i].hScore < currentNode.hScore))
                {
                    currentNode = openSet[i];
                }
            }

            // Remove the current node from the open set and add it to the closed set
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // If we have found the end node, reconstruct the path and return it
            if (currentNode?.position == end)
            {
                return ReconstructPath(currentNode);
            }

            // Loop through the current node's neighbors
            foreach (TilemapNode neighbor in graph.GetNeighbors(currentNode))
            {
                // If the neighbor is in the closed set, skip it
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                // Calculate the neighbor's tentative g score
                int tentativeGScore = currentNode.gScore + graph.GetCost(currentNode.position, neighbor.position);

                // If the neighbor is not in the open set, add it
                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                // If the tentative g score is greater than or equal to the neighbor's g score, skip it
                else if (tentativeGScore >= neighbor.gScore)
                {
                    continue;
                }

                // This path is the best we have found so far, so record it
                neighbor.cameFrom = currentNode;
                neighbor.gScore = tentativeGScore;
                neighbor.hScore = HeuristicCostEstimate(neighbor.position, end);
                neighbor.fScore = neighbor.gScore + neighbor.hScore;
            }
        }

        // If we reach this point, there is no path to the end node
        return null;
    }

    private static List<TilemapNode> ReconstructPath(TilemapNode endNode)
    {
        List<TilemapNode> path = new List<TilemapNode>();
        TilemapNode currentNode = endNode;

        while (currentNode != null)
        {
            path.Insert(0, currentNode);
            currentNode = currentNode.cameFrom;
        }

        return path;
    }

    private static int HeuristicCostEstimate(Vector2Int start, Vector2Int end)
    {
        return Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
    }
}