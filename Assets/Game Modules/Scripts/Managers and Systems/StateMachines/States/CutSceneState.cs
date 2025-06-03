using UnityEngine;

public class CutSceneState : GameState
{
    public CutSceneState(BaseStateMachine<GameState> context, bool isRoot) : base(context, isRoot)
    {
        
    }

    public override void EnterState()
    {
        Debug.Log($"CutSceneState.EnterState(); - {this}, SuperState = {CurrentSuperState}");
    }

    public override void Tick() { }

    public override void ExitState() { }
}