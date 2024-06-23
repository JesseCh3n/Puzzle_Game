using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private float _timer;
    private float _destroyTime = 0;
    ObjectPool _associatedPool;
    private bool _setToDestroy = false;
    [SerializeField] float _damage;

    public void SetObjectPool(ObjectPool pool)
    {
        _timer = 0;
        _destroyTime = 0;
        _associatedPool = pool;
        _setToDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_setToDestroy)
        {
            _timer += Time.deltaTime;
            if (_timer >= _destroyTime)
            {
                _setToDestroy = false;
                _timer = 0;
                Destroy();
            }
        }
    }
    public void Destroy()
    {
        if (_associatedPool != null)
        {

            _associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time)
    {
        _setToDestroy = true;
        _destroyTime = time;
    }

    void Damage(IDamageable damage)
    {
        if (damage != null)
        {
            damage.GetDamage(_damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        Health damageable = other.GetComponent<Health>();
        damageable.DeductHealth(_damage);
    }

}
