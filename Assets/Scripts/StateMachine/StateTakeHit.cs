using UnityEngine;

public class StateTakeHit : BaseState
{
    public StateTakeHit(StateMachine stateMachine, EnemyActions enemyActions, VitalitySystem vitalitySystem)
        : base(stateMachine, enemyActions, vitalitySystem) { }

    private float _timer;
    private float _endOfAnimTime = 0.3f;

    public override void UpdateState()
    {
        _enemyActions.Animator.SetTrigger(_enemyActions.HurtAnim);
        _timer += Time.deltaTime;

        if (_timer >= _endOfAnimTime)
        {
            if (_enemyActions.CanAttack)
            {
                _stateMachine.SwitchState(_enemyActions.StateAttack);
            }
            else
            {
                _stateMachine.SwitchState(_enemyActions.StateMove);
            }
        }
    }
}
