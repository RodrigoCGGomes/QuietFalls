using UnityEngine;

public class CameraStateMachine
{
    public CameraState currentState { get; private set; }

    public void Initialize(CameraState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }
    public void ChangeState(CameraState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }
}