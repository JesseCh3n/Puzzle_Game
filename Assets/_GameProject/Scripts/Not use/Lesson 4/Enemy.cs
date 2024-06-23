using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolingPoint;
    [SerializeField] private Transform _enemyEye;
    [SerializeField] private float _playerCheckDistance;
    [SerializeField] private float _checkRadius;

    private Transform _player;
    private NavMeshAgent _agent;
    public bool _isIdle = true;
    public bool _isCloseToPlayer;
    public bool _isPlayerFound;

    private int _currentPatrolingTarget = 0;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _patrolingPoint[_currentPatrolingTarget].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isIdle)//10
        {
            //call idle function
            Idle();
        }
        else if (_isPlayerFound)
        {
            if (_isCloseToPlayer)//Less than 2
            {
                //attack
                AttackPlayer();
            }
            else//Greater than 2
            {
                //follow player
                FollowPlayer();
            }


        }

     //   Debug.Log(Vector3.Distance(transform.position, _player.position));
    }

    private void Idle()
    {
        if (_agent.remainingDistance < 0.1f)
        {
            _currentPatrolingTarget++;
            if (_currentPatrolingTarget >= _patrolingPoint.Length)
            {
                _currentPatrolingTarget = 0;
            }
            _agent.destination = _patrolingPoint[_currentPatrolingTarget].position;
            // Debug.Log("Current target " + _patrolingPoint[_currentPatrolingTarget]);
        }
        if (Physics.SphereCast(_enemyEye.position, _checkRadius, transform.forward, out RaycastHit hit, _playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                _isIdle = false;
                _isPlayerFound = true;
                _player = hit.transform;
                _agent.destination = _player.position;
                Debug.Log("Player Found: ");
            }

        }

    }
    private void FollowPlayer()
    {
        if (_player != null)
        {
            if (Vector3.Distance(transform.position, _player.position) > 10)
            {
                _isPlayerFound = false;
                _isIdle = true;

            }
            if (Vector3.Distance(transform.position, _player.position) < 2)
            {
                _isCloseToPlayer = true;
            }
            else
            {
                _isCloseToPlayer = false;
            }
            _agent.destination = _player.position;
        }
        else
        {
            _isPlayerFound = false;
            _isIdle = true;
            _isCloseToPlayer = false;
        }
    }
    private void AttackPlayer()
    {
        Debug.Log("Attacking Player");
        if (Vector3.Distance(transform.position, _player.position) > 2)
        {
            _isCloseToPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_enemyEye.position, _checkRadius);
        Gizmos.DrawWireSphere(_enemyEye.position + _enemyEye.forward * _playerCheckDistance, _checkRadius);
        Gizmos.DrawLine(_enemyEye.position, _enemyEye.position + _enemyEye.forward * _playerCheckDistance);
    }

}
