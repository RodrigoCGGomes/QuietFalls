using UnityEngine;

public class InGameState : GameBaseState
{
    public InGameState(GameStateMachine currentContext, bool isRoot = false) : base(currentContext, isRoot)
    { 
    
    }

    public override void EnterState() 
    {
        Debug.Log($"GameBaseState.EnterState(); - {this}");
        InitializeSubState();
    }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void InitializeSubState()
    {
        SetAndEnterSubState(_factory.PlayerControlledState());
    }


}