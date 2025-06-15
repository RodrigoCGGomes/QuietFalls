using UnityEngine;

public class PreGameState : GameState
{
    public PreGameState(bool isRoot) : base(isRoot) 
    {
        stateName = "Pre Game State";
    }

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