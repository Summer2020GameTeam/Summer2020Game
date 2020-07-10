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
    private Camera cam;

    private Rigidbody2D _rigidbody;
    private PlayerComponent player;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
    }
    public void HandleInput(InputAction.CallbackContext context)
    {
        context.action.performed += ctx => HandleDash();
    }

    private void HandleDash()
    {
        if (dashAvailable)
        {
            dashAvailable = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction =  mousePosition - new Vector2(this.transform.position.x, this.transform.position.y);
            
            _rigidbody.velocity = direction.normalized * DashSpeed;
            StartCoroutine(DashTimer());
            StartCoroutine(RechargeDash());
        }
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(DashTime);
        _rigidbody.velocity = new Vector2( _rigidbody.velocity.x * 0.2f, _rigidbody.velocity.y * 0.2f);
    }

    private IEnumerator RechargeDash()
    {
        yield return new WaitForSeconds(DashCooldownTime);
        dashAvailable = true;
    }
}
