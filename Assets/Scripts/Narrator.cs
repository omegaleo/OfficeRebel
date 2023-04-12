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
        
        // We also want to listen for when a player has stolen an item
        Player.Instance.StoleItem += OnItemStolen;
    }

    private void OnItemStolen()
    {
        if (Player.Instance.ItemsStolen % 2 == 0)
        {
            // Every 2 items we increase the suspicion
            GameManager.Instance.Suspicion++;
            
            // Call for voice line regarding the boss being warned
            AudioController.Instance.PlayVoiceLine(VoiceLineType.Warned);

            // Set the BossRadius
            BossRadius.Instance.SetRadius();
        }
        else
        {
            // Call for voice line about warning the boss
            AudioController.Instance.PlayVoiceLine(VoiceLineType.Warning);
        }
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
