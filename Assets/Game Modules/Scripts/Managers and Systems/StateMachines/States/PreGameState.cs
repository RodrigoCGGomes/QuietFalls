using UnityEngine;

public class PreGameState : GameState
{
    //Constructor
    public PreGameState(BaseStateMachine<GameState> currentContext, bool isRoot) : base(currentContext, isRoot)
    {

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