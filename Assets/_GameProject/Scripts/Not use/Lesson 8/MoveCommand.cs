using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    public override bool _isComplete => ReachedDestination();

    private NavMeshAgent _agent;
    private Vector3 _destination;
    public MoveCommand(NavMeshAgent agent,Vector3 destination)
    {
        this._agent = agent;
        this._destination = destination;
    }


    private bool ReachedDestination()
    {
        if (_agent.remainingDistance > 0.1f)
        {
            return false;
        }
        return true;

    }

    public override void Execute()
    {
        _agent.SetDestination(_destination);
    }
}
