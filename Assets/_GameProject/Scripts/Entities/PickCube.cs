using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour,IPickable
{
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnPicked(Transform attachPoint)
    {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.SetParent(attachPoint);
        _rb.isKinematic = true;
        _rb.useGravity=false;

    }
    public void OnDropped()
    {
        _rb.isKinematic = false;
        _rb.useGravity = true;
        transform.SetParent(null);
    }

 
}
