using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public System.Action ActionLanded { get; set; }

    private bool _isGroundDown;
    public bool IsGroundDown
    {
        get { return _isGroundDown; }
        set 
        {
            if (_isGroundDown != value && _isGroundDown == false && value == true)
            {
                ActionLanded?.Invoke();
            }
            _isGroundDown = value;
        }
    }

    private bool _isGroundLeft;
    public bool IsGroundLeft
    {
        get { return _isGroundLeft; }
        set { _isGroundLeft = value; }
    }

    private bool _isGroundRight;
    public bool IsGroundRight
    {
        get { return _isGroundRight; }
        set { _isGroundRight = value; }
    }
}
