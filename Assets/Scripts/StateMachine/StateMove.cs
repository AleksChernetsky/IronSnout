using UnityEngine;

public class StateMove : BaseState
{
    public StateMove(StateMachine stateMachine, EnemyActions enemyActions, VitalitySystem vitalitySystem)
        : base(stateMachine, enemyActions, vitalitySystem) { }

    public override void UpdateState()
    {
        Vector3 direction = (_enemyActions.Target.position - _enemyActions.transform.position).normalized;
        if (!_enemyActions.CanAttack)
        {
            if (!_enemyActions.FastEnemy)
            {
                _enemyActions.Animator.SetBool(_enemyActions.WalkAnim, true);
                _enemyActions.transform.position += direction * _enemyActions.MovementSpeed * Time.deltaTime;
            }
            else
            {
                _enemyActions.Animator.SetBool(_enemyActions.RunAnim, true);
                _enemyActions.transform.position += direction * _enemyActions.MovementSpeed * 2 * Time.deltaTime;
            }
        }
        else
        {
            _enemyActions.Animator.SetBool(_enemyActions.WalkAnim, false);
            _enemyActions.Animator.SetBool(_enemyActions.RunAnim, false);
            _stateMachine.SwitchState(_enemyActions.StateAttack);
        }
    }
}
