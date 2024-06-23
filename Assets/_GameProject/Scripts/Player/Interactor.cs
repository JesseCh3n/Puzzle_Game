using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
   protected PlayerInput _input;

    public  virtual void Start()
    {
        //   _input = GetComponent<PlayerInput>();

        _input = PlayerInput.GetInstance();
    }

    void Update()
    {
        Interact();
    }

    public abstract void Interact();

}
