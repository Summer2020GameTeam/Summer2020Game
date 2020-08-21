using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideComponent : MonoBehaviour
{
    [SerializeField]
    private float SlideTimer = 1;
    [SerializeField]
    private float SlideFriction;
    [SerializeField]
    private float MinDashSpeed;
    [SerializeField]
    private float MaxDashSpeed;
    [SerializeField]
    private float SlideSpeed = 10;

    public bool IsDashing { get; set; }
    private Rigidbody2D _rigidbody;
    private PlayerComponent player;
    private PlayerMovementComponent movementComponent;
    private PlayerJumpComponent jumpComponent;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerComponent>();
        movementComponent = GetComponent<PlayerMovementComponent>();
        jumpComponent = GetComponent<PlayerJumpComponent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleSlide()
    {
        if (!IsDashing && enabled)
        {
            player.SetPlayerState(PlayerState.Sliding);
            _rigidbody.transform.localScale += new Vector3(0, -_rigidbody.transform.localScale.y / 2);

            //Debug Code
            //movementComponent.Speed = movementComponent.Speed * 1.2f;
            StartCoroutine(DoSlide());

            /*_rigidbody.drag = 0.1f;
            _rigidbody.mass = 0.1f;
            _rigidbody.gravityScale = 4;*/
            Debug.Log("SLIDIIIIIIIING!");
        }
    }

    public IEnumerator DoSlide()
    {
        float CurrentTime = 0;

        while(CurrentTime < SlideTimer)
        {
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            jumpComponent.getFloorCollider().GetContacts(contacts);
            if (contacts.Count > 0)
            {
                Vector2 normal = Vector2.Perpendicular(contacts[0].normal) * -player.FacingDirection;
                _rigidbody.AddForce(normal * (40 * (SlideTimer - CurrentTime)));
            }
            CurrentTime += Time.deltaTime;
            yield return null;
        }
    }
}
