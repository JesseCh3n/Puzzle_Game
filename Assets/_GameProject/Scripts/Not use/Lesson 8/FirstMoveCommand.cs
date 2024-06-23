using UnityEngine;

public class FirstMoveCommand : Command
{
    public override bool _isComplete => ReachedDestination();

    private Transform _object;
    private Vector3 _destination;
    private float _speed;
    Vector3 _targetPosition;
    public FirstMoveCommand(Transform objectToMove, Vector3 destination, float speed)
    {
        this._object = objectToMove;
        this._destination = destination;
        this._speed = speed;
    }

    private bool ReachedDestination()
    {
        if (Vector3.Distance(_object.position, _destination) > 20f)
        {
            return false;
        }
        return true;
    }

    public override void Execute()
    {
        _targetPosition = _object.localPosition + new Vector3(0, 5f, 0);
        float step = _speed * Time.deltaTime;
        Debug.Log("Iam here");
        _object.position = Vector3.Lerp(_object.position, _targetPosition, step);
    }
}
