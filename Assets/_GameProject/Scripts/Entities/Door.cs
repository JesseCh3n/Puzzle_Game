using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //[SerializeField] private MeshRenderer _doorRenderer;
    //[SerializeField] private Material _defaultDoorColor;
    //[SerializeField] private Material _detectedDoorColor;
    [SerializeField] private Animator _doorAnimator;
    private float _timer = 0;
    private const float _waitTime = 1.0f;

    [SerializeField] private bool _isLocked = true;
   
    
    public void LockDoor()
    {
        _isLocked = true;
       // OpenDoor(false);
        _doorAnimator.SetBool("Door", false);
    }

    public void UnlockDoor()
    {
        _isLocked = false;
    }
    public void OpenDoor(bool state)
    {
        if (!_isLocked)
        {
            _doorAnimator.SetBool("Door", state);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isLocked)
        {
            //_doorRenderer.material = _detectedDoorColor;
            _timer = 0;
            //   Debug.Log("Player Entered to the Trigger Detection");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (_isLocked)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= _waitTime)
        {
            _timer = _waitTime;
            // _doorAnimator.SetBool("Door",true);

            OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //   _doorRenderer.material = _defaultDoorColor;
       // _doorAnimator.SetBool("Door", false);
        OpenDoor(false);
        Debug.Log("Door closed");
    }
    
}
