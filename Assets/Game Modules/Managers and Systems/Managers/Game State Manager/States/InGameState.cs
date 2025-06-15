using UnityEngine;

public class InGameState : GameState
{
    public InGameState(bool isRoot = false) : base(isRoot)
    {
        stateName = "In Game State";
    }

    public override void EnterState()
    {
        Debug.Log($"InGameState.EnterState(); - {this}");
        InitializeSubState();
    }

    public override void Tick() { }

    public override void ExitState() { }
}