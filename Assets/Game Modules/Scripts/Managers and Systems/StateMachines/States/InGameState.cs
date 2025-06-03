using UnityEngine;

public class InGameState : GameState
{
    public InGameState(BaseStateMachine<GameState> context, bool isRoot = false) : base(context, isRoot)
    {
        
    }

    public override void EnterState()
    {
        Debug.Log($"InGameState.EnterState(); - {this}");
        InitializeSubState();
    }

    public override void Tick() { }

    public override void ExitState() { }
}

