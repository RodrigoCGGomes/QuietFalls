using UnityEngine;

public class CutSceneState : GameBaseState
{
    public CutSceneState(GameStateMachine context, GameStateFactory factory, bool isRoot) : base(context, factory, isRoot)
    {

    }

    public override void EnterState()
    {
        Debug.LogWarning("Entered State : CutSceneState, Sub State");
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