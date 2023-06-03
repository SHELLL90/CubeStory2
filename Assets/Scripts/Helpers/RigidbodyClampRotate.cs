using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyClampRotate : MonoBehaviour
{
    [SerializeField] private float maxRotateSpeed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ClampSpeedRotate();
    }

    private void ClampSpeedRotate()
    {
        _rigidbody.angularVelocity = Mathf.Clamp(_rigidbody.angularVelocity, -maxRotateSpeed, maxRotateSpeed);
    }
}
