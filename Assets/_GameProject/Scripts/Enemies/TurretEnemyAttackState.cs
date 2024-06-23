using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyAttackState : TurretEnemyState
{
    Health _playerHeath;

    float _distanceToPlayer;
    private float _timer;

    public TurretEnemyAttackState(TurretEnemyController enemy) : base(enemy)
    {
        _playerHeath = enemy._player.GetComponent<Health>();
    }
    public void Shoot()
    {
        PooledObject pooledBullet = _enemy._bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = _enemy._shootPosition.position;
        bullet.transform.rotation = _enemy._shootPosition.rotation;
        bullet.velocity = _enemy._agent.velocity*_enemy._bulletVelocity;
        _enemy._bulletPool.DestroyPooledObject(pooledBullet, 5);
    }

    public override void OnStateEnter()
    {
        _timer = _enemy._shootingRate;
        //Debug.Log("Turret enemy is attacking the player");
    }

    public override void OnStateExit()
    {
        //Debug.Log("Turret enemy Stopped attacking the Player");
    }

    public override void OnStateUpdate()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        } else if (_timer <= 0)
        {
            Shoot();
            _timer = _enemy._shootingRate;
        }
        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);
            if (_distanceToPlayer > 20)
            {
                _enemy.ChangeState(new TurretEnemyIdleState(_enemy));
            }
            _enemy._agent.destination = _enemy._player.position;
        }
    }

}
