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

    private string lastJumpButtonPressed;
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

    public void HandleJump()
    {
        if (isGrounded)
        {
            _rigidbody.gravityScale = JumpGravity;
            _rigidbody.AddForce(new Vector2(0, JumpForce));
            isGrounded = false;
        }
    }

    public void HandleCancel()
    {
            _rigidbody.gravityScale = BaseGravity;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(transform.transform.InverseTransformDirection(_rigidbody.velocity).y <= 0)
        {
            currentJumpTime = 0f;
            isGrounded = true;
        }
    }
}
