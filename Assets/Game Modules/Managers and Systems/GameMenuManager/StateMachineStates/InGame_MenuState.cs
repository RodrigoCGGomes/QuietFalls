using UnityEngine;

public class InGameMenus : GameState
{
    public InGameMenus(bool isRoot) : base(isRoot)
    {
        GameDebugger.WriteVariable("menu-state-machine", "InGameMenus");
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
        throw new System.NotImplementedException();
    }
}
