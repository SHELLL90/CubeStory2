using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps { Player, None }

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    #region Player
    public static Action<Vector2> ActionMove { get; set; }
    public static Action ActionJump { get; set; }
    public static Action ActionLeft { get; set; }
    public static Action ActionRight { get; set; }
    public static Action ActionDown { get; set; }

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


    #endregion Player

    public void SwitchActionMap(ActionMaps actionMap)
    {
        _playerInput.SwitchCurrentActionMap(actionMap.ToString());
    }

}