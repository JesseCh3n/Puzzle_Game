using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{

    public UnityEvent _onLevelStart;
    public UnityEvent _onLevelEnd;

    [SerializeField] private bool _isFinalLevel;

    public void StartLevel()
    {
        _onLevelStart?.Invoke();
    }

    public void GameOver()
    {
        Debug.Log("Hi, I am here");
        GameManager.GetInstance().ChangeState(GameManager.GameState.GameOver, this);
    }

    public void EndLevel()
    {
        _onLevelEnd?.Invoke();
        if (_isFinalLevel)
        {
            //TODO: Game Ended
            GameManager.GetInstance().ChangeState(GameManager.GameState.GameEnd, this);
        }
        else
        {
            //TODO: Level Ended
            GameManager.GetInstance().ChangeState(GameManager.GameState.LevelEnd, this);
        }
    }


}
