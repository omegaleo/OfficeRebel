using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class BossRadius : InstancedBehaviour<BossRadius>
{
    public bool isPlayerInRadius;

    private CircleCollider2D _collider;
    private SpriteRenderer _renderer;

    private const float BASE_RADIUS = 3f;
    
    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        SetRadius();
    }

    public void SetRadius()
    {
        var radius = CalculateBossRadius();

        transform.localScale = new Vector3(radius, radius);

        var color = Color.yellow;
        switch (GameManager.Instance.Suspicion)
        {
            case >= 8:
                color = Color.red;
                break;
            case >= 5:
                ColorUtility.TryParseHtmlString("#eb8500", out color);
                break;
            default:
                color = Color.yellow;
                break;
        }

        color.a = 0.25f;

        _renderer.color = color;
    }

    private float CalculateBossRadius()
    {
        int itemsStolen = Player.Instance.ItemsStolen;
        float suspicionMultiplier = GameManager.Instance.Suspicion / 10f;
        float itemsMultiplier = itemsStolen * 0.05f;
        float radius = BASE_RADIUS + (itemsMultiplier * suspicionMultiplier);
        return Mathf.Clamp(radius, BASE_RADIUS, 15f);
    }
    
    /*private float CalculateBossRadius() =>
        3f + ((Player.Instance.ItemsStolen * 0.05f) * (GameManager.Instance.Suspicion * 0.15f));*/

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
            isPlayerInRadius = false;
        }
    }
}
