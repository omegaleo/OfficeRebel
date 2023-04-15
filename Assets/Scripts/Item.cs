using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    
    private float _value;
    private float _respawnTime = 10f;
    private bool _respawnAtStart = false;
    
    private bool _mouseOver;
    private bool _canSteal;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        var item = GameManager.Instance.ItemAssociations.FirstOrDefault(x => x.Type == _type);

        _respawnAtStart = new List<bool>() { true, false }.Random();
        
        if (item != null)
        {
            _renderer.sprite = item.Texture;
            _value = item.Value;
            _respawnTime = item.RespawnTime;
        }
        
        if (!_respawnAtStart)
        {
            _renderer.enabled = false;
            StartCoroutine(RespawnItem());
        }
        
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
                    HUDManager.Instance.GameOver();
                }
                else
                {
                    Player.Instance.Money += _value;
                    Player.Instance.ItemsStolen++;

                    if (Player.Instance.StoleItem.GetInvocationList().Any())
                    {
                        Player.Instance.StoleItem.Invoke();
                    }
                    
                    _renderer.enabled = false;
                    StartCoroutine(RespawnItem());
                }
            }
            else
            {
                AudioController.Instance.PlaySoundEffect(SoundEffectType.Error);
            }
        }
        else if (!_renderer.enabled)
        {
            _mouseOver = false;
        }
    }

    private IEnumerator RespawnItem()
    {
        yield return new WaitForSeconds(_respawnTime);
        _renderer.enabled = true;
    }
}
