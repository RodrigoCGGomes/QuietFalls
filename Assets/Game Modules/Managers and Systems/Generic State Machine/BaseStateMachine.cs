using System;
using UnityEngine;

public class BaseStateMachine<T> where T : BaseState<T>
{
    public T CurrentState { get; private set; }
    public BaseStateFactory<T> factory { get; set; }
    public StateMachineInfo stateMachineInfo;

    public event Action<StateMachineInfo> OnStateEntered;

    public BaseStateMachine(T _initialState, BaseStateFactory<T> _factory, BaseStateMachineType _type)
    {
        factory = _factory;
        if (_initialState != null)
        {
            ChangeState(_initialState);
        }
        else
        { 
            Debug.LogWarning($"BaseStateMachine: Initial state is null, cannot change state.");
        }
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
        OnStateEntered?.Invoke(stateMachineInfo);

        GameDebugger.WriteVariable($"sm_state_game", $"{newState.stateName}");
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