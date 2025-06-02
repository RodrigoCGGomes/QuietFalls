
/// <summary>
/// All Game States inherit from GameBaseState. And these states are managed by GameStateMachine.
/// </summary>
public abstract class GameBaseState
{
    struct StateInfo
    {
        public string stateName;
        public string stateDescription;
    }
    private StateInfo stateInfo;

    private bool _isRootState = false;
    private GameStateMachine _ctx;
    protected GameStateFactory _factory;
    public GameBaseState _currentSubState;
    public GameBaseState _currentSuperState;

    //Constructor
    public GameBaseState(GameStateMachine currentContext, bool isRoot)
    {
        _ctx = currentContext;
        _factory = currentContext.factory;
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
        ExitState();
        if (_isRootState)
        {
            newState.EnterState();
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetAndEnterSubState(newState);
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