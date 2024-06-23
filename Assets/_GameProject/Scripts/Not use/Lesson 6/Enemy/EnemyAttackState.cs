using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    Health _playerHeath;
    float _damagePerSecond = 20f;

    float _distanceToPlayer;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {

        _playerHeath = enemy._player.GetComponent<Health>();
    }
    void Attack()
    {
        if (_playerHeath != null)
        {
            _playerHeath.DeductHealth(_damagePerSecond * Time.deltaTime);
        }
    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy is attacking the player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy Stopped attacking the Player");
    }

    public override void OnStateUpdate()
    {
        Attack();
        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);
            if (_distanceToPlayer > 2)
            {
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }
            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            _enemy.ChangeState(new EnemyIdleState(_enemy));

        }

    }

}
