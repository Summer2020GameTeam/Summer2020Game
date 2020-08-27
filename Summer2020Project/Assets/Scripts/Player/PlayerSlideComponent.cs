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
    private float SlideSpeed = 2000;

    public bool IsDashing { get; set; }
    private Rigidbody2D _rigidbody;
    private PlayerComponent player;
    private PlayerMovementComponent movementComponent;
    private PlayerJumpComponent jumpComponent;
    private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    private List<ContactPoint2D> lastCorrectContacts = new List<ContactPoint2D>();


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
        //Asserting if slide is availible and if a surface below player is detected.
        if (!IsDashing && enabled)
        {
            if (player.GetPlayerState() != PlayerState.Sliding)
            {
                try
                {
                    jumpComponent.getFloorCollider().GetContacts(contacts);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message + ": Player not currently touching any floors.");
                    return;
                }
            }

            //Once a contact surface has been detected and confirmed, execute actual slide logic.
            if (contacts.Count > 0)
            {
                player.SetPlayerState(PlayerState.Sliding);
                _rigidbody.transform.localScale += new Vector3(0, -_rigidbody.transform.localScale.y / 2);
                movementComponent.Speed = movementComponent.Speed / 2;
                StartCoroutine(DoSlide(contacts));
            }
        }
    }

    public IEnumerator DoSlide(List<ContactPoint2D> contacts)
    {
        float CurrentTime = 0;
        Vector2 normal;
        
        while (CurrentTime < SlideTimer)
        {
            try
            {
                jumpComponent.getFloorCollider().GetContacts(contacts);
            }
            catch(System.Exception e) 
            {

            }

            if(contacts.Count > 0)
            {
                normal = Vector2.Perpendicular(contacts[0].normal) * player.FacingDirection;
                lastCorrectContacts.Clear();
                foreach(ContactPoint2D point in contacts)
                {
                    lastCorrectContacts.Add(point);
                }
            }
            else
            {
                normal = Vector2.Perpendicular(lastCorrectContacts[0].normal) * player.FacingDirection;
            }

            float boostMultiplier = SlideSpeed;
            float angle = Mathf.Atan2(normal.x, normal.y)*180/(float)System.Math.PI;
            if (angle > -90 && angle < -20)
            {
                angle = angle * -1;
                angle -= 20;
                angle = 70 - angle;
                boostMultiplier = (boostMultiplier * (angle / 70) + (boostMultiplier * 0.28f)) * -1;
            }
            else if(angle < 90 && angle > 20)
            {
                angle -= 20;
                angle = 70 - angle;
                boostMultiplier = (boostMultiplier * (angle / 70) + (boostMultiplier * 0.28f)) * -1;
            }
            else if(angle < 160 && angle > 90)
            {
                angle -= 90;
                boostMultiplier = (boostMultiplier * (angle / 70) + (boostMultiplier * 0.28f));
            }
            else if(angle > -160 && angle < -90)
            {
                angle = angle * -1;
                angle -= 90;
                boostMultiplier = (boostMultiplier * (angle / 70) + (boostMultiplier * 0.28f));
            }
            else
            {
                boostMultiplier = boostMultiplier * 0.28f;
            }

            _rigidbody.AddForce(normal * ((boostMultiplier * (1 - (CurrentTime / SlideTimer))) - (boostMultiplier/4) * (CurrentTime / SlideTimer)) * Time.deltaTime);
            CurrentTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(StopSlide());
    }

    public IEnumerator StopSlide()
    {
        player.SetPlayerState(PlayerState.Default);

        movementComponent.Speed = movementComponent.Speed * 2;

        _rigidbody.transform.localScale += new Vector3(0, _rigidbody.transform.localScale.y);

        yield return null;
    }
}
