using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private UnityEvent unityEvent;
    [Header("Time Delay")]
    [SerializeField] private float timeDelay;

    public void StartDelay()
    {
        StopAllCoroutines();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(timeDelay);
        unityEvent?.Invoke();
    }
}
