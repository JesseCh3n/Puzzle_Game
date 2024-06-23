
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Timer _remainingTime;
    [SerializeField] private EscapeTrigger _escapeTrigger;
    [Header("UI Elements")]
    public TMP_Text _txtHealth;
    public TMP_Text _txtTimer;
    public TMP_Text _gameOverText;


    // Start is called before the first frame update
    private void OnEnable()
    {
        _playerHealth._onHealthUpdated += OnHealthUpdate;
        _playerHealth._onDeath += OnDeath;
        _remainingTime._onTimeUpdated += OnTimeUpdate;
        _remainingTime._onGameOver += OnDeath;
        _escapeTrigger.onGameRestart += OnRestart;
    }


    public void OnDeath()
    {
        _gameOverText.text = "Game Over!";
    }

    public void OnEnd()
    {
        _gameOverText.text = "You Win!";
    }

    private void OnDisable()
    {
        _playerHealth._onHealthUpdated -= OnHealthUpdate;
    }

    public void OnHealthUpdate(float health)
    {
        _txtHealth.text = "Health : " + Mathf.Floor(health);
    }

    public void OnTimeUpdate(float time)
    {
        _txtTimer.text = "Time Remain: " + Mathf.Floor(time);
    }

    public void OnRestart()
    {
        _gameOverText.text = "Press ESC to restart.";
    }

}
