using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiveComponent : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    public Vector2 InputValue;
    private Rigidbody2D _rigidbody;
    private PlayerComponent player;

    private bool isInWater;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
    }

    private void OnEnable()
    {
        _rigidbody.gravityScale = 0;
    }
    void Update()
    {
        HandleMovement(InputValue);
    }
    private void HandleMovement(Vector2 inputVector)
    {
        Vector2 oldPosition = transform.position;
        Vector2 newPosition = oldPosition + (inputVector * Speed * Time.deltaTime);
        transform.position = newPosition;
        if (inputVector.x != 0)
        {
            player.FacingDirection = inputVector.x;

            if (inputVector.x > 0 && _rigidbody.velocity.x < 0 ||
               inputVector.x < 0 && _rigidbody.velocity.x > 0)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
        }
    }

    public void HandleDive()
    {
        if (isInWater)
        {
            player.SetPlayerState(PlayerState.Swimming);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 4)
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            isInWater = false;
            player.SetPlayerState(PlayerState.Surface);
        }
    }
}
