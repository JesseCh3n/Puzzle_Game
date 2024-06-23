using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IshootStrategy
{
    ShootInteractor _interactor;
    Transform _shootPoint;
    [SerializeField] float _damage;

    public BulletShootStrategy(ShootInteractor interactor)
    {
        _interactor = interactor;
        _shootPoint = interactor.GetShootPoint();
    }

    public void Shoot()
    {
        PooledObject pooledBullet = _interactor._bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
        bullet.velocity = _shootPoint.forward * _interactor.GetShootVelocity();
        _interactor._bulletPool.DestroyPooledObject(pooledBullet,5);
    }

}
