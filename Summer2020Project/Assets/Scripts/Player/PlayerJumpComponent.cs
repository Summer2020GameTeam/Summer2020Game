﻿using System;
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
    private PlayerComponent playerComponent;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerComponent = GetComponent<PlayerComponent>();
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
            playerComponent.SetPlayerState(PlayerState.Jumping);
            _rigidbody.gravityScale = JumpGravity;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
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

    //Todo: Used in two different components. Refactor by making a seperate (static?) script for this. (Used by Jump and Input components).
    public bool isGrounded()
    {
        Collider2D[] tempColliders = Physics2D.OverlapBoxAll(playerGroundCheckTransform.position, playerGroundCheckSize, 0);
        if (tempColliders != null)
        {
            for(int i = 0; i < tempColliders.Length; i++)
            {
                if (tempColliders[i].gameObject.layer == 8 || tempColliders[i].gameObject.layer == 4)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    //Todo: Un-Spaghet
    public Collider2D getFloorCollider()
    {
        Collider2D[] tempColliders = Physics2D.OverlapBoxAll(playerGroundCheckTransform.position, playerGroundCheckSize, 0);
        if (tempColliders != null)
        {
            for (int i = 0; i < tempColliders.Length; i++)
            {
                if (tempColliders[i].gameObject.layer == 8 || tempColliders[i].gameObject.layer == 4)
                {
                    return tempColliders[i];
                }
            }
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(playerGroundCheckTransform.position, playerGroundCheckSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded())
        {
            playerComponent.SetPlayerState(PlayerState.Default);
        }
    }
}
