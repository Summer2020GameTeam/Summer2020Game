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
    private float DashSpeed = 20;
    [SerializeField]
    private float DashTime = 1;

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
            dashAvailable = false;
            _rigidbody.AddForce(new Vector2(DashSpeed * player.FacingDirection, 0), ForceMode2D.Impulse);
            StartCoroutine(DashTimer());
            StartCoroutine(RechargeDash());
        }
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(DashTime);
        _rigidbody.AddForce(new Vector2(-DashSpeed * player.FacingDirection, 0), ForceMode2D.Impulse);
    }

    private IEnumerator RechargeDash()
    {
        yield return new WaitForSeconds(DashCooldownTime);
        dashAvailable = true;
    }
}
