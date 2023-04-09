using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player's movement speed
    [SerializeField] private float moveSpeed = 6f;

    // Player's Movement direction
    private Vector2 _movement;
    
    // The Player's Rigidbody 2D component
    private Rigidbody2D _rb2d;
    
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
    
    private const string MoveRight = "MoveRight";
    private const string MoveLeft = "MoveLeft";
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
        GameManager.Instance.OnMove += Move;
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

        if (_movement != Vector2.zero)
        {
            if (_movement.x > 0f)
            {
                _animator.SetBool(Right, true);
                _animator.SetBool(Left, false);
                _animator.SetBool(Up, false);
                _animator.SetBool(Down, false);
                _direction = LookingDirection.Right;
            }
            else if (_movement.x < 0f)
            {
                _animator.SetBool(Right, false);
                _animator.SetBool(Left, true);
                _animator.SetBool(Up, false);
                _animator.SetBool(Down, false);
                _direction = LookingDirection.Left;
            }
            else if (_movement.y > 0f)
            {
                _animator.SetBool(Right, false);
                _animator.SetBool(Left, false);
                _animator.SetBool(Up, true);
                _animator.SetBool(Down, false);
                _direction = LookingDirection.Up;
            }
            else if (_movement.y < 0f)
            {
                _animator.SetBool(Right, false);
                _animator.SetBool(Left, false);
                _animator.SetBool(Up, false);
                _animator.SetBool(Down, true);
                _direction = LookingDirection.Down;
            }
        }
        else
        {
            _animator.SetBool(Right, false);
            _animator.SetBool(Left, false);
            _animator.SetBool(Up, false);
            _animator.SetBool(Down, false);
        }
    }
}
