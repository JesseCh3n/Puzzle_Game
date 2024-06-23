using UnityEngine;

public class SecondMoveCommand : Command
{
    public override bool _isComplete => ReachedDestination();

    private Transform _object;
    private Vector3 _destination;
    private float _speed;
    public SecondMoveCommand(Transform objectToMove, Vector3 destination, float speed)
    {
        this._object = objectToMove;
        this._destination = destination;
        this._speed = speed;
    }

    private bool ReachedDestination()
    {
        if (Vector3.Distance(_object.position, _destination) > 1f)
        {
            return false;
        }
        return true;
    }

    public override void Execute()
    {
        float step = _speed * Time.deltaTime;
        _object.position = Vector3.MoveTowards(_object.position, _destination, step);
    }
}
