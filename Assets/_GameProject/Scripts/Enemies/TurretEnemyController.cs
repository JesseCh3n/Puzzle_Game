using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TurretEnemyController : MonoBehaviour
{
    public Transform _enemyEye;
    public float _playerCheckDistance;
    public float _checkRadius = 0.4f;
    public Transform _enemy;
    public Transform[] _patrolingPoint;
    public NavMeshAgent _agent;
    [HideInInspector] public Transform _player;
    public TurretEnemyState _currentState;
    public Transform _shootPosition;
    public ObjectPool _bulletPool;
    public float _bulletVelocity;
    public float _shootingRate;

    public UnityEvent _turretDie;

    private int _damageCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = new TurretEnemyIdleState(this);
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


    public void ChangeState(TurretEnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    public void Die()
    {
        _damageCounter = 0;
        _turretDie?.Invoke();
        this.gameObject.SetActive(false);
    }
}
