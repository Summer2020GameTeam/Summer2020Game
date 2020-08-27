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
    private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    private PlayerJumpComponent jumpComponent;

    public float Velocity;
    private float oldInputValue;

    private void Awake()
    {
        Speed = 10;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
        playerDash = _rigidbody.GetComponent<PlayerDashComponent>();
        jumpComponent = GetComponent<PlayerJumpComponent>();
    }

    private void LateUpdate()
    {
        HandleMovement(InputValue);
    }

    private void HandleMovement(Vector2 inputVector)
    {
        Vector2 normal = Vector2.zero;

        try
        {
            jumpComponent.getFloorCollider().GetContacts(contacts);
            if(contacts.Count > 0)
            {
                normal = Vector2.Perpendicular(contacts[0].normal) * player.FacingDirection;
            }
        }
        catch (System.Exception e)
        {

        }

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
            if(inputVector.x != 0 && Math.Abs(oldInputValue) <= Math.Abs(inputVector.x) && Math.Abs(_rigidbody.velocity.x) < Speed / 2)
            {
                if(normal != Vector2.zero)
                {
                    _rigidbody.AddForce(normal * (22000 * Math.Abs(inputVector.x)) * Time.deltaTime, 0);
                }
                else
                {
                    _rigidbody.AddForce(new Vector2((22000 * Math.Abs(inputVector.x)) * player.FacingDirection * Time.deltaTime, 0), ForceMode2D.Force);

                }
            }
            else
            {
                if (normal != Vector2.zero)
                {
                    _rigidbody.AddForce(normal * (4000 * Math.Abs(inputVector.x)) * Time.deltaTime, 0);
                }
                else
                {
                    _rigidbody.AddForce(new Vector2((4000 * Math.Abs(inputVector.x)) * player.FacingDirection * Time.deltaTime, 0), ForceMode2D.Force);
                }
            }
        }
        
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * Math.Abs(inputVector.x), _rigidbody.velocity.y);

        oldInputValue = inputVector.x;
    }
}