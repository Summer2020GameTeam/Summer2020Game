using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PlayerGrappleComponent : MonoBehaviour
{
    [SerializeField]
    GameObject GrappleProjectile;
    [SerializeField]
    float GrappleProjectileSpeed;
    [SerializeField]
    float GrappleLenght;
    [SerializeField]
    float GrappleForce;
    [HideInInspector]
    public Vector2 InputVector = Vector2.zero;

    private GameObject launchedGrapple;
    private LineRenderer line;

    private PlayerComponent player;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        player = GetComponent<PlayerComponent>();
        line = GetComponent<LineRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if(launchedGrapple != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, launchedGrapple.transform.position);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }

    public void LaunchGrapple()
    {
        if (launchedGrapple == null && enabled)
        {
            launchedGrapple = Instantiate(GrappleProjectile, transform.position, Quaternion.identity);
            GrappleProjectileBehaviourComponent grappleBehaviour = launchedGrapple.GetComponent<GrappleProjectileBehaviourComponent>();
            float direction = 1;
            if (player.FacingDirection > 0)
            {
                direction = 1;
            }
            else if(player.FacingDirection < 0)
            {
                direction = -1;
            }
            if(InputVector != Vector2.zero)
            {
                launchedGrapple.GetComponent<Rigidbody2D>().velocity = (InputVector * GrappleProjectileSpeed) + _rigidbody.velocity;
            }
            else
            {
                launchedGrapple.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction, 0) * GrappleProjectileSpeed) + _rigidbody.velocity;
            }
            grappleBehaviour.ParentBody = GetComponent<Rigidbody2D>();
            grappleBehaviour.LenghtSpan = GrappleLenght;
            grappleBehaviour.GrappleForce = GrappleForce;
        }
    }
}
