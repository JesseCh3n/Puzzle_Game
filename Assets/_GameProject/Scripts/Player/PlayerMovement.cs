using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

   // private PlayerInput _playerInput;

    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _moveMultiplier = 1;
    [SerializeField] private float _sprintMultiplier = 2;
    private CharacterController _characterController;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundCheckDistance;
    private Vector3 _playerVelocity;
    public bool _isGrounded { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
       // _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }


    private void MovePlayer()
    {
        _moveMultiplier = PlayerInput.GetInstance()._sprintHeld ? _sprintMultiplier : 1;
        _characterController.Move((transform.forward * PlayerInput.GetInstance()._vertical + transform.right * PlayerInput.GetInstance()._horizontal) * _moveSpeed * Time.deltaTime * _moveMultiplier);

        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);

    }
    private void PlayerGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundLayerMask);
    //    Debug.Log(_isGrounded);
    
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        PlayerGrounded();
    }

    public void SetYVelocity(float value)
    {
        _playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return PlayerInput.GetInstance()._vertical * _moveSpeed * _moveMultiplier;
    }
}
