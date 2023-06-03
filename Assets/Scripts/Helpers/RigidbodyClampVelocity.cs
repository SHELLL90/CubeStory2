using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyClampVelocity : MonoBehaviour
{
    [SerializeField] private float maxVelocity;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ClampVelocity();
    }

    private void ClampVelocity()
    {
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxVelocity);
    }
}
