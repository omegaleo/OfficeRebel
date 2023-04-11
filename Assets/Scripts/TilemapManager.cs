using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class TilemapManager : InstancedBehaviour<TilemapManager>
{
    public NodeGraph groundNodes;

    [Header("Tilemaps")] 
    public Tilemap groundTilemap;
    
    // Start is called before the first frame update
    void Start()
    {
        groundNodes = new NodeGraph(groundTilemap);
    }
}
