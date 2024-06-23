using UnityEngine;
using UnityEngine.AI;

public class BuildCommand : Command
{
    public override bool _isComplete => BuildComplete();

    private NavMeshAgent _agent;
    private Builder _builder;

    public BuildCommand(NavMeshAgent agent, Builder builder)
    {
        this._agent = agent;
        this._builder = builder;
    }




    private bool BuildComplete()
    {
        if (_agent.remainingDistance > 0.1f)
            return false;

        if (_builder != null)
            _builder.Build();

        return true;
    }

    public override void Execute()
    {

        _agent.SetDestination(_builder.transform.position);
    }


}
