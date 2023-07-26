using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private bool disposable = true;
    [SerializeField] private float delay = 0.0f;
    [Header("Event")]
    public UnityEvent unityEvent;

    private bool _waiting;

    public void EventInvoke()
    {
        if (_waiting) return;
        StartCoroutine(WaitInvoke());
    }

    private IEnumerator WaitInvoke()
    {
        _waiting = true;
        yield return new WaitForSecondsRealtime(delay);

        unityEvent?.Invoke();
        if (disposable) Destroy(this);
        else _waiting = false;
    }
}
