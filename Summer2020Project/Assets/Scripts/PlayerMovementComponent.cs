﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10;
    private Vector2 InputValue = Vector2.zero;

    private void Update()
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
        Vector2 newPosition = oldPosition + (inputVector * Speed * Time.deltaTime);
        transform.position = newPosition;
    }
}
