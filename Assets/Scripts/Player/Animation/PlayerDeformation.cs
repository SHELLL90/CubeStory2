using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerStates))]
public class PlayerDeformation : MonoBehaviour
{
    [Header("Animation Landed Names")]
    [SerializeField] private DeformationSetting landedDown;
    [SerializeField] private DeformationSetting landedUp;
    [SerializeField] private DeformationSetting landedLeft;
    [SerializeField] private DeformationSetting landedRight;
    [Header("Animation Movement Names")]
    [SerializeField] private DeformationSetting idle;
    [SerializeField] private DeformationSetting movementLeft;
    [SerializeField] private DeformationSetting movementRight;

    private Animator _animator;
    private PlayerStates _playerStates;

    private bool _canPlayLandedAnimation = true;

    private int _frames = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerStates = GetComponent<PlayerStates>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChoiceNecessary(collision.relativeVelocity.magnitude, collision.GetContact(0).point);
    }

    private void FixedUpdate()
    {
        if (_frames % 5 == 0) MovementDeformation();
        _frames++;
    }

    private void MovementDeformation()
    {
        if (_playerStates.IsGroundDown && _playerStates.IsMovement)
        {
            _animator.SetBool(idle.nameAnimation, false);
            
            if (_playerStates.DiretionMovement.x > 0 && movementRight.velocityToPlay <= _playerStates.VelocityMagnitude)
            {
                _animator.SetBool(movementRight.nameAnimation, true);
                _animator.SetBool(movementLeft.nameAnimation, false);
            }
            else if (_playerStates.DiretionMovement.x < 0 && movementLeft.velocityToPlay <= _playerStates.VelocityMagnitude)
            {
                _animator.SetBool(movementRight.nameAnimation, false);
                _animator.SetBool(movementLeft.nameAnimation, true);
            }
            _canPlayLandedAnimation = false;
        }
        else if (!_canPlayLandedAnimation)
        {
            _animator.SetBool(idle.nameAnimation, true);

            _animator.SetBool(movementRight.nameAnimation, false);
            _animator.SetBool(movementLeft.nameAnimation, false);

            _canPlayLandedAnimation = true;
        }
    }

    private void ChoiceNecessary(float velocity, Vector2 point)
    {
        float angle = Vector2.SignedAngle(point - (Vector2)transform.position, transform.right);

        if (angle <= 45 && angle >= -45) TryPlay(landedRight, velocity);
        else if (angle <= 135 && angle >= 45) TryPlay(landedDown, velocity);
        else if (angle <= -135 || angle >= 135) TryPlay(landedLeft, velocity);
        else if (angle <= -45 && angle >= -135) TryPlay(landedUp, velocity);
    }

    private void TryPlay(DeformationSetting deformationSetting)
    {
        if (_playerStates.VelocityMagnitude >= deformationSetting.velocityToPlay)
        {
            _animator.Play(deformationSetting.nameAnimation);
        }
    }
    private void TryPlay(DeformationSetting deformationSetting, float velocity)
    {
        if (velocity >= deformationSetting.velocityToPlay)
        {
            _animator.Play(deformationSetting.nameAnimation);
        }
    }
}

[System.Serializable]
public class DeformationSetting
{
    public string nameAnimation;
    public float velocityToPlay = 10.0f;
}
