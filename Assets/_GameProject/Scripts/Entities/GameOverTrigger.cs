using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Timer _timer;
    private bool _playerDie = false;
    private bool _timeExpired = false;

    public UnityEvent _isOver;
    // Start is called before the first frame update

    private void OnEnable()
    {
        _playerHealth._onDeath += PlayerDie;
        _timer._onGameOver += TimeExpired;
        Debug.Log(_timer._onGameOver);
    }

    public void PlayerDie()
    {
        _playerDie = true;
    }

    public void TimeExpired()
    {
        _timeExpired = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerDie || _timeExpired)
        {
            _isOver?.Invoke();
        }
    }
}
