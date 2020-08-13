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
    private float DashSpeed = 40;
    [SerializeField]
    private float DashTime = 0.2f;
    [SerializeField]
    private float MinimalDashSpeed = 5;
    [SerializeField]
    private float BreakTime = 0.4f;
    [SerializeField]
    private float DashBreakMultiplier = 0.80f;

    private Rigidbody2D _rigidbody;
    private PlayerComponent player;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
    }

    private void Update()
    {
        Debug.Log(_rigidbody.velocity);
    }
    public void HandleDash()
    {
        if (dashAvailable && enabled)
        {
            Debug.Log(_rigidbody.velocity);
            dashAvailable = false;
            if(Math.Abs(_rigidbody.velocity.x) > 5)
            {
                _rigidbody.AddForce(new Vector2(DashSpeed * player.FacingDirection, 0), ForceMode2D.Impulse);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(DashSpeed * player.FacingDirection, _rigidbody.velocity.y);
            }
           
            StartCoroutine(DashTimer());
            StartCoroutine(RechargeDash());
        }
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(DashTime);
        if (Math.Abs(_rigidbody.velocity.x) >= DashSpeed/2)
        {
            _rigidbody.AddForce(new Vector2((-DashSpeed*0.8f) * player.FacingDirection, 0), ForceMode2D.Impulse);
        }
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * DashBreakMultiplier, _rigidbody.velocity.y);
        yield return new WaitForSeconds(BreakTime);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * (float)Math.Pow(System.Convert.ToDouble(DashBreakMultiplier), -1d), _rigidbody.velocity.y);
    }

    private IEnumerator RechargeDash()
    {
        yield return new WaitForSeconds(DashCooldownTime);
        dashAvailable = true;
    }
}
