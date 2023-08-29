using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public static PlayerHealth Health { get; private set; }

    private void OnEnable()
    {
        TryGetComponents();
    }

    private void OnDisable()
    {
        SetNullComponents();
    }

    private void TryGetComponents()
    {
        Health = GetComponent<PlayerHealth>();
    }

    private void SetNullComponents()
    {
        Health = null; 
    }
}
