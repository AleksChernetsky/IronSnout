using UnityEngine;

public class StateCheckDirection : BaseState
{
    public StateCheckDirection(StateMachine stateMachine, EnemyActions enemyActions, VitalitySystem vitalitySystem)
        : base(stateMachine, enemyActions, vitalitySystem) { }

    public override void UpdateState()
    {
        Vector2 direction = _enemyActions.transform.position - _enemyActions.Target.position;
        if (direction.x > 0)
        {
            _enemyActions.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            _enemyActions.transform.rotation = Quaternion.identity;
        }
        _stateMachine.SwitchState(_enemyActions.StateMove);
    }
}
