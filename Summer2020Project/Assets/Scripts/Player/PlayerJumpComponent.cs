using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpComponent : MonoBehaviour
{
    [Header("Jump Stats")]
    [SerializeField]
    float JumpForce = 150;
    [SerializeField]
    float JumpTime = 1;
    [SerializeField]
    float currentJumpTime = 0f;
    [SerializeField]
    float BaseGravity = 2.3f;
    [SerializeField]
    float JumpGravity = 0f;

    [SerializeField]
    InputAction jumpWAction;
    [SerializeField]
    InputAction jumpSpaceAction;
    [SerializeField]
    InputAction jumpUpArrowAction;
    [SerializeField]
    private InputActionAsset playerControls;

    private string lastJumpButtonPressed;
    private bool isGrounded;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        var gameplayActionMap = playerControls.FindActionMap("Player");
        jumpSpaceAction = gameplayActionMap.FindAction("JumpSpace");
        jumpWAction = gameplayActionMap.FindAction("JumpW");
        jumpUpArrowAction = gameplayActionMap.FindAction("JumpUpArrow");

        jumpSpaceAction.performed += ctx => HandleJump(ctx.action.name);
        jumpSpaceAction.canceled += ctx => HandleCancel(ctx.action.name);
        jumpWAction.performed += ctx => HandleJump(ctx.action.name);
        jumpWAction.canceled += ctx => HandleCancel(ctx.action.name);
        jumpUpArrowAction.performed += ctx => HandleJump(ctx.action.name);
        jumpUpArrowAction.canceled += ctx => HandleCancel(ctx.action.name);
    }

    private void OnEnable()
    {
        jumpSpaceAction.Enable();
        jumpWAction.Enable();
        jumpUpArrowAction.Enable();
    }

    private void OnDisable()
    {
        jumpSpaceAction.Disable();
        jumpWAction.Disable();
        jumpUpArrowAction.Disable();
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            currentJumpTime += Time.deltaTime;
            if(currentJumpTime >= JumpTime)
            {
                _rigidbody.gravityScale = BaseGravity;
            }
        }
    }

    private void HandleJump(string buttonName)
    {
        if (isGrounded)
        {
            _rigidbody.gravityScale = JumpGravity;
            _rigidbody.AddForce(new Vector2(0, JumpForce));
            isGrounded = false;
            lastJumpButtonPressed = buttonName;
        }
    }

    private void HandleCancel(string buttonName)
    {
        if (lastJumpButtonPressed == buttonName)
        {
            Debug.Log("Cancelled");
            _rigidbody.gravityScale = BaseGravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(transform.transform.InverseTransformDirection(_rigidbody.velocity).y <= 0)
        {
            Debug.Log(transform.transform.InverseTransformDirection(_rigidbody.velocity).y);
            currentJumpTime = 0f;
            isGrounded = true;
        }
    }
}
