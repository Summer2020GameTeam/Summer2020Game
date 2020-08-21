using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField]
    public float Speed { get; set; }
    public Vector2 InputValue = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private PlayerComponent player;
    private PlayerDashComponent playerDash;

    public float Velocity;
    private float oldInputValue;

    private void Awake()
    {
        Speed = 10;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
        playerDash = _rigidbody.GetComponent<PlayerDashComponent>();
    }

    private void LateUpdate()
    {
        HandleMovement(InputValue);
        Debug.Log(_rigidbody.velocity.x);
    }

    private void HandleMovement(Vector2 inputVector)
    {
        if (inputVector.x != 0)
        {
            player.FacingDirection = inputVector.normalized.x;

            if (inputVector.x > 0 && _rigidbody.velocity.x < 0 ||
               inputVector.x < 0 && _rigidbody.velocity.x > 0)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
        }

        if (Math.Abs(_rigidbody.velocity.x) < Speed)
        {
            if(inputVector.x != 0 && Math.Abs(oldInputValue) <= Math.Abs(inputVector.x))
            {
                _rigidbody.AddForce(new Vector2((18000 - (17000 * Math.Abs(inputVector.x))) * player.FacingDirection * Time.deltaTime, 0), ForceMode2D.Force);
            }
        }
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * Math.Abs(inputVector.x), _rigidbody.velocity.y);

        oldInputValue = inputVector.x;
    }
}