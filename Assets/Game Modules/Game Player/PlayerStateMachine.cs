using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }
    private GamePlayer player;

    /// <summary>
    /// Constructor takes the player instance as a parameter so that the state machine knows which player it is managing.
    /// </summary>
    public PlayerStateMachine(GamePlayer parPlayer)
    {
        player = parPlayer;
    }

    public void Initialize(PlayerState startingState)
    {
        //Debug.LogWarning("Initialized PlayerStateMachine");
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }

    public void Tick()
    {
        //currentState.Tick();
        //player.playerCamera.stateMachine.currentState.Tick();
    }
}
