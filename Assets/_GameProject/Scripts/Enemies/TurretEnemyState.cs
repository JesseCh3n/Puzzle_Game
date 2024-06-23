public abstract class TurretEnemyState
{

    protected TurretEnemyController _enemy;

    public TurretEnemyState(TurretEnemyController enemy)
    {
        this._enemy = enemy;
    }


    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();


}
