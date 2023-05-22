using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private AnimationCurve curveMultiplySpeedRotate;
    [SerializeField] private float defaultSpeedRotate = 10.0f;
    [SerializeField] private float maxSpeedRotate = 30.0f;
    [Range(0, 180.0f)][SerializeField] private float offsetToStartRotate = 15.0f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        TryRotatePlayer();
    }

    private void TryRotatePlayer()
    {
        float currentOffset = transform.eulerAngles.z;
        if (Displaced(currentOffset))
        {
            RotatePlayer(currentOffset);
        }
    }

    private void RotatePlayer(float currentOffset)
    {
        float multiplySpeed = CalculationMultiply(currentOffset);
        float speedRotate = defaultSpeedRotate * multiplySpeed;

        //_rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.identity, speedRotate));
        //_rigidbody.angularVelocity = 0;
        if (currentOffset < 180) speedRotate *= -1.0f;
        _rigidbody.AddTorque(speedRotate);
        _rigidbody.angularVelocity = Mathf.Clamp(_rigidbody.angularVelocity, -maxSpeedRotate, maxSpeedRotate);
        //_rigidbody.angularVelocity = speedRotate;
    }

    private bool Displaced(float offset)
    {
        bool displaced = false;

        if (offset > 180) offset = Mathf.Abs(offset - 360.0f);

        if (offset > offsetToStartRotate)
        {
            displaced = true;
        }

        return displaced;
    }

    private float CalculationMultiply(float offset)
    {
        float multiply = 0;

        if (offset <= 180)
        {
            multiply = curveMultiplySpeedRotate.Evaluate(offset / 180.0f);
        }
        else
        {
            multiply = curveMultiplySpeedRotate.Evaluate(Mathf.Abs(offset - 360.0f) / 180.0f);
        }
        return multiply;
    }
}
