using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEndTrigger : MonoBehaviour
{
    private bool _enemyOneDie = false;
    private bool _enemyTwoDie;

    [SerializeField] private LevelManager _endingLevel;
    // Start is called before the first frame update

    public void RobotDie()
    {
        _enemyOneDie = true;
    }

    public void TurretDie()
    {
        _enemyTwoDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyOneDie && _enemyTwoDie)
        {
            _endingLevel.EndLevel();
        }
    }
}
