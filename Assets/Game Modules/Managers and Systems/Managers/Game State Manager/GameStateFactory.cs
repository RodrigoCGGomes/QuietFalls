public class GameStateFactory : BaseStateFactory<GameState>
{
    public static GameState InGameState(BaseStateMachine<GameState> context)
    {
        return new InGameState(isRoot: true);
    } 
    public static GameState PreGameState(BaseStateMachine<GameState> context) => new PreGameState(isRoot: true);
    public static GameState PlayerControlledState(BaseStateMachine<GameState> context) => new ControllingCharacter(isRoot: false);
    public static GameState CutSceneState(BaseStateMachine<GameState> context) => new CutSceneState(isRoot: false);
    public static GameState SandboxState(BaseStateMachine<GameState> context) => new SandboxState(isRoot: false);
}