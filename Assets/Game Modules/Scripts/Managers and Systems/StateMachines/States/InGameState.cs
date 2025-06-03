using UnityEngine;

public class InGameState : GameState
{
    public InGameState(BaseStateMachine<GameState> context, bool isRoot = false) : base(context, isRoot)
    {
        stateInfo = new StateInfo { stateName = "InGame", stateDescription = "Main gameplay state" };
    }

    public override void EnterState()
    {
        Debug.Log($"InGameState.EnterState(); - {this}");
        InitializeSubState();
    }

    public override void Tick() { }

    public override void ExitState() { }

}