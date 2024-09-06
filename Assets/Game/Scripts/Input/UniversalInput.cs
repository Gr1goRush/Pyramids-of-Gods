using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UniversalInput : PlayerInput, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool _pointerDown;
    private Vector2 _drag;
    private Vector2 _pointerPos = Vector2.zero;

    private void Awake()
    {
        _pointerPos = new Vector2(Screen.width, Screen.height)/2;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerPos = eventData.position;
        _pointerDown = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _pointerPos = eventData.position;
        _drag = Vector2.zero;
        _pointerDown =false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _drag = eventData.delta;
        _pointerPos = eventData.position;
    }

    public override bool GetInputAction(InputKey key)
    {
        switch (key)
        {
            case InputKey.PointerDown:
                return _pointerDown;
            default:
                return false;
        }
    }

    public override Vector2 GetInputAxis(AxisKey key)
    {
        switch (key)
        {
            case AxisKey.Swipe:
                return _drag;
            case AxisKey.PointerPosition:
                return _pointerPos;
            default:
                return Vector2.zero;
        }
    }

    public override Vector2 GetInputAxisRaw(AxisKey key)
    {
        switch (key)
        {
            case AxisKey.Swipe:
                return _drag.normalized;
            case AxisKey.PointerPosition:
                return _pointerPos;
            default:
                return Vector2.zero;
        }
    }
}
