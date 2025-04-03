using UnityEngine;

public class ControllingCharacter : GameBaseState
{
    public ControllingCharacter(GameStateMachine context, GameStateFactory factory, bool isRoot) : base(context, factory, isRoot)
    { 
    
    }

    public override void EnterState()
    {
        Debug.LogWarning("Entered State : ControllingCharacter, Sub State");
    }

    public override void UpdateState()
    { 
    
    }

    public override void ExitState()
    {

    }
}
