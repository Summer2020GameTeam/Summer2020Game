using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputComponent : MonoBehaviour
{
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public float GrappleInput { get; private set; }
    public float DashInput { get; private set; }
    void FixedUpdate()
    {
        HandleInput();
    }

    void HandleInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        GrappleInput = Input.GetAxis("Fire1");
        DashInput = Input.GetAxis("Fire3"); 
    }
}
