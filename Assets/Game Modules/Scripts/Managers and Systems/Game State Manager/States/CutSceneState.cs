using UnityEngine;

public class CutSceneState : GameBaseState
{
    public CutSceneState(GameStateMachine context, bool isRoot) : base(context, isRoot)
    {

    }

    public override void EnterState()
    {
        Debug.Log($"GameBaseState.EnterState(); - {this}, SuperState = {base._currentSuperState}");
        GamePlayerManager.GetRegisteredPlayer().GetStateMachine().ChangeState(new DialogueBlock(GamePlayerManager.GetRegisteredPlayer()));
        GamePlayerManager.GetRegisteredCamera().stateMachine.ChangeState(new DialogueCamera(GamePlayerManager.GetRegisteredPlayer()));
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
}