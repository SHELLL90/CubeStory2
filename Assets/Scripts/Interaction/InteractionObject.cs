using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private bool disposable = true;
    [Header("Event")]
    public UnityEvent unityEvent;

    public void EventInvoke()
    {
        unityEvent?.Invoke();
        if (disposable) Destroy(this);
    }
}
