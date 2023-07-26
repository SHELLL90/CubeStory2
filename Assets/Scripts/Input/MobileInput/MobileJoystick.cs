using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class MobileJoystick : OnScreenControl, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;
    [Header("Force Side")]
    [SerializeField] private MobileInputEvent forceLeft;
    [SerializeField] private MobileInputEvent forceRight;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    private Vector3 _touchPosition;
    private Vector3 _lastDirection;

    public void OnPointerDown(PointerEventData eventData)
    {
        _touchPosition = eventData.position;
        
        SendDirection();

        Vector3 direction = GetDirection();
        if (direction.x > 0)
        {
            forceRight.SendEvent(1.0f);
        } 
        else if (direction.x < 0)
        {
            forceLeft.SendEvent(1.0f);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        _touchPosition = eventData.position;

        SendDirection();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _touchPosition = transform.position;

        SendDirection();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        forceLeft.SendEvent(0.0f);
        forceRight.SendEvent(0.0f);
        _touchPosition = transform.position;

        SendDirection();
    }

    private void SendDirection()
    {
        _touchPosition.y = transform.position.y;
        Vector3 direction = GetDirection();

        bool needSend = false;

        if (direction.x != 0)
        {
            if (direction.x > 0 && _lastDirection.x <= 0)
            {
                needSend = true;
            }
            else if (direction.x < 0 && _lastDirection.x >= 0)
            {
                needSend = true;
            }
        }

        if (direction.x == 0) needSend = true;
        
        if (needSend) SendValueToControl((Vector2)direction);

        _lastDirection = direction;
    }

    private Vector3 GetDirection()
    {
        return (_touchPosition - transform.position).normalized;
    }
}
