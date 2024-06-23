using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] private LevelManager[] _levels;
    private LevelManager _currentLevel;
    private int _currentLevelIndex = 0;
    private bool _isInputActive = true;

    public UnityEvent _onGameOver;
    public UnityEvent _onGameEnd;

    public bool IsInputActive()
    {
        return _isInputActive;
    }

    private  static GameManager _instance;
    public static GameManager GetInstance()
    {
        return _instance;
    }


    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }
    private GameState _currentState;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (_levels.Length > 0)
        {
            ChangeState(GameState.Briefing, _levels[_currentLevelIndex]);
            Debug.Log("briefiiiing...........");
        }
    }
    private void StartBriefing()
    {
        Debug.Log("Briefing Started..... ");
        _isInputActive = false;

        ChangeState(GameState.LevelStart, _currentLevel);
    }
    private void InitiateLevel()
    {
        Debug.Log("Level Start");
        _isInputActive = true;
        _currentLevel.StartLevel();
        ChangeState(GameState.LevelIn, _currentLevel);
    }
    private void RunLevel()
    {
        Debug.Log("LevelIn");
    }
    private void CompleteLevel()
    {
        Debug.Log("Level End " + _levels[_currentLevelIndex].gameObject.name);
        ChangeState(GameState.LevelStart, _levels[++_currentLevelIndex]);
    }
    private void GameOver()
    {
        _onGameOver?.Invoke();
        Debug.Log("Game Over, You Lose!");
    }
    private void GameEnd()
    {
        _onGameEnd?.Invoke();
        Debug.Log("Game End");
    }

    public void ChangeState(GameState state, LevelManager level)
    {
        _currentState = state;
        _currentLevel = level;

        switch (_currentState)
        {
            case GameState.Briefing:
                StartBriefing();
                break;
            case GameState.LevelStart:
                InitiateLevel();
                break;
            case GameState.LevelIn:
                RunLevel();
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
        }
    }
}
