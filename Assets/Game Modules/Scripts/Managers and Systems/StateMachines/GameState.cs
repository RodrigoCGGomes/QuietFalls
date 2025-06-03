using System;

public abstract class GameState : BaseState
{
    protected GameState(BaseStateMachine<GameState> context, bool isRoot) : base(context, isRoot) { }

    protected override object GetFactory(object context)
    {
        if (context is GameStateMachine gameContext)
            return gameContext.factory;
        throw new InvalidOperationException("Context must be GameStateMachine for GameState.");
    }
}