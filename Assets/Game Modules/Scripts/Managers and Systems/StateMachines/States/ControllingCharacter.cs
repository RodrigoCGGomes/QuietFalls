using UnityEditor;
using UnityEngine;

public class ControllingCharacter : GameState
{
    public ControllingCharacter(BaseStateMachine<GameState> context, bool isRoot) : base(context, isRoot)
    {
        stateInfo = new StateInfo { stateName = "ControllingCharacter", stateDescription = "Player controlling character" };
    }

    public override void EnterState()
    {
        Debug.Log($"ControllingCharacter.EnterState(); - {this}, SuperState = {CurrentSuperState}");
    }

    public override void Tick() { }

    public override void ExitState() { }
}