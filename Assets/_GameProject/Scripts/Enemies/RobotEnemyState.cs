public abstract class RobotEnemyState
{

    protected RobotEnemyController _enemy;

    public RobotEnemyState(RobotEnemyController enemy)
    {
        this._enemy = enemy;
    }


    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();


}
