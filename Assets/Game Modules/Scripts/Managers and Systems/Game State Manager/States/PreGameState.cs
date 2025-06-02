using UnityEngine;

public class PreGameState : GameBaseState
{
    //Constructor
    public PreGameState(GameStateMachine currentContext, bool isRoot) : base(currentContext, isRoot)
    {

    }

    public override void EnterState() 
    {
        Debug.LogWarning("Entered State : PreGameState, Root State");
        GameStateManager.ChangeState(new SandboxState(GameStateMachine.GetContext(), false));
    }

    public override void UpdateState() { }

    public override void ExitState() { }

}