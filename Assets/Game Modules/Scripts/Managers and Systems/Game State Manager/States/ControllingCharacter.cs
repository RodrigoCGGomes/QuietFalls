using UnityEngine;

public class ControllingCharacter : GameBaseState
{
    public ControllingCharacter(GameStateMachine context, bool isRoot) : base(context, isRoot)
    { 
    
    }

    public override void EnterState()
    {
        Debug.Log($"GameBaseState.EnterState(); - {this}, SuperState = {base._currentSuperState}");
    }

    public override void UpdateState()
    { 
    
    }

    public override void ExitState()
    {

    }
}
