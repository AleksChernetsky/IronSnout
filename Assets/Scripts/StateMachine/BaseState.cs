public class BaseState
{
    protected StateMachine _stateMachine;
    protected EnemyActions _enemyActions;
    protected VitalitySystem _vitalitySystem;

    public BaseState(StateMachine stateMachine, EnemyActions enemyActions, VitalitySystem vitalitySystem)
    {
        _stateMachine = stateMachine;
        _enemyActions = enemyActions;
        _vitalitySystem = vitalitySystem;
    }

    public virtual void UpdateState() { }
}
