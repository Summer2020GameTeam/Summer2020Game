using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField]
    public float Speed { get; set; }
    //[SerializeField]
    //private float dragForce = 2000f;
    public Vector2 InputValue = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private PlayerComponent player;
    private PlayerDashComponent playerDash;
    private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    private PlayerJumpComponent jumpComponent;
    private DirectionalDragComponent dragComponent;
    private Vector2 finalForce;

    public float Velocity;
    public float oldInputValue;

    private void Awake()
    {
        Speed = 10;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
        playerDash = _rigidbody.GetComponent<PlayerDashComponent>();
        jumpComponent = GetComponent<PlayerJumpComponent>();
        dragComponent = GetComponent<DirectionalDragComponent>();
    }

    private void LateUpdate()
    {
        HandleMovement(InputValue);
        //Debug.Log("Speed: " + _rigidbody.velocity.x);
    }

    private void HandleMovement(Vector2 inputVector)
    {
        finalForce = new Vector2();
        float dragForce = dragComponent.ApplyDrag();
        Vector2 normal = Vector2.zero;
        //Debug.Log("Speed: " + _rigidbody.velocity.x + " Drag: " + dragForce);


        if (inputVector.x != 0)
        {
            player.FacingDirection = inputVector.normalized.x;

            if (inputVector.x > 0 && _rigidbody.velocity.x < 0 ||
               inputVector.x < 0 && _rigidbody.velocity.x > 0)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
        }

        try
        {
            jumpComponent.getFloorCollider().GetContacts(contacts);
            if(contacts.Count > 0)
            {
                if(inputVector.x != 0)
                {
                    normal = Vector2.Perpendicular(contacts[0].normal) * player.FacingDirection;
                }
                else
                {
                    normal = Vector2.Perpendicular(contacts[0].normal) * _rigidbody.velocity.normalized;
                }
            }
        }
        catch (System.Exception e)
        {

        }

        if (Math.Abs(_rigidbody.velocity.x) < Speed)
        {
            if(inputVector.x != 0 && Math.Abs(oldInputValue) <= Math.Abs(inputVector.x) && Math.Abs(_rigidbody.velocity.x) < Speed / 2)
            {
                if(normal != Vector2.zero)
                {
                    finalForce = normal * (24000 * Math.Abs(inputVector.x));
                    if (dragForce < 0)
                    {
                        finalForce += (normal * dragForce);
                    }
                    else
                    {
                        finalForce += (-normal * dragForce);
                    }
                    finalForce = finalForce * Time.deltaTime;
                    _rigidbody.AddForce(finalForce);
                }
                else
                {
                    _rigidbody.AddForce(new Vector2(((24000 * Math.Abs(inputVector.x) * player.FacingDirection) + (dragForce)) * Time.deltaTime, 0), ForceMode2D.Force);
                }
            }
            else
            {
                if (normal != Vector2.zero)
                {
                    finalForce = normal * (4000 * Math.Abs(inputVector.x));
                    if(dragForce < 0)
                    {
                        finalForce += (normal * dragForce);
                    }
                    else
                    {
                        finalForce += (-normal * dragForce);
                    }
                    finalForce = finalForce * Time.deltaTime;
                    _rigidbody.AddForce(finalForce);
                }
                else
                {
                    _rigidbody.AddForce(new Vector2(((4000 * Math.Abs(inputVector.x) * player.FacingDirection) + (dragForce)) * Time.deltaTime, 0), ForceMode2D.Force);
                }
            }
        }
        else
        {
            _rigidbody.AddForce(new Vector2(dragForce * Time.deltaTime, 0), ForceMode2D.Force);
        }

        //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x * Math.Abs(inputVector.x), _rigidbody.velocity.y);
        //dragComponent.ApplyDrag();

        oldInputValue = inputVector.x;
    }
}