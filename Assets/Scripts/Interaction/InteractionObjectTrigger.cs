using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjectTrigger : InteractionObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventInvoke();
    }
}
