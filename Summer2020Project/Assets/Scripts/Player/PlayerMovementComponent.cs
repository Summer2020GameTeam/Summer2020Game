using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10;
    public Vector2 InputValue = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private PlayerComponent player;
    private PlayerDashComponent playerDash;

    public float Velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
        playerDash = _rigidbody.GetComponent<PlayerDashComponent>();
    }

    private void LateUpdate()
    {
        HandleMovement(InputValue);
    }

    private void HandleMovement(Vector2 inputVector)
    {
            if (Math.Abs(_rigidbody.velocity.x) < Speed)
            {
                _rigidbody.velocity = new Vector2(Speed * inputVector.x, _rigidbody.velocity.y);
            }
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * Math.Abs(inputVector.x), _rigidbody.velocity.y);

            if (inputVector.x != 0)
            {
                player.FacingDirection = inputVector.normalized.x;

                if (inputVector.x > 0 && _rigidbody.velocity.x < 0 ||
                   inputVector.x < 0 && _rigidbody.velocity.x > 0)
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                }
            }
    }
}