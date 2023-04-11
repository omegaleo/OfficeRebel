using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Player : InstancedBehaviour<Player>
{
    // Player's movement speed
    [SerializeField] private float moveSpeed = 6f;

    // Player's Movement direction
    private Vector2 _movement;
    
    // The Player's Rigidbody 2D component
    private Rigidbody2D _rb2d;

    /// <summary>
    /// Total amount of money the player managed to steal
    /// </summary>
    public float Money = 0f;
    /// <summary>
    /// Total amount of items the player stole, will be used for punishment and boss radius
    /// </summary>
    public int ItemsStolen = 0;
    
    #region Animator Related
    private Animator _animator;
    
    public enum LookingDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    
    private LookingDirection _direction;
    
    private const string MoveRight = "MovingRight";
    private const string MoveLeft = "MovingLeft";
    private const string MoveUp = "MovingUp";
    private const string MoveDown = "MovingDown";
    
    private static readonly int Right = Animator.StringToHash(MoveRight);
    private static readonly int Left = Animator.StringToHash(MoveLeft);
    private static readonly int Up = Animator.StringToHash(MoveUp);
    private static readonly int Down = Animator.StringToHash(MoveDown);
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        // Get our required components
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        // Subscribe to the Action
        GameManager.Instance.OnMove += Move;
    }

    private void OnEnable()
    {
        // Subscribe to the Action
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMove += Move;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to the Action in order to avoid errors when Invoking it
        GameManager.Instance.OnMove += Move;
    }

    private void OnDestroy()
    {
        // Unsubscribe to the Action in order to avoid errors when Invoking it
        GameManager.Instance.OnMove += Move;
    }

    private void Move(Vector2 movement)
    {
        _movement = movement;
    }
    
    private void FixedUpdate()
    {
        _rb2d.MovePosition(_rb2d.position +
                           _movement * moveSpeed * Time.deltaTime);

        _animator.SetBool(Right, false);
        _animator.SetBool(Left, false);
        _animator.SetBool(Up, false);
        _animator.SetBool(Down, false);
        
        if (_movement != Vector2.zero)
        {
            if (_movement.x > 0f)
            {
                _animator.SetBool(Right, true);
                _direction = LookingDirection.Right;
            }
            else if (_movement.x < 0f)
            {
                _animator.SetBool(Left, true);
                _direction = LookingDirection.Left;
            }
            else if (_movement.y > 0f)
            {
                _animator.SetBool(Up, true);
                _direction = LookingDirection.Up;
            }
            else if (_movement.y < 0f)
            {
                _animator.SetBool(Down, true);
                _direction = LookingDirection.Down;
            }
        }
    }
}
