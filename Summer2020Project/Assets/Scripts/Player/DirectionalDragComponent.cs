using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalDragComponent : MonoBehaviour
{
    // The drag factor can range from 1 (no drag) to 0 (stops immediately).
    [SerializeField]
    public float x = 0.5f;
    [SerializeField]
    public float y = 1;
    [SerializeField]
    public float z = 1;
    PlayerMovementComponent playerMovementComponent;
    Rigidbody2D _rigidbody;
    [SerializeField]
    public float drag = 1f;
    [SerializeField]
    public float sideDragFactor = 0.0025f;
    [SerializeField]
    public float acceleration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerMovementComponent = GetComponent <PlayerMovementComponent>();
    }

    public float ApplyDrag()
    {
        /*Vector3 vel = transform.InverseTransformDirection(_rigidbody.velocity);
        vel.x *= x;
        vel.y *= y;
        vel.z *= z;
        _rigidbody.velocity = transform.TransformDirection(vel);*/

        Vector3 velocity = transform.InverseTransformDirection(_rigidbody.velocity);
        if(Math.Abs(playerMovementComponent.oldInputValue) > Math.Abs(playerMovementComponent.InputValue.x) || playerMovementComponent.InputValue.x == 0)
        {
            float force_x = -drag / (sideDragFactor / (Math.Abs(15 - Math.Abs(playerMovementComponent.InputValue.x) * 15) + 1)) * ((velocity.x / (1.5f - Math.Abs(playerMovementComponent.InputValue.x))) / 2);
            Debug.Log(force_x);
            //_rigidbody.AddForce(new Vector2(force_x, 0));
            return force_x;
        }
        return 0;
        //Debug.Log("velocity: " + velocity);
        //_rigidbody.AddRelativeForce(new Vector2(_rigidbody.mass * acceleration, _rigidbody.mass * acceleration));
    }
}
