using UnityEngine;

public class CutSceneState : GameState
{
    public CutSceneState(bool isRoot) : base(isRoot)
    {
        stateName = "Cut Scene";
    }

    public override void EnterState()
    {
        Debug.Log($"CutSceneState.EnterState(); - {this}, SuperState = {CurrentSuperState}");
    }

    public override void Tick() { }

    public override void ExitState() { }
}