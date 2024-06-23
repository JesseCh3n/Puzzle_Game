using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
 //   [SerializeField] private PlayerInput _input;
    private float _cameraRotation;
    [SerializeField] private bool _invertMouse = false;
    [SerializeField] private float _turnSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }
    private void RotateCamera()
    {
        _cameraRotation += Time.deltaTime * PlayerInput.GetInstance()._mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _cameraRotation = Mathf.Clamp(_cameraRotation, -40f, 40f);
        transform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);
    }

}
