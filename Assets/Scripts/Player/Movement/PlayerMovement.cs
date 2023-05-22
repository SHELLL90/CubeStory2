using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStates))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float defaultMaxSpeed = 15.0f;
    [SerializeField] private float defaultAcceleration = 30.0f;
    [Header("Torque")]
    [SerializeField] private float defaultSpeedTorque = 10.0f;
    [SerializeField] private ForceMode2D forceModeTorque;

    private float _maxSpeed;
    public float MaxSpeed
    {
        get { return _maxSpeed; }
        private set { _maxSpeed = value; }
    }

    private float _acceleration;
    public float Acceleration
    {
        get { return _acceleration; }
        private set { _acceleration = value; }
    }


    private Rigidbody2D _rigidbody;
    private PlayerStates _playerStates;

    private Vector2 _directionMove;

    private void OnEnable()
    {
        InputManager.ActionMove += SetMoveDirection;
    }

    private void OnDisable()
    {
        InputManager.ActionMove -= SetMoveDirection;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerStates = GetComponent<PlayerStates>();

        MaxSpeed = defaultMaxSpeed;
        Acceleration = defaultAcceleration;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_rigidbody.velocity.magnitude < MaxSpeed)
        {
            _rigidbody.AddForce(_directionMove * Acceleration);
            if (!_playerStates.IsGroundDown || _directionMove.x != 0) _rigidbody.AddTorque(-_directionMove.x * defaultSpeedTorque, forceModeTorque);
        }
    }

    private void SetMoveDirection(Vector2 direction)
    {
        _directionMove = direction;
    }
}
