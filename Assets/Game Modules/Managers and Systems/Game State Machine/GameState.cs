public abstract class GameState : BaseState<GameState>
{
    protected GameState(BaseStateMachine<GameState> context, bool isRoot) : base(context, isRoot) { }

    protected override object GetFactory(BaseStateMachine<GameState> context)
    {
        return context.factory; // No need to cast since context is already the right type
    }

    public abstract override void EnterState();
    public abstract override void Tick();
    public abstract override void ExitState();
}