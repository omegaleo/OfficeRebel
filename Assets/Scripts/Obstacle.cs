using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Vector2 _initialPosition = Vector2.zero;
    [SerializeField] [Tooltip("Time in seconds until the obstacle goes back to it's original position")] private float _respawnTime = 20f;

    private bool _moving = false;
    private bool _resetting = false;

    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;

        _renderer = GetComponent<SpriteRenderer>();
        
        GameManager.Instance.OnMouseMove += OnMouseMove;
    }

    private void OnMouseMove(Vector2 pos)
    {
        // Get the mouse's position in our scene
        var mousePosInWorld = Camera.main.ScreenToWorldPoint(pos);
        mousePosInWorld.z = transform.position.z;

        // Calculate the distance from the mouse to the narrator
        var distance = Vector2.Distance(mousePosInWorld, transform.position);

        var mouseOver = (distance >= -0.5f && distance <= 0.5f);

        if (mouseOver && GameManager.Instance.IsInteracting)
        {
            _renderer.color = Color.yellow;
            transform.position = mousePosInWorld;

            if (_resetting)
            {
                _resetting = false;
                StopAllCoroutines();
            }
        }
        else
        {
            _renderer.color = Color.white;
            if (transform.position.ToVector2() != _initialPosition && !_resetting)
            {
                _resetting = true;
                StartCoroutine(ResetPosition());
                
                // Set the current position of the obstacle in the tilemap
                TilemapManager.Instance.SetObstacleAtPosition(transform.position);
            }
        }
    }

    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(_respawnTime);
        _resetting = false;
        TilemapManager.Instance.RemoveObstacleAtPosition(transform.position);
        transform.position = _initialPosition;
    }
}
