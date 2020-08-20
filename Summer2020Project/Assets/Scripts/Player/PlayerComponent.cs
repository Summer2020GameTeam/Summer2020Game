﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [HideInInspector]
    public float FacingDirection;

    private PlayerInputComponent input;
    [SerializeField]
    private PlayerState state = PlayerState.Default;
    [SerializeField]
    [Range(0, 1)]
    private float timeScale = 1;

    private PlayerMovementComponent movement;
    private PlayerJumpComponent jump;
    private PlayerGrappleComponent grapple;
    private PlayerDashComponent dash;
    private PlayerDiveComponent dive;

    private void Awake()
    {
        input = GetComponent<PlayerInputComponent>();
        movement = GetComponent<PlayerMovementComponent>();
        jump = GetComponent<PlayerJumpComponent>();
        grapple = GetComponent<PlayerGrappleComponent>();
        dash = GetComponent<PlayerDashComponent>();
        dive = GetComponent<PlayerDiveComponent>();
        input.JumpPressed.AddListener(jump.HandleJump);
        input.JumpReleased.AddListener(jump.HandleCancel);
        input.GrapplePressed.AddListener(grapple.LaunchGrapple);
        input.DashPressed.AddListener(dash.HandleDash);
        input.DivePressed.AddListener(dive.HandleDive);
    }

    private void Update()
    {
        Time.timeScale = timeScale;

        switch (state)
        {
            case PlayerState.Default:
                movement.enabled = true;
                jump.enabled = true;
                dive.enabled = false;
                grapple.enabled = true;
                movement.InputValue.x = input.HorizontalInput;
                break;
            case PlayerState.Swimming:
                dive.enabled = true;
                jump.enabled = false;
                movement.enabled = false;
                dive.InputValue = new Vector2(input.HorizontalInput, input.VerticalInput);
                break;
            case PlayerState.Dashing:
                jump.enabled = false;
                movement.enabled = false;
                grapple.enabled = false;
                break;
            default:
                break;

        }
    }

    public void SetPlayerState(PlayerState newState)
    {
        state = newState;
        switch (newState)
        {
            case PlayerState.Default:
                movement.enabled = true;
                jump.enabled = true;
                dive.enabled = false;
                break;
            case PlayerState.Swimming:
                dive.enabled = true;
                jump.enabled = false;
                movement.enabled = false;
                break;
            default:
                break;
        }
    }

}
