using UnityEditorInternal;

/// <summary>
/// This state machine is used to track if the game has started or are in the main menu.
/// Then within the In-GameState, what state is the game in?.
/// </summary>
public class GameStateMachine
{
    public static GameStateMachine instance;
    private GameBaseState _currentState;
    public GameStateFactory factory;

    public GameStateMachine()
    {
        instance = this;
        factory = new GameStateFactory(this);
        _currentState = factory.InGameState();
        _currentState.EnterState();
    }

    public GameBaseState CurrentState 
    { 
        get { return _currentState; }
        set { _currentState = value; } 
    }

    public GameBaseState CurrentSubState
    {
        get { return _currentState._currentSubState; }
    }

    public static GameStateMachine GetContext()
    {
        return instance;
    }

}