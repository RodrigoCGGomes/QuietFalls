using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }
    public GamePlayer player;

    public PlayerStateMachine(GamePlayer parPlayer)
    {
        player = parPlayer;
        Debug.LogWarning($"Printing out the parPlayer argument that was passed to playerStateMachine: {parPlayer.name} ");
    }

    public void Initialize(PlayerState startingState)
    {
        Debug.LogWarning("Initialized PlayerStateMachine");
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
