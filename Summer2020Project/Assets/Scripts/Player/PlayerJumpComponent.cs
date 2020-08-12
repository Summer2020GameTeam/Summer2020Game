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
    private Transform playerGroundCheckTransform;
    [SerializeField]
    private Vector2 playerGroundCheckSize;

    private string lastJumpButtonPressed;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody.gravityScale = BaseGravity;
    }

    private void FixedUpdate()
    {
        if (!isGrounded())
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
        if (isGrounded() && enabled)
        {
            currentJumpTime = 0;
            _rigidbody.gravityScale = JumpGravity;
            _rigidbody.AddForce(new Vector2(0, JumpForce));
        }
    }

    public void HandleCancel()
    {
        if (enabled)
        {
            _rigidbody.gravityScale = BaseGravity;
        }

    }

    private bool isGrounded()
    {
        Collider2D[] tempColliders = Physics2D.OverlapBoxAll(playerGroundCheckTransform.position, playerGroundCheckSize, 0);
        if (tempColliders != null)
        {
            for(int i = 0; i < tempColliders.Length; i++)
            {
                if (tempColliders[i].gameObject.layer == 8 || tempColliders[i].gameObject.layer == 4)
                {
                    Debug.Log("Ground check" + tempColliders[i].gameObject.layer);
                    return true;
                }
            }
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(playerGroundCheckTransform.position, playerGroundCheckSize);
    }
}
