using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    public UnityEvent _OnPush;
    public UnityEvent _OnHoverEnter;
    public UnityEvent _OnHoverExit;

    public void OnHoverEnter()
    {
        _OnHoverEnter?.Invoke();
    }
    public void OnHoverExit()
    {
        _OnHoverExit?.Invoke();
    }

    public void OnSelect()
    {
        _OnPush?.Invoke();
    }


}
