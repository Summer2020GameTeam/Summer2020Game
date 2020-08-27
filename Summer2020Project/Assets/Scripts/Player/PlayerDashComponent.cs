using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashComponent : MonoBehaviour
{
    [SerializeField]
    private float DashCooldownTime = 2;
    private bool dashAvailable = true;
    [SerializeField]
    private float DashSpeed = 60;
    [SerializeField]
    private float DashDistance = 10;
    private float DashTime = 0;
    [SerializeField]
    private float MinimalDashSpeed = 5;
    [SerializeField]
    private float BreakTime = 0.4f;
    [SerializeField]
    private float DashBreakMultiplier = 0.80f;

    private float savedVelocity;
    private float savedGravityScale;
    private Rigidbody2D _rigidbody;
    private PlayerComponent player;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
    }

    private void Update()
    {
        
    }
    public void HandleDash()
    {
        if (dashAvailable && enabled)
        {
            player.SetPlayerState(PlayerState.Dashing);
            dashAvailable = false;
            if (Math.Abs(DashSpeed * player.FacingDirection + _rigidbody.velocity.x) < DashSpeed)
            {
                DashTime = DashDistance / DashSpeed;
            }
            else
            {
                DashTime = DashDistance / (DashSpeed + Math.Abs(_rigidbody.velocity.x));
            }
            StartCoroutine(DoDash());
        }
    }

    private IEnumerator DoDash()
    {
        savedGravityScale = _rigidbody.gravityScale;
        savedVelocity = _rigidbody.velocity.x;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = new Vector2(0, 0f);
        yield return new WaitForSeconds(0.1f);
        if(Math.Abs(DashSpeed * player.FacingDirection + _rigidbody.velocity.x) < DashSpeed)
        {
            _rigidbody.velocity = new Vector2(DashSpeed * player.FacingDirection, 0);
        }
        else
        {
            _rigidbody.velocity = new Vector2(Math.Abs(DashSpeed + Math.Abs(_rigidbody.velocity.x)) * player.FacingDirection, 0);
        }

        StartCoroutine(DashTimer());
        StartCoroutine(RechargeDash());
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(DashTime);
        _rigidbody.gravityScale = savedGravityScale;

        //Keep player momentum after dash.
        _rigidbody.velocity = new Vector2(savedVelocity * 1.05f, 5);
        player.SetPlayerState(PlayerState.Default);
    }

    private IEnumerator RechargeDash()
    {
        yield return new WaitForSeconds(DashCooldownTime);
        dashAvailable = true;
    }
}
