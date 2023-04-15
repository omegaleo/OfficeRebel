using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandBook : MonoBehaviour
{
    Image bookImage;
    Vector3 bookPosition;

    private bool _moving = false;
    private bool _resetting = false;
    [SerializeField] private Vector2 _initialPosition = Vector2.zero;

    [SerializeField] [Tooltip("Time in seconds until the obstacle goes back to it's original position")]
    private float _respawnTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.localPosition;
        bookImage = GetComponent<Image>();
        GameManager.Instance.OnMouseMove += OnMouseMove;
    }

    private void OnMouseMove(Vector2 pos)
    {
        //Get mouse's position on viewport
        var mousePosOnScreen = Camera.main.ScreenToWorldPoint(pos);
        mousePosOnScreen.z = transform.position.z;
        var distance = Vector2.Distance(mousePosOnScreen, transform.position);
        var mouseOver = (distance >= -0.6f && distance <= 0.6f);

        if ((mouseOver || _moving) && GameManager.Instance.IsInteracting)
        {
            bookImage.color = Color.yellow;
            transform.position = mousePosOnScreen;
            _moving = true;
        }
        else if (!GameManager.Instance.IsInteracting && _moving)
        {
            bookImage.color = Color.white;
            _moving = false;

            if (transform.localPosition.ToVector2() != _initialPosition)
            {
                if (_onTopOfBoss)
                {
                    AudioController.Instance.PlaySoundEffect(SoundEffectType.Bonk);
                    Boss.Instance.Bonk();
                
                    bookImage.enabled = false;
                    _resetting = true;
                    StartCoroutine(ResetPosition());
                }
                
                transform.localPosition = _initialPosition;
            }
        }
    }

    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(_respawnTime);
        _resetting = false;
    }

    private bool _onTopOfBoss = false;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            _onTopOfBoss = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            _onTopOfBoss = false;
        }
    }
}