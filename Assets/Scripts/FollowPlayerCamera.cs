using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to make the camera flow the player smoothly
/// </summary>
public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    
    // For video on how this works check: https://www.youtube.com/watch?v=ZBj3LBA2vUY
    private void FixedUpdate()
    {
        var targetPosition = Player.Instance.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
