
public class GameStateFactory
{
    GameStateMachine _context;

    public GameStateFactory (GameStateMachine currentContext)
    {
        _context = currentContext;
    }

    public InGameState InGameState()
    { 
        return new InGameState(_context, isRoot: true);
    }

    public PreGameState PreGameState()
    {
        return new PreGameState(_context, isRoot : true);
    }

    public ControllingCharacter PlayerControlledState()
    {
        return new ControllingCharacter(_context, isRoot : false);
    }

    public CutSceneState CutSceneState()
    {
        return new CutSceneState(_context, isRoot : false);
    }

    public SandboxState SandboxState()
    {
        return new SandboxState(_context, isRoot: false);
    }
}
