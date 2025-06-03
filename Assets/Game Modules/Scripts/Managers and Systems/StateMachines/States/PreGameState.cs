using UnityEngine;

public class PreGameState : GameState
{
    public PreGameState(BaseStateMachine<GameState> context, bool isRoot) : base(context, isRoot) { }

    public override void EnterState()
    {
        Debug.LogWarning("EnterState() - PreGameState : GameState");
    }

    public override void Tick() { }

    public override void ExitState()
    {
        Debug.LogWarning("Exit() - PreGameState : GameState");
    }
}