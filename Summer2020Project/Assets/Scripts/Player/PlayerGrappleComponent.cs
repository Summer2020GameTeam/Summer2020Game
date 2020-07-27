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
    float GrappleLifespan;
    [SerializeField]
    float GrappleForce;

    private GameObject launchedGrapple;
    private LineRenderer line;

    PlayerComponent player;

    private void Awake()
    {
        player = GetComponent<PlayerComponent>();
        line = GetComponent<LineRenderer>();
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

    public void HandleInput(InputAction.CallbackContext context)
    {
        Debug.Log("Test");
        context.action.performed += ctx => LaunchGrapple();
    }

    private void LaunchGrapple()
    {
        if (launchedGrapple == null)
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
            launchedGrapple.GetComponent<Rigidbody2D>().velocity = new Vector2(GrappleProjectileSpeed * direction, 0);
            grappleBehaviour.ParentBody = GetComponent<Rigidbody2D>();
            grappleBehaviour.Lifespan = GrappleLifespan;
            grappleBehaviour.GrappleForce = GrappleForce;
        }
    }
}
