using UnityEngine;

public class InGameState : GameBaseState
{
    public InGameState(GameStateMachine currentContext, GameStateFactory gameStateFactory, bool isRoot = false) : base(currentContext, gameStateFactory, isRoot)
    { 
    
    }

    public override void EnterState() 
    {
        Debug.LogWarning("Entered State : InGameState, Root State");
        InitializeSubState();
    }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void InitializeSubState()
    {
        SetAndEnterSubState(_factory.PlayerControlledState());
    }


}