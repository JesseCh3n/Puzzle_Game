using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _interactionDistance;
    private RaycastHit _rayCastHit;
    private ISelectable _selectable;


    [Header("Pick and Drop")]
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private Transform _attachTransfrom;
    [SerializeField] private float _pickupDistance;
    private bool _isPicked;
    private IPickable _pickable;

    //Move
    [Header("Move")]
    private CharacterController _characterController;
    private float _horizontal, _vertical;
    [SerializeField] private float _moveSpeed;
    private float _moveMultiplier = 1;
    //Move





    //Rotation
    [Header("Rotation")]
    [SerializeField] private float _turnSpeed;
    private float _mouseX, _mouseY;
    [SerializeField] private Transform _cameraTransform;
    private float _cameraRotation;
    [SerializeField] private bool _invertMouse;

    //Rotation

    //Check Ground & Jump
    [Header("Jump")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private float _jumpVelocity;
    private bool _isGrounded;
    private Vector3 _playerVelocity;
    private float _gravity = -9.81f;
    //Check Ground & Jump

    //Shoot
    [Header("Shoot")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Rigidbody _rocketPrefab;
    [SerializeField] private float _shootForce;
    // [SerializeField] private float _rocketForce;
    [SerializeField] private Transform _shootPoint;
    //Shoot

    //Sprint
    [SerializeField] private float _sprintMultiplier = 2;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        PlayerGrounded();
        PlayerJump();
        ShootBullet();
        ShootRocket();
        Interact();
        PickAndDrop();
    }


    private void GetInput()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        _moveMultiplier = Input.GetButton("Sprint") ? _sprintMultiplier : 1;
    }
    private void MovePlayer()
    {
        _characterController.Move((transform.forward * _vertical + transform.right * _horizontal) * _moveSpeed * Time.deltaTime * _moveMultiplier);

        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
    private void RotatePlayer()
    {
        //Rotate the Player Horizontally
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        //Lets Rotate vertically

        _cameraRotation += Time.deltaTime * _mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _cameraRotation = Mathf.Clamp(_cameraRotation, -40f, 40f);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);

    }
    private void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            bullet.AddForce(_shootPoint.forward * _shootForce);
            Destroy(bullet.gameObject, 5f);
        }
    }
    private void ShootRocket()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody rocket = Instantiate(_rocketPrefab, _shootPoint.position, _shootPoint.rotation);
            rocket.AddForce(_shootPoint.forward * _shootForce);
            Destroy(rocket.gameObject, 5.0f);
        }
    }
    private void PlayerGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundLayerMask);
    }
    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerVelocity.y = _jumpVelocity;
        }
    }
    private void Interact()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _rayCastHit, _interactionDistance, _interactionLayer))
        {
            _selectable = _rayCastHit.transform.GetComponent<ISelectable>();

            if (_selectable != null)
            {
                _selectable.OnHoverEnter();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _selectable.OnSelect();
                }

            }
        }

        if (_rayCastHit.transform == null & _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }

    }
    private void PickAndDrop()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _rayCastHit, _pickupDistance, _pickupLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isPicked)
            {
                _pickable = _rayCastHit.transform.GetComponent<IPickable>();
                if (_pickable == null) return;

                _pickable.OnPicked(_attachTransfrom);
                _isPicked = true;
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }

    }

}
