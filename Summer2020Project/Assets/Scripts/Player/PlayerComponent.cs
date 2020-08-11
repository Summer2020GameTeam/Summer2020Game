using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [HideInInspector]
    public float FacingDirection;

    private PlayerInputComponent input;

    private PlayerState state = PlayerState.Surface;

    private PlayerMovementComponent movement;
    private PlayerJumpComponent jump;
    private PlayerGrappleComponent grapple;
    private PlayerDashComponent dash;

    private void Awake()
    {
        input = GetComponent<PlayerInputComponent>();
        movement = GetComponent<PlayerMovementComponent>();
        jump = GetComponent<PlayerJumpComponent>();
        grapple = GetComponent<PlayerGrappleComponent>();
        dash = GetComponent<PlayerDashComponent>();
        input.JumpPressed.AddListener(jump.HandleJump);
        input.JumpReleased.AddListener(jump.HandleCancel);
        input.GrapplePressed.AddListener(grapple.LaunchGrapple);
        input.DashPressed.AddListener(dash.HandleDash);
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case PlayerState.Surface:
                movement.InputValue.x = input.HorizontalInput;
                break;
            case PlayerState.Swimming:
                break;
            default:
                break;
        }
    }

}
