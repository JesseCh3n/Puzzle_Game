using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{


    private int _currentPatrolingTarget = 0;
    public EnemyIdleState(EnemyController enemy) : base(enemy) { }

    public override void OnStateEnter()
    {
        _enemy._agent.destination = _enemy._patrolingPoint[_currentPatrolingTarget].position;
        //Debug.Log("Enemy is now Idling");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy exiting idling state");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._agent.remainingDistance < 0.1f)
        {
            _currentPatrolingTarget++;
            if (_currentPatrolingTarget >= _enemy._patrolingPoint.Length)
            {
                _currentPatrolingTarget = 0;
            }

            _enemy._agent.destination = _enemy._patrolingPoint[_currentPatrolingTarget].position;
        }
        if (Physics.SphereCast(_enemy._enemyEye.position, _enemy._checkRadius, _enemy.transform.forward, out RaycastHit hit, _enemy._playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                _enemy._player = hit.transform;
                _enemy._agent.destination = _enemy._player.position;
            }
            _enemy.ChangeState(new EnemyFollowState(_enemy));
        }

    }


}
