using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : InstancedBehaviour<GameManager>
{
    protected override void Awake()
    {
        // Call the InstancedBehaviour declaration method
        base.Awake();
        
        // Mark this object so it doesn't get destroyed when switching scenes
        DontDestroyOnLoad(this.gameObject);
    }

    [Range(0,10)]
    public int Suspicion = 0;
    
    #region Colors
    public Color NormalHairColor = Color.clear;
    public Color NormalClothesColor = Color.clear;
    public Color NormalClothesShadowColor = Color.clear;
    public List<Color> HairColors = new List<Color>();
    public List<Color> ClothesColors = new List<Color>();
    public List<Color> ClothesShadowsColors = new List<Color>();
    #endregion

    #region InputSystem

    /// <summary>
    /// This action will be used mostly in the Player class to move the player
    /// </summary>
    public Action<Vector2> OnMove;

    /// <summary>
    /// This method will be used to declare to the Player Input class which method to call for the "Move" action and will call the OnMove action when it's values are different than 0
    /// </summary>
    /// <param name="value"></param>
    public void OnMoveDown(InputAction.CallbackContext value)
    {
        // Read the value that was sent from the Player Input class
        var movement = value.ReadValue<Vector2>();

        // Do we have any class listening to the OnMove action?
        if (OnMove.GetInvocationList().Any())
        {
            // Invoke the action to all classes listening to it
            OnMove.Invoke(movement);
        }
    }
    
    /// <summary>
    /// This action will be used mostly in the Player class to move the player
    /// </summary>
    public Action<Vector2> OnMouseMove;

    /// <summary>
    /// Hold the previous mouse position value to compare whenever the mouse moves
    /// </summary>
    private Vector2 _prevMousePos = Vector2.zero;
    
    /// <summary>
    /// This method will be used to declare to the Player Input class which method to call for the "Move" action and will call the OnMove action when it's values are different than 0
    /// </summary>
    /// <param name="value"></param>
    public void OnMouseMoveDown(InputAction.CallbackContext value)
    {
        // Read the value that was sent from the Player Input class
        var position = value.ReadValue<Vector2>();

        if (position != _prevMousePos)
        {
            // Do we have any class listening to the OnMove action?
            if (OnMouseMove.GetInvocationList().Any())
            {
                // Invoke the action to all classes listening to it
                OnMouseMove.Invoke(position);
            }

            _prevMousePos = position;
        }
    }

    public Action OnInteract;
    /// <summary>
    /// Variable that will be used to detect if we're dragging an object
    /// </summary>
    public bool IsInteracting;
    
    public void OnInteractDown(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            // Do we have any class listening to the OnInteract action?
            if (OnInteract.GetInvocationList().Any())
            {
                // Invoke the action to all classes listening to it
                OnInteract.Invoke();
            }
        }

        IsInteracting = !value.canceled;
    }
    
    public Action OnOptions;

    public void OnOptionsDown(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            // Do we have any class listening to the OnInteract action?
            if (OnOptions.GetInvocationList().Any())
            {
                // Invoke the action to all classes listening to it
                OnOptions.Invoke();
            }
        }
    }
    #endregion

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Suspicion = 0;
        AudioController.Instance.NextSong();
    }

    public Action OnSuspicionIncreased;

    public void IncreaseSuspicion()
    {
        Suspicion++;

        if (OnSuspicionIncreased.GetInvocationList().Any())
        {
            OnSuspicionIncreased.Invoke();
        }
    }
}
