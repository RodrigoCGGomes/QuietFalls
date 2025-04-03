
public class GameStateFactory
{
    GameStateMachine _context;

    public GameStateFactory (GameStateMachine currentContext)
    {
        _context = currentContext;
    }

    public InGameState InGameState()
    { 
        return new InGameState(_context, this, isRoot: true);
    }

    public PreGameState PreGameState()
    {
        return new PreGameState(_context, this, isRoot : true);
    }

    public ControllingCharacter PlayerControlledState()
    {
        return new ControllingCharacter(_context, this, isRoot : false);
    }

    public CutSceneState CutSceneState()
    {
        return new CutSceneState(_context, this, isRoot : false);
    }
}
