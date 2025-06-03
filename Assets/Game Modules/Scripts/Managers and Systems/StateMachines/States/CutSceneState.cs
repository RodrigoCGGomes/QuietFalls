using UnityEditor;
using UnityEngine;


public class CutSceneState : GameState
{
    public CutSceneState(BaseStateMachine<GameState> context, bool isRoot) : base(context, isRoot)
    {
        stateInfo = new StateInfo { stateName = "CutScene", stateDescription = "Cutscene playing" };
    }

    public override void EnterState()
    {
        Debug.Log($"CutSceneState.EnterState(); - {this}, SuperState = {CurrentSuperState}");
        // Temporarily commented out to avoid errors until PlayerStateMachine and CameraStateMachine are updated
        // GamePlayerManager.GetRegisteredPlayer().ChangeState(new DialogueBlock(GamePlayerManager.GetRegisteredPlayer()));
        // GamePlayerManager.GetRegisteredCamera().ChangeState(new DialogueCamera(GamePlayerManager.GetRegisteredCamera()));
    }

    public override void Tick() { }

    public override void ExitState() { }
}