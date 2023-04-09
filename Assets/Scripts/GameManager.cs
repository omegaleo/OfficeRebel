using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : InstancedBehaviour<GameManager>
{
    
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
    #endregion
}
