using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStates))]
[RequireComponent(typeof(PlayerIsGround))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int numberJumps = 2;
    [SerializeField] private float delayBetweenJumps = 0.05f;
    [SerializeField] private JumpForceSetting jumpUp;
    [SerializeField] private JumpForceSetting jumpLeft;
    [SerializeField] private JumpForceSetting jumpRight;
    [Header("Other")]
    [SerializeField] private ForceMode2D forceMode = ForceMode2D.Force;

    public int CurrentNumberJumps { get; set; }

    private float _timeCanNextJump;

    private Rigidbody2D _rigidbody;
    private PlayerStates _playerStates;

    private void OnEnable()
    {
        InputManager.ActionJump += TryJump;
    }

    private void OnDisable()
    {
        InputManager.ActionJump -= TryJump;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerStates = GetComponent<PlayerStates>();

        _playerStates.ActionLanded += ResetNumberJumps;

        ResetNumberJumps();
    }

    private void TryJump()
    {
        if (CurrentNumberJumps <= 0 || _timeCanNextJump > Time.time) return;
        bool jumped = false;
        if (!_playerStates.IsGroundDown)
        {
            if (_playerStates.IsGroundLeft && _playerStates.IsGroundRight)
            {
                Jump(jumpUp);
                jumped = true;
            }
            else
            {
                if (_playerStates.IsGroundLeft)
                {
                    Jump(jumpRight);
                    jumped = true;
                }
                else if (_playerStates.IsGroundRight)
                {
                    Jump(jumpLeft);
                    jumped = true;
                }
            }
        }
        if (!jumped) Jump(jumpUp);
    }

    private void Jump(JumpForceSetting setting)
    {
        _rigidbody.AddForce(setting.worldDirectionJump * setting.defaultJumpForce, forceMode);
        CurrentNumberJumps--;

        _timeCanNextJump = Time.time + delayBetweenJumps;
    }

    private void ResetNumberJumps()
    {
        CurrentNumberJumps = numberJumps;
    }
}

[System.Serializable]
public class JumpForceSetting
{
    public float defaultJumpForce;
    public Vector2 worldDirectionJump = Vector2.up;
}
