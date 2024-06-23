using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IshootStrategy
{
    ShootInteractor _interactor;
    Transform _shootPoint;
    [SerializeField] float _damage;
    public RocketShootStrategy(ShootInteractor interactor)
    {
        _interactor = interactor;
        _shootPoint = _interactor.GetShootPoint();

    }
    public void Shoot()
    {
        PooledObject pooledRocket = _interactor._rocketPool.GetPooledObject();
        pooledRocket.gameObject.SetActive(true);

        Rigidbody rocket = pooledRocket.GetComponent<Rigidbody>();
        rocket.transform.position = _shootPoint.position;
        rocket.transform.rotation = _shootPoint.rotation;

        rocket.velocity = _shootPoint.forward * _interactor.GetShootVelocity();
        _interactor._rocketPool.DestroyPooledObject(pooledRocket, 5);
    }
}
