using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRadius : MonoBehaviour
{
    public bool isPlayerInRadius;

    private CircleCollider2D _collider;
    private RectTransform _transform;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _transform = GetComponent<RectTransform>();
    }

    public void SetRadius(Vector3 radius)
    {
        // When suspicion is higher
        _transform.localScale = radius;
    }

    public void DisableRadius(float timeoutInSeconds = 30f)
    {
        // When boss has been bonked
        _collider.enabled = false;
        StartCoroutine(ReEnableRadius(timeoutInSeconds));
    }

    IEnumerator ReEnableRadius(float timeoutInSeconds = 30f)
    {
        yield return new WaitForSeconds(timeoutInSeconds);
        _collider.enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerInRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerInRadius = true;
        }
    }
}
