using UnityEngine;

public class SandboxState : GameState
{
    //Constructor
    public SandboxState(bool isRoot) : base(isRoot)
    {
        stateName = "Sandbox State";
    }

    public override void EnterState()
    {
        Debug.LogWarning("Entered State : SandboxState, Sub State");
    }

    public override void Tick() { }

    public override void ExitState() { }

}