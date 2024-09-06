using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerInput : MonoBehaviour
{
    private Dictionary<EventKey, UnityEvent> _events = new Dictionary<EventKey, UnityEvent>();

    public static PlayerInput Get()
    {
        return FindObjectOfType<PlayerInput>();
    }
    public void AddListener(EventKey key, UnityAction action)
    {
        GetEvent(key).AddListener(action);
    }
    public void RemoveListener(EventKey key, UnityAction action)
    {
        GetEvent(key).RemoveListener(action);
    }
    protected UnityEvent GetEvent(EventKey key)
    {
        if (_events.ContainsKey(key))
        {
            return _events[key];
        }
        else
        {
            return _events[key] = new UnityEvent();
        }

    }
    public abstract bool GetInputAction(InputKey key);
    public abstract Vector2 GetInputAxis(AxisKey key);
    public abstract Vector2 GetInputAxisRaw(AxisKey key);

}
public enum EventKey
{
}
public enum InputKey
{
    PointerDown 
}
public enum AxisKey
{
    PointerPosition,
    Swipe
}