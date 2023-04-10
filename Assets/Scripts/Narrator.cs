using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    private void Start()
    {
        // We want to listen for mouse position to know if it's over our Narrator
        GameManager.Instance.OnMouseMove += MouseMove;
    }

    private void MouseMove(Vector2 pos)
    {
        // Get the mouse's position in our scene
        var mousePosInWorld = Camera.main.ScreenToWorldPoint(pos);
        mousePosInWorld.z = transform.position.z;

        // Calculate the distance from the mouse to the narrator
        var distance = Vector2.Distance(mousePosInWorld, transform.position);

        // Is distance within a determined offset?
        if (distance >= -0.5f && distance <= 0.5f) 
        {
            // Mouse is over narrator
            Debug.Log("The mouse is over me, I'm in danger...");
        }
    }
}
