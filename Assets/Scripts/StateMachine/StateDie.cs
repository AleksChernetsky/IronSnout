using UnityEngine;

public class StateDie : BaseState
{
    public StateDie(StateMachine stateMachine, EnemyActions enemyActions, VitalitySystem vitalitySystem)
        : base(stateMachine, enemyActions, vitalitySystem) { }

    private float _timer;
    private float _endOfAnimTime = 0.35f;

    public override void UpdateState()
    {
        _enemyActions.Animator.SetTrigger(_enemyActions.DeadAnim);
        _timer += Time.deltaTime;

        if (_timer >= _endOfAnimTime)
        {
            _enemyActions.gameObject.SetActive(false);
            _enemyActions.ResetCharacter();
            _stateMachine.SwitchState(_enemyActions.StateCheckDirection);
        }
    }
}
