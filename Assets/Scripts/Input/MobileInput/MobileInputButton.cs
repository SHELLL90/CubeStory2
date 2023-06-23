using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class MobileInputButton : OnScreenControl, IPointerExitHandler, IPointerEnterHandler
{
    [InputControl(layout = "Button")]
    [SerializeField]
    private string m_ControlPath;

    private bool _pressed;

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_pressed)
        {
            SendValueToControl(0.0f);
            _pressed = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_pressed)
        {
            SendValueToControl(1.0f);
            _pressed = true;
        }
    }

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }
}
