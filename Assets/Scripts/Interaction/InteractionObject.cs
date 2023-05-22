using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionObject : MonoBehaviour
{
    [Header("Event")]
    public UnityEvent unityEvent;

    public void EventInvoke()
    {
        unityEvent?.Invoke();
    }
}
