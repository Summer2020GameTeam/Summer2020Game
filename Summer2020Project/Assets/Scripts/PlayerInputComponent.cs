using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputComponent : MonoBehaviour
{
    public float HorizontalInput { get; private set; }
    public UnityEvent JumpPressed = new UnityEvent();
    public UnityEvent JumpReleased = new UnityEvent();
    public float GrappleInput { get; private set; }
    public float DashInput { get; private set; }
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Vertical"))
        {
            JumpPressed.Invoke();
        }
        if (Input.GetButtonUp("Vertical"))
        {
            JumpReleased.Invoke();
        }
        GrappleInput = Input.GetAxis("Fire1");
        DashInput = Input.GetAxis("Fire3"); 
    }
}
