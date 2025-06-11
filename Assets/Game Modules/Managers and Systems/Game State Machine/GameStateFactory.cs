public class GameStateFactory : BaseStateFactory<GameState>
{
    public static GameState InGameState(BaseStateMachine<GameState> context)
    {
        return new InGameState(context, isRoot: true);
    } 
    public static GameState PreGameState(BaseStateMachine<GameState> context) => new PreGameState(context, isRoot: true);
    public static GameState PlayerControlledState(BaseStateMachine<GameState> context) => new ControllingCharacter(context, isRoot: false);
    public static GameState CutSceneState(BaseStateMachine<GameState> context) => new CutSceneState(context, isRoot: false);
    public static GameState SandboxState(BaseStateMachine<GameState> context) => new SandboxState(context, isRoot: false);
}