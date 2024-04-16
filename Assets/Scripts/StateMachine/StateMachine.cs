public class StateMachine
{
    public BaseState CurrentState { get; set; }

    public void Initialize(BaseState defaultState)
    {
        CurrentState = defaultState;
        CurrentState.UpdateState();
    }
    public void SwitchState(BaseState newState)
    {
        CurrentState = newState;
    }
}
