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

    private bool isGrounded;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
    public void HandleInput(InputAction.CallbackContext context)
    {
        context.action.performed += ctx => HandleJump();
        context.action.canceled += ctx => _rigidbody.gravityScale = BaseGravity;
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            _rigidbody.gravityScale = JumpGravity;
            _rigidbody.AddForce(new Vector2(0, JumpForce));
            isGrounded = false;
        }
        else if(currentJumpTime < JumpTime)
        {
            _rigidbody.gravityScale = JumpGravity;
        }
        else
        {
            _rigidbody.gravityScale = BaseGravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentJumpTime = 0f;
        isGrounded = true;
    }
}
