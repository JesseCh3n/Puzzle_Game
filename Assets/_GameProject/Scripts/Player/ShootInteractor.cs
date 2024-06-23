using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [Header("Shoot")]
    public ObjectPool _bulletPool;
    public ObjectPool _rocketPool;

    [Header("Gun")]
    public MeshRenderer _gunRenderer;
    public Color _bulletGunColor;
    public Color _rocketGunColor;
    private IshootStrategy _currentShootStrategy;


    [SerializeField] private float _shootVelocity;
    [SerializeField] private Transform _shootPoint;
    private PlayerMovement _playerMovement;
    private float _finalShootVelocity;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    public override void Interact()
    {
        if (_currentShootStrategy == null)
        {
            _currentShootStrategy = new BulletShootStrategy(this);
        }

        if (_input._weapon1Pressed)//Select Bullet
        {
            _currentShootStrategy = new BulletShootStrategy(this);
        }
        if (_input._weapon2Pressed)//Select Rocket
        {
            _currentShootStrategy = new RocketShootStrategy(this);
        }
        if (_input._primaryShootPressed && _currentShootStrategy != null)
        {
            _currentShootStrategy.Shoot();
        }

    }
    public float GetShootVelocity()
    {
        _finalShootVelocity = _playerMovement.GetForwardSpeed() + _shootVelocity;
        return _finalShootVelocity;
    }
    public Transform GetShootPoint()
    {
        return _shootPoint;
    }


}
