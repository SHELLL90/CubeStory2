using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps { Player, None }

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private ActionMaps startActionMap;

    private MainPlayerInput _input;

    private PlayerInput _playerInput;

    //private void OnEnable()
    //{
    //    Instance = this;
    //    _input.Enable();

    //    _input.Player.Move.performed += OnMove;
    //    _input.Player.Move.canceled += OnMove;
    //    _input.Player.Move.started += OnMove;

    //    _input.Player.Jump.performed += OnJump;
    //    _input.Player.Jump.canceled += OnJump;
    //    _input.Player.Jump.started += OnJump;


    //    _input.Player.Left.performed += OnLeft;
    //    _input.Player.Left.canceled += OnLeft;
    //    _input.Player.Left.started += OnLeft;

    //    _input.Player.Right.performed += OnRight;
    //    _input.Player.Right.canceled += OnRight;
    //    _input.Player.Right.started += OnRight;

    //    _input.Player.Down.performed += OnDown;
    //    _input.Player.Down.canceled += OnDown;
    //    _input.Player.Down.started += OnDown;

    //    _input.Player.Attack.performed += OnAttack;
    //    _input.Player.Attack.canceled += OnAttack;
    //    _input.Player.Attack.started += OnAttack;
    //}

    //private void OnDisable()
    //{
    //    Instance = null;
    //    _input.Disable();
    //}

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Awake()
    {
        //if (Instance != null)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}

        Instance = this;
        //transform.SetParent(null);

        //DontDestroyOnLoad(this.gameObject);

        _playerInput = GetComponent<PlayerInput>();
        SwitchActionMap(startActionMap);
        //_input = new MainPlayerInput();
    }

    #region Player
    public static Action<Vector2> ActionMove { get; set; }
    public static Action ActionJump { get; set; }
    public static Action ActionLeft { get; set; }
    public static Action ActionRight { get; set; }
    public static Action ActionDown { get; set; }
    public static Action ActionAttack { get; set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 axis = context.ReadValue<Vector2>();
        ActionMove?.Invoke(axis);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ActionJump?.Invoke();
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ActionLeft?.Invoke();
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ActionRight?.Invoke();
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ActionDown?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ActionAttack?.Invoke();
        }
    }


    #endregion Player
    #region ActionMap

    private string _lastActionMap;

    public void SwitchActionMap(ActionMaps actionMap)
    {
        SwitchActionMap(actionMap.ToString());
    }
    public void SwitchActionMap(string actionMap)
    {
        //_lastActionMap
        //if (actionMap == "Player") _input.Player.Enable();
        //else _input.Player.Disable();
        _lastActionMap = _playerInput.currentActionMap.name;
        Debug.Log("Last Action Map: " + _lastActionMap);
        _playerInput.SwitchCurrentActionMap(actionMap);
    }
    public void SwitchToLastActionMap()
    {
        SwitchActionMap(_lastActionMap);
    }

    #endregion ActionMap
}
