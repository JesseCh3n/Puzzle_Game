using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeTrigger : Interactor
{
    public Action onGameRestart;
    private String _currentSceneName;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        onGameRestart();
        _currentSceneName = SceneManager.GetActiveScene().name;

    }

    public override void Interact()
    {
        if (_input._escapePressed)
        {
            SceneManager.LoadScene(_currentSceneName);
        }
    }

}
