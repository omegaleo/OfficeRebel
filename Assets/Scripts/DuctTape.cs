using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuctTape : MonoBehaviour
{
    [SerializeField] private Vector2 _initialPosition = Vector2.zero;
    [SerializeField] private float _respawnTime = 60f;
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
        if (!_renderer.enabled) return;
        
        // Get the mouse's position in our scene
        var mousePosInWorld = Camera.main.ScreenToWorldPoint(pos);
        mousePosInWorld.z = transform.position.z;

        // Calculate the distance from the mouse to the narrator
        var distance = Vector2.Distance(mousePosInWorld, transform.position);

        var mouseOver = (distance >= -0.5f && distance <= 0.5f);

        if ((mouseOver || _moving) && GameManager.Instance.IsInteracting)
        {
            _renderer.color = Color.yellow;
            transform.position = mousePosInWorld;

            if (_resetting)
            {
                _resetting = false;
                StopAllCoroutines();
            }

            _moving = true;
        }
        else if (!GameManager.Instance.IsInteracting && _moving)
        {
            _renderer.color = Color.white;
            _moving = false;

            if (transform.position.ToVector2() != _initialPosition && !_resetting)
            {
                var distanceToNarrator = Vector3.Distance(transform.position, Narrator.Instance.transform.position);

                if (distanceToNarrator is >= -0.5f and <= 0.5f)
                {
                    Narrator.Instance.DuctTape();
                    _renderer.enabled = false;
                    StartCoroutine(Respawn());
                }
                
                transform.position = _initialPosition;
            }
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_respawnTime);
        _renderer.enabled = true;
    }
}
