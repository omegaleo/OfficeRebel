using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ITDoor : InstancedBehaviour<ITDoor>
{
    private TilemapCollider2D _collider;
    private TilemapRenderer _renderer;
    
    void Start()
    {
        _renderer = GetComponent<TilemapRenderer>();
        _collider = GetComponent<TilemapCollider2D>();
        
        _renderer.enabled = false;
        _collider.enabled = false;

        GameManager.Instance.OnSuspicionIncreased += SetDoor;
    }

    public void SetDoor()
    {
        if (GameManager.Instance.Suspicion >= 5)
        {
            _renderer.enabled = true;
            _collider.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
            _collider.enabled = false;
        }
    }
}
