using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10;
    private Vector2 InputValue = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement(InputValue);
    }
    public void HandleInput(InputAction.CallbackContext context)
    {
        context.action.performed += ctx => InputValue = ctx.ReadValue<Vector2>();
        context.action.canceled += ctx => InputValue = Vector2.zero;
    }

    private void HandleMovement(Vector2 inputVector)
    {
        Vector2 oldPosition = transform.position;
        Vector2 newPosition = new Vector2(oldPosition.x + (inputVector.x * Speed * Time.deltaTime), oldPosition.y);
        transform.position = newPosition;
    }
}