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

    private Animator _animator;
    private PlayerStates _playerStates;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerStates = GetComponent<PlayerStates>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChoiceNecessary(collision.relativeVelocity.magnitude, collision.GetContact(0).point);
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
