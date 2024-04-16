using UnityEngine;

public class StateAttack : BaseState
{
    public StateAttack(StateMachine stateMachine, EnemyActions enemyActions, VitalitySystem vitalitySystem)
        : base(stateMachine, enemyActions, vitalitySystem) { }

    private float _coolDown = 1;

    public override void UpdateState()
    {
        _coolDown += Time.deltaTime;
        if (_enemyActions.CanAttack)
        {
            if (_coolDown >= _enemyActions.AttackSpeed)
            {
                _enemyActions.Animator.SetTrigger(_enemyActions.AttackAnim);                
                _coolDown = 0;
            }
        }
        else
        {
            _stateMachine.SwitchState(_enemyActions.StateMove);
        }
    }
}
