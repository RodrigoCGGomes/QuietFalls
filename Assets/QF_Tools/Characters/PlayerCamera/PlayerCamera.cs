using UnityEngine;

/// <summary>
/// PlayerCamera class is the only component required in the player's camera gameobject.
/// It manages the cameara view in a state machine programming pattern.
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    private GamePlayer player;
    public CameraStateMachine stateMachine;

    // Possible Camera States
    private ThirdPersonCamera thirdPersonState;

    public void Initialize(GamePlayer parPlayer)
    {
        player = parPlayer;                                     // Passes on the player reference.
        stateMachine = new CameraStateMachine(parPlayer);       // Create the state machine
        thirdPersonState = new ThirdPersonCamera(parPlayer);    // Create instances of each CameraState.
        stateMachine.Initialize(thirdPersonState);              // Initialize first CameraState.
    }
}