using UnityEngine;

public class SandboxState : GameBaseState
{
    //Constructor
    public SandboxState(GameStateMachine currentContext, bool isRoot) : base(currentContext, isRoot)
    {

    }

    public override void EnterState()
    {
        Debug.LogWarning("Entered State : SandboxState, Sub State");
    }

    public override void UpdateState() { }

    public override void ExitState() { }

}