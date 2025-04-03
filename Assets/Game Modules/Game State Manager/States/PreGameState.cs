using UnityEngine;

public class PreGameState : GameBaseState
{
    //Constructor
    public PreGameState(GameStateMachine currentContext, GameStateFactory gameStateFactory, bool isRoot) : base(currentContext, gameStateFactory, isRoot)
    {

    }

    public override void EnterState() 
    {
        Debug.LogWarning("Entered State : PreGameState, Root State");
    }

    public override void UpdateState() { }

    public override void ExitState() { }

}