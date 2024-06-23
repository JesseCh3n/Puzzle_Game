using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RobotEnemyController : MonoBehaviour
{
    public Transform[] _patrolingPoint;
    public Transform _enemyEye;
    public float _playerCheckDistance;
    public float _checkRadius = 0.4f;
    public NavMeshAgent _agent;
    [HideInInspector] public Transform _player;
    public RobotEnemyState _currentState;
    public Transform _laser;

    public UnityEvent _robotDie;

    private int _damageCounter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _laser.gameObject.SetActive(false);
        _currentState = new RobotEnemyIdleState(this);
        _currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnStateUpdate();
        if (_damageCounter == 10)
        {
            Die();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log(_damageCounter);
            _damageCounter += 1;
        }
    }

    public void ChangeState(RobotEnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    public void Die()
    {
        _damageCounter = 0;
        _robotDie?.Invoke();
        this.gameObject.SetActive(false);
    }

}
