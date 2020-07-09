using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashComponent : MonoBehaviour
{
    [SerializeField]
    private float DashCooldown = 2;
    private float currentDashCooldown;
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
    public void HandleInput(InputAction.CallbackContext context)
    {
        context.action.performed += ctx => HandleDash();
    }

    private void HandleDash()
    {
        _rigidbody.velocity = new Vector2(DashSpeed * player.FacingDirection, _rigidbody.velocity.y);
        StartCoroutine(DashTimer());
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(DashTime);
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }
}
