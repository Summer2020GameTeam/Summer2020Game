using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputComponent : MonoBehaviour
{
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public UnityEvent JumpPressed = new UnityEvent();
    public UnityEvent JumpReleased = new UnityEvent();
    public UnityEvent GrapplePressed = new UnityEvent();
    public UnityEvent DashPressed = new UnityEvent();
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            JumpPressed.Invoke();
        }
        if (Input.GetButtonUp("Jump"))
        {
            JumpReleased.Invoke();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            GrapplePressed.Invoke();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            DashPressed.Invoke();
        }
    }
}
