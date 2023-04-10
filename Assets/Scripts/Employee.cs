using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

public class Employee : MonoBehaviour
{
    // For MVP the Employee will not move so as to save some dev time
    
    // List of possible sprites for the Employee to turn into
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    // Our sprite renderer, we might need to access it later
    private SpriteRenderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        // Do we have any sprites in the list?
        if (sprites.Any())
        {
            // Grab a random sprite from the list
            var sprite = sprites.Random();
            
            // Switch the colors
            // sprite = sprite.ShiftEmployeeColors(); // Not working for now
            
            // Set the sprite in the renderer to the new one
            _renderer.sprite = sprite;
        }
    }
}
