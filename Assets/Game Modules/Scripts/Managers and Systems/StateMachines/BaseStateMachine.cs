using UnityEngine;

public class BaseStateMachine<T> where T : BaseState
{
    public T currentState { get; set; }
    public BaseStateFactory<T> factory { get; set; }

    public BaseStateMachine(T initialState, BaseStateFactory<T> _factory)
    {
        factory = _factory;
        ChangeState(initialState);
    }

    public void ChangeState(T newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }

    public T GetRootState()
    {
        return currentState;
    }


}
