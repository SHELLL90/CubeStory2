using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStates))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerForce : MonoBehaviour
{
    [SerializeField] private PlayerForceSetting forceDown;
    [SerializeField] private PlayerForceSetting forceLeft;
    [SerializeField] private PlayerForceSetting forceRight;
    [Header("Other")]
    [SerializeField] private float delayBetweenTryForceSide = 0.1f;
    [SerializeField] private ForceMode2D forceMode;
    [Header("Force Down Platform Setting")]
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float lengthRay;

    private Rigidbody2D _rigidbody;
    private PlayerStates _playerStates;

    private float _lastTimeTryForceLeft;
    private float _lastTimeTryForceRight;

    private float _timeCanNextForceDown;
    private float _timeCanNextForceSide;

    private void OnEnable()
    {
        InputManager.ActionDown += TryForceDown;
        InputManager.ActionLeft += TryForceLeft;
        InputManager.ActionRight += TryForceRight;
    }

    private void OnDisable()
    {
        InputManager.ActionDown -= TryForceDown;
        InputManager.ActionLeft -= TryForceLeft;
        InputManager.ActionRight -= TryForceRight;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerStates = GetComponent<PlayerStates>();
    }

    private void TryForceDown()
    {
        if (_timeCanNextForceDown < Time.time)
        {
            bool bottomPlatform = TryIgnorePlatform();
            if (_playerStates.IsGroundDown && !bottomPlatform)
            {
                return;
            }

            Force(forceDown);
            _timeCanNextForceDown = Time.time + forceDown.delayBetweenForce;

            _playerStates.ActionForceDown?.Invoke();
        }
    }

    // Platform down

    private bool TryIgnorePlatform()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, lengthRay, platformLayer.value);
        if (hit.collider != null)
        {
            PlatformIngorePlayer platformIngorePlayer = hit.transform.GetComponent<PlatformIngorePlayer>();
            if (platformIngorePlayer != null)
            {
                platformIngorePlayer.IgnorePlayer();
                return true;
            }
        }
        return false;
    }
    //

    private void TryForceLeft()
    {
        if (_lastTimeTryForceLeft + delayBetweenTryForceSide > Time.time && _timeCanNextForceSide < Time.time && !_playerStates.IsGroundLeft)
        {
            Force(forceLeft);
            _timeCanNextForceSide = Time.time + forceLeft.delayBetweenForce;

            _playerStates.ActionForceLeft?.Invoke();
        }
        _lastTimeTryForceLeft = Time.time;
    }

    private void TryForceRight()
    {
        if (_lastTimeTryForceRight + delayBetweenTryForceSide > Time.time && _timeCanNextForceSide < Time.time && !_playerStates.IsGroundRight)
        {
            Force(forceRight);
            _timeCanNextForceSide = Time.time + forceRight.delayBetweenForce;

            _playerStates.ActionForceRight?.Invoke();
        }
        _lastTimeTryForceRight = Time.time;
    }

    public void Force(PlayerForceSetting forceSetting)
    {
        Vector2 direction = forceSetting.directionForce;
        if (!forceSetting.worldDirection) direction = transform.TransformDirection(direction);
        float powerForce = forceSetting.defaultPowerForce;

        _rigidbody.AddForce(direction * powerForce, forceMode);
    }
}

[System.Serializable]
public class PlayerForceSetting
{
    public float defaultPowerForce;
    public float delayBetweenForce;
    public Vector2 directionForce;
    public bool worldDirection;
}
