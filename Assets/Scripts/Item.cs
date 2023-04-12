using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] [Tooltip("Monetary value for the item")] private float value;
    [SerializeField] [Tooltip("Time in seconds until the item respawns after being stolen")] private float respawnTime = 10f;

    private bool _mouseOver;
    private bool _canSteal;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        GameManager.Instance.OnInteract += OnInteract;
        GameManager.Instance.OnMouseMove += OnMouseMove;
    }

    private void OnMouseMove(Vector2 pos)
    {
        if (!_renderer.enabled) return;
        
        // Get the mouse's position in our scene
        var mousePosInWorld = Camera.main.ScreenToWorldPoint(pos);
        mousePosInWorld.z = transform.position.z;

        // Calculate the distance from the mouse to the narrator
        var distance = Vector2.Distance(mousePosInWorld, transform.position);

        // Is distance within a determined offset?
        if (distance >= -0.5f && distance <= 0.5f) 
        {
            // Mouse is over object
            _mouseOver = true;
            
            // Change Color to Yellow
            _renderer.color = Color.yellow;
        }
        else
        {
            _mouseOver = false;
            _renderer.color = Color.white;
        }

        var distanceToPlayer = Vector2.Distance(Player.Instance.transform.position, transform.position);
        _canSteal = distanceToPlayer >= -2.5f && distanceToPlayer <= 2.5f;
    }

    private void OnInteract()
    {
        if (_mouseOver && _renderer.enabled)
        {
            if (_canSteal)
            {
                if (Boss.Instance.IsPlayerInRadius)
                {
                    Debug.Log("Player was caught stealing");
                }
                else
                {
                    Player.Instance.Money += value;
                    Player.Instance.ItemsStolen++;
                    _renderer.enabled = false;
                    StartCoroutine(RespawnItem());
                }
            }
            else
            {
                Debug.Log("Player needs to get closer");
            }
        }
    }

    private IEnumerator RespawnItem()
    {
        yield return new WaitForSeconds(respawnTime);
        _renderer.enabled = true;
    }
}
