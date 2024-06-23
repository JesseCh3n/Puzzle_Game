using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePadCounter : MonoBehaviour
{
    public UnityEvent onUnlock;
    public UnityEvent onLock;

    private bool isLocked1 = false;
    private bool isLocked2 = false;

    // Start is called before the first frame update
    public void onActivation1()
    {
        isLocked1 = true;
    }

    public void onDeactivation1()
    {
        isLocked1 = false;
    }
    public void onActivation2()
    {
        isLocked2 = true;
    }

    public void onDeactivation2()
    {
        isLocked2 = false;
    }

    public void unlockDoor()
    {
        if (isLocked1 && isLocked2)
        {
            onUnlock?.Invoke();
        }
    }

    public void lockDoor()
    {
        if (!isLocked1 || !isLocked2)
        {
            onLock?.Invoke();
        }
    }

    private void Update()
    {
        unlockDoor();
        lockDoor();
    }
}
