using UnityEngine;

public class CameraStateMachine
{
    public CameraState currentState { get; private set; }
    //public GamePlayer player;


    #region Constructors
    private CameraStateMachine()
    {
        //Private default constructor to prevent instantiation without passing the GamePlayer
    }

    public CameraStateMachine(GamePlayer parPlayer)
    {
        //player = parPlayer;
        Debug.LogWarning("Created an instance of CameraStateMachine");
    }
    #endregion

    public void Initialize(CameraState startingState)
    {
        //currentState.player = player;
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(CameraState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }
}   