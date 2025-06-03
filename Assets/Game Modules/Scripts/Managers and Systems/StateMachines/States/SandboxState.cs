using UnityEngine;

public class SandboxState : GameState
{
    //Constructor
    public SandboxState(BaseStateMachine<GameState> currentContext, bool isRoot) : base(currentContext, isRoot)
    {

    }

    public override void EnterState()
    {
        Debug.LogWarning("Entered State : SandboxState, Sub State");
    }

    public override void Tick() { }

    public override void ExitState() { }

}