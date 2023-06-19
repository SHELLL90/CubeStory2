using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Down, Up, Left, Right }
public class PlayerStates : MonoBehaviour
{
    public System.Action ActionForceDown { get; set; }
    public System.Action ActionForceUp { get; set; }
    public System.Action ActionForceLeft { get; set; }
    public System.Action ActionForceRight { get; set; }

    public System.Action<Side, float> ActionLanded;
    public System.Action ActionLandedDown { get; set; }
    public System.Action ActionLandedLeft { get; set; }
    public System.Action ActionLandedRight { get; set; }
    public System.Action ActionLandedUp { get; set; }
    public System.Action ActionDamage { get; set; }
    public System.Action<bool> ActionDead { get; set; }

    public bool IsMovement { get; set; }
    public float VelocityMagnitude { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 DiretionMovement { get; set; }

    private bool _isDead;
    public bool IsDead
    {
        get { return _isDead; }
        set 
        { 
            _isDead = value;
            ActionDead?.Invoke(_isDead);
        }
    }


    private bool _isGroundDown;
    public bool IsGroundDown
    {
        get { return _isGroundDown; }
        set 
        {
            if (_isGroundDown != value && _isGroundDown == false && value == true)
            {
                ActionLandedDown?.Invoke();
                ActionLanded?.Invoke(Side.Down, VelocityMagnitude);
            }
            _isGroundDown = value;
        }
    }

    private bool _isGroundLeft;
    public bool IsGroundLeft
    {
        get { return _isGroundLeft; }
        set 
        {
            if (_isGroundLeft != value && _isGroundLeft == false && value == true)
            {
                ActionLandedLeft?.Invoke();
                ActionLanded?.Invoke(Side.Left, VelocityMagnitude);
            }
            _isGroundLeft = value;
        }
    }

    private bool _isGroundRight;
    public bool IsGroundRight
    {
        get { return _isGroundRight; }
        set 
        {
            if (_isGroundRight != value && _isGroundRight == false && value == true)
            {
                ActionLandedRight?.Invoke();
                ActionLanded?.Invoke(Side.Right, VelocityMagnitude);
            }
            _isGroundRight = value;
        }
    }

    private bool _isGroundUp;
    public bool IsGroundUp
    {
        get { return _isGroundDown; }
        set
        {
            if (_isGroundUp != value && _isGroundUp == false && value == true)
            {
                ActionLandedUp?.Invoke();
                ActionLanded?.Invoke(Side.Up, VelocityMagnitude);
            }
            _isGroundUp = value;
        }
    }

    public System.Action<bool> ActionTargetSearched { get; set; }

    private bool _targetSearched;

    public bool TargetSearched
    {
        get { return _targetSearched; }
        set 
        {
            if (_targetSearched != value)
            {
                ActionTargetSearched?.Invoke(value);
            }
            _targetSearched = value;
        }
    }

    public System.Action<float> ActionReloading { get; set; }
}
