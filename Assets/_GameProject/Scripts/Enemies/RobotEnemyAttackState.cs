using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemyAttackState : RobotEnemyState
{
    Health _playerHeath;

    float _distanceToPlayer;

    public RobotEnemyAttackState(RobotEnemyController enemy) : base(enemy)
    {

        _playerHeath = enemy._player.GetComponent<Health>();
    }

    public override void OnStateEnter()
    {
        _enemy._laser.gameObject.SetActive(true);
        //Debug.Log("Robot enemy is attacking the player");
    }

    public override void OnStateExit()
    {
        _enemy._laser.gameObject.SetActive(false);
        //Debug.Log("Robot enemy Stopped attacking the Player");
    }

    public override void OnStateUpdate()
    {
        //Attack via Laser scripts
        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);
            if (_distanceToPlayer > 20)
            {
                _enemy.ChangeState(new RobotEnemyIdleState(_enemy));
            }
            _enemy._agent.destination = _enemy._player.position;
        }
    }

}
