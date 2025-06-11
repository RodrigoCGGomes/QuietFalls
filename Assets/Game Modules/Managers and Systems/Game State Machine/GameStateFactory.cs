public class GameStateFactory : BaseStateFactory<GameState>
{
    public GameState InGameState(BaseStateMachine<GameState> context) => new InGameState(context, isRoot: true);
    public GameState PreGameState(BaseStateMachine<GameState> context) => new PreGameState(context, isRoot: true);
    public GameState PlayerControlledState(BaseStateMachine<GameState> context) => new ControllingCharacter(context, isRoot: false);
    public GameState CutSceneState(BaseStateMachine<GameState> context) => new CutSceneState(context, isRoot: false);
    public GameState SandboxState(BaseStateMachine<GameState> context) => new SandboxState(context, isRoot: false);
}