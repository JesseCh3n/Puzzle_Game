using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private LayerMask _pickupLayer;
    public UnityEvent _onCubePlaced;
    public UnityEvent _onCubeRemoved;

    [SerializeField] private float _checkRadius;

    private void OnCollisionEnter(Collision collision)
    {
        //check if cube is closer to the middle
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _checkRadius, _pickupLayer);

        foreach(var collider in hitColliders)
        {
            //unlock door if there is at least one cube overlaps
            if(collider.CompareTag("PickCube"))
            {
                _onCubePlaced?.Invoke();
                break;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("PickCube"))
        {
            _onCubeRemoved?.Invoke();
        }
    }

}
