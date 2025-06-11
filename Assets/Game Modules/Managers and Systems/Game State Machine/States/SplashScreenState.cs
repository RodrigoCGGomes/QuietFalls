using UnityEngine;

public class SplashScreenState : GameState
{
    public SplashScreenState(BaseStateMachine<GameState> context, bool isRoot) : base(context, isRoot)
    {
        stateName = "Splash Screen State";
    }

    public override void EnterState()
    {
        Debug.LogWarning("EnterState() - PreGameState : GameState");
    }

    public override void Tick() { }

    public override void ExitState()
    {
        Debug.LogWarning("Exit() - PreGameState : GameState");
    }
}