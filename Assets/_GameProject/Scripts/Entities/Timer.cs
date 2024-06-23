using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] public float _maxTime;

    public Action<float> _onTimeUpdated;
    public Action _onGameOver;

    public bool _isDead { get; private set; }

    public float _time;

    // Start is called before the first frame update
    void Start()
    {
        _time = _maxTime;
        _onTimeUpdated(_maxTime);
    }

    void Update()
    {
        if (_isDead) return;

        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _isDead = true;
            _onGameOver();
            _time = 0;
        }
        _onTimeUpdated(_time);
    }

}
