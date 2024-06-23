using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : Interactor
{

    [SerializeField] private float _jumpVelocity;
    protected PlayerMovement _movement;

    public override void Start()
    {
        base.Start();
        _movement = GetComponent<PlayerMovement>();
    }

    public override void Interact()
    {
      //  Debug.Log("ISUpdating");
        if (_input._jumpPressed && _movement._isGrounded)
        {
            _movement.SetYVelocity(_jumpVelocity);
        }
    }

    //public override void Update()
    //{
    //    base.Update();
    //}

}
