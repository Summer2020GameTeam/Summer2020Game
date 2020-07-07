using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpComponent : MonoBehaviour
{
    [Header("Jump Properties")]
    private float VerInput;

    private bool grounded;

    [SerializeField]
    private int PowerMultiplier = 1;
    [SerializeField]
    public float JumpPower = 150;
    [SerializeField]
    private float DefaultGravity = 1;
    [SerializeField]
    private float HeavyGravity = 2;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void HandleInput(InputAction.CallbackContext context)
    {
        context.action.performed += ctx => VerInput  = 1;
        context.action.canceled += ctx => VerInput = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vector2.Angle(collision.GetContact(0).normal, Vector2.up) < 60)
        {
            _rigidbody.gravityScale = DefaultGravity;
        }
    }


    void FixedUpdate()
    {
        VerticalMovement();
    }


    void VerticalMovement()
    {
        if (PowerMultiplier != 0)
        {
            if (VerInput > 0 && IsGrounded())
            {
                _rigidbody.velocity = (new Vector2(0, Mathf.Sqrt(PowerMultiplier) * JumpPower * Time.deltaTime));
            }
        }

        if (!IsGrounded() && VerInput <= 0)
        {
            _rigidbody.gravityScale = HeavyGravity;
        }
    }

    bool IsGrounded()
    {
        int layerMask = 1 << 10;
        int ignoreRaycastMask = 1 << 2;
        int finalMask = layerMask | ignoreRaycastMask;
        finalMask = ~finalMask;
        RaycastHit2D obj = Physics2D.Raycast(transform.position, Vector2.down, .6f, finalMask);
        return obj;
    }
}
