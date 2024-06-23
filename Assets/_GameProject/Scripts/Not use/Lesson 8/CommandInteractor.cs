using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    Queue<Command> _commands = new Queue<Command>();

    [SerializeField] private Transform _lift;
    [SerializeField] private Transform _liftPoint1;
    [SerializeField] private Transform _liftPoint2;
    [SerializeField] private Transform _liftPoint3;
    public float _speed;

    [SerializeField] private Camera _cam;
    private Command _currentCommand;

    public override void Interact()
    {
        if (_input._commandPressed)//Press G
        {
            Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out var hitinfo))
            {
                if (hitinfo.transform.CompareTag("LiftButton") && Vector3.Distance(_lift.position, _liftPoint1.position) < 20f)
                {
                    Debug.Log("Lift is here");
                    _commands.Enqueue(new FirstMoveCommand(_lift, _liftPoint2.position, _speed));
                }
                else if (hitinfo.transform.CompareTag("LiftButton") && Vector3.Distance(_lift.position, _liftPoint2.position) < 1f)
                {
                    Debug.Log(Vector3.Distance(_lift.position, _liftPoint2.position));
                    _commands.Enqueue(new SecondMoveCommand(_lift, _liftPoint3.position, _speed));
                }
                else if (hitinfo.transform.CompareTag("LiftButton") && Vector3.Distance(_lift.position, _liftPoint3.position) < 1f)
                {
                    Debug.Log(Vector3.Distance(_lift.position, _liftPoint3.position));
                    _commands.Enqueue(new ThirdMoveCommand(_lift, _liftPoint1.position, _speed));
                }
            }
        }
        ProcessCommands();
    }


    void ProcessCommands()
    {
        if (_currentCommand != null && !_currentCommand._isComplete)
        {
            return;
        }
        if (_commands.Count == 0)
        {
            return;
        }
        _currentCommand = _commands.Dequeue();
        Debug.Log(_currentCommand);
        _currentCommand.Execute();
    }

}
