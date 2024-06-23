using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isUp;
    private bool _isAcross;
    bool _isMoving;
    Vector3 _targetPosition;


    public void ToggleLift()
    {
        if (_isMoving)
        {
            return;
        }
        else if (_isUp)
        {
            _targetPosition = transform.localPosition - new Vector3(0, 5f, 0);
            _isUp = false;
        }
        else
        {
            _targetPosition = transform.localPosition + new Vector3(0, 5f, 0);
            _isUp = true;
        }

        _isMoving = true;
    }

    void Update()
    {
        if (_isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, _speed * Time.deltaTime);
        }
      
        if (Vector3.Distance(transform.localPosition, _targetPosition) < 0.06f)
        {
            _isMoving = false;
        }
    }
}
