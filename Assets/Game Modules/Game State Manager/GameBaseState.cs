
/// <summary>
/// All Game States inherit from GameBaseState. And these states are managed by GameStateMachine.
/// </summary>
public abstract class GameBaseState
{
    private bool _isRootState = false;
    private GameStateMachine _ctx;
    protected GameStateFactory _factory;
    private GameBaseState _currentSubState;
    private GameBaseState _currentSuperState;

    //Constructor
    public GameBaseState(GameStateMachine currentContext, GameStateFactory gameStateFactory, bool isRoot)
    {
        _ctx = currentContext;
        _factory = gameStateFactory;
        _isRootState = isRoot;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public virtual void InitializeSubState() { }


    protected void SetAndEnterSubState(GameBaseState newSubState)
    {
        SetSubState(newSubState);      // sets hierarchy
        newSubState.EnterState();      // auto-enters
    }
    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            {
                _currentSubState.UpdateStates();
            }
        }
    }
    public void SwitchStates(GameBaseState newState) 
    {
        ExitState();            // First, we trigger the exit logic of the current state.
        newState.EnterState();  // Then, we trigger the enter logic of the current state.

        if (_isRootState)
        {
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(GameBaseState newSuperstate) 
    {
        _currentSuperState = newSuperstate;
    }
    protected void SetSubState(GameBaseState newSubState)
    { 
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}