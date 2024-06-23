using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public float _maxHealth;

    public Action<float> _onHealthUpdated;
    public Action _onDeath;

    public bool _isDead { get; private set; }

    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;
        if (_onHealthUpdated != null)
        {
            _onHealthUpdated(_maxHealth);
        }
    }

    public void DeductHealth(float value)
    {
        if (_isDead) return;
        //Debug.Log(value);
        //Debug.Log(_health);
        _health -= value;

        if (_health <= 0)
        {
            _isDead = true;
            _onDeath();
            _health = 0;
        }
        _onHealthUpdated(_health);
    }

}
