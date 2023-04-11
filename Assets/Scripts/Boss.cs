using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss : InstancedBehaviour<Boss>
{
    [SerializeField] private BossRadius _radius;

    public bool IsPlayerInRadius => (_radius != null) && _radius.isPlayerInRadius;

    #region PathFinding + Movement

    [SerializeField] private List<GameObject> _targets = new List<GameObject>();

    private GameObject _target;
    private Vector2Int _targetPosition;
    private Vector2Int _currentPosition;
    private NodeGraph _nodeGraph;
    private List<TilemapNode> _path;
    private bool _moving;
    private IEnumerator Move()
    {
        while (_moving)
        {
            // Move the NPC along the path
            if (_path?.Count > 0)
            {
                TilemapNode nextNode = _path.First();
                transform.position = new Vector3(nextNode.position.x, nextNode.position.y, transform.position.z);
                _currentPosition = (Vector2Int)TilemapManager.Instance.groundTilemap.WorldToCell(transform.position);
                
                _path.RemoveAt(0);
            }

            var distance = Vector2.Distance(transform.position, _target.transform.position);
            if (distance >= -1f && distance<= 1f)
            {
                yield return SetTargetPosition();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator SetTargetPosition()
    {
        _target = _targets.Where(x => x != _target).ToList().Random();
        _targetPosition = (Vector2Int)TilemapManager.Instance.groundTilemap.WorldToCell(_target.transform.position);

        while (TilemapManager.Instance.groundNodes == null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        if (_nodeGraph == null || _nodeGraph.GetNodeCount() == 0)
        {
            _nodeGraph = TilemapManager.Instance.groundNodes;
        }
        
        // Find the shortest path from the NPC's current position to the target position
        _path = AStar.FindPath(_nodeGraph, _currentPosition, _targetPosition);

        if (!_moving)
        {
            _moving = true;
            StartCoroutine(Move());
        }
    }
    
    #endregion

    private void Start()
    {
        _targets.Add(Player.Instance.gameObject);
        _targets.AddRange(GameObject.FindGameObjectsWithTag("Employees"));
        StartCoroutine(SetTargetPosition());
        _currentPosition = (Vector2Int)TilemapManager.Instance.groundTilemap.WorldToCell(transform.position);
    }
}