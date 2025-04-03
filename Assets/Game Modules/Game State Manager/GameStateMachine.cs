/// <summary>
/// This state machine is used to track if the game has started or are in the main menu.
/// Then within the In-GameState, what state is the game in?.
/// </summary>
public class GameStateMachine
{
    private GameBaseState _currentState;
    public GameStateFactory factory;

    public GameStateMachine()
    {
        factory = new GameStateFactory(this);
        _currentState = factory.InGameState();
        _currentState.EnterState();
    }

    public GameBaseState CurrentState 
    { 
        get { return _currentState; }
        set { _currentState = value; } 
    }
}