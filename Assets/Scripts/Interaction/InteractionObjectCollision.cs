using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjectCollision : InteractionObject
{
    [Header("Setting")]
    [SerializeField] private float relativeVelocityInteraction = 10.0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= relativeVelocityInteraction)
        {
            EventInvoke();
        }
    }
}
