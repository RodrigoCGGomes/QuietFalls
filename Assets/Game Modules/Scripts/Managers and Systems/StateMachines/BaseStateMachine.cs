using UnityEngine;

public class BaseStateMachine<T> where T : BaseState<T>
{
    public T CurrentState { get; private set; }
    public BaseStateFactory<T> factory { get; set; }
    public StateMachineInfo stateMachineInfo;
    

    public BaseStateMachine(T initialState, BaseStateFactory<T> _factory)
    {
        factory = _factory;
        ChangeState(initialState);
        stateMachineInfo = new StateMachineInfo();
    }

    public void ChangeState(T newState)
    {
        Debug.LogWarning($"Some StateMachine went ChangeState : {newState}");
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }
        CurrentState = newState;
        CurrentState?.EnterState();
    }

    public T GetRootState()
    {
        return CurrentState;
    }
    
    public BaseStateFactory<T> GetFactory()
    {
        return factory;
    }
}