using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [HideInInspector]
    public float FacingDirection;

    private PlayerInputComponent input;

    private PlayerMovementComponent movement;
    private PlayerJumpComponent jump;

    private void Awake()
    {
        input = GetComponent<PlayerInputComponent>();
        movement = GetComponent<PlayerMovementComponent>();
        jump = GetComponent<PlayerJumpComponent>();
        input.JumpPressed.AddListener(jump.HandleJump);
        input.JumpReleased.AddListener(jump.HandleCancel);
    }

    private void FixedUpdate()
    {
        movement.InputValue.x = input.HorizontalInput;
    }

}
