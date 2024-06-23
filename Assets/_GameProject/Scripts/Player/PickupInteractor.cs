using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractor : Interactor
{
  
    [Header("Pick and Drop")]
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private Transform _attachTransfrom;
    [SerializeField] private float _pickupDistance;
    [SerializeField] private Camera _cam;
    private bool _isPicked;
    private IPickable _pickable;
    private RaycastHit _rayCastHit;
    public override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _rayCastHit, _pickupDistance, _pickupLayer))
        {
            if (_input._activatePressed && !_isPicked)
            {
                _pickable = _rayCastHit.transform.GetComponent<IPickable>();
                if (_pickable == null) return;

                _pickable.OnPicked(_attachTransfrom);
                _isPicked = true;
                return;
            }
        }
        if (_input._activatePressed && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }

}
