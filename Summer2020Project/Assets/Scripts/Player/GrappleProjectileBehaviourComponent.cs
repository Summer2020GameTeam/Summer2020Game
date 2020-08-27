using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProjectileBehaviourComponent : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D ParentBody;
    [HideInInspector]
    public float LenghtSpan = 7;
    private float CurrentLength = 0;
    [HideInInspector]
    public float GrappleForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Vector2 forceVector = CalculateForceVector();
            ParentBody.AddForce(forceVector * GrappleForce, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }

    private Vector2 CalculateForceVector()
    {
        Vector2 forceVector = new Vector2();
        forceVector = transform.position - ParentBody.transform.position;
        forceVector = forceVector.normalized;
        return forceVector;
    }

    private void Update()
    {
        if (Vector2.Distance(ParentBody.transform.position, transform.position) >= LenghtSpan)
        {
            Destroy(gameObject);
        }
    }
}
