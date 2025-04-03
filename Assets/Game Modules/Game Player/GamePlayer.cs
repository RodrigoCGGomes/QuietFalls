using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is attached to the player prefab to make it playable.
/// </summary>
public class GamePlayer : CharacterEngine
{
    #region Variables
    private PlayerStateMachine stateMachine;    //State Machine instance that will manage the PlayerState

    //Player States
    public Walking walkingState;

    //References
    public PlayerCamera playerCamera;           // Reference to the this player`s PlayerCamera component.

    //Shared variables between PlayerStates
    public Vector2 moveValue;  //REMEMBER TO CHECK SMOOTHED VERSION AS IT APPEARS TO NEVER BE USED
    public Vector2 moveInputVector;             // WASD/Left Stick Vector2       /   Set by Input Listener.
    #endregion

    #region MonoBehaviour Calls
    private void OnEnable()
    {
        //Time.timeScale = 0.1f;

        //Create the state machine
        stateMachine = new PlayerStateMachine(this);

        //Create instances of each CameraState
        walkingState = new Walking(this);

        //Initialize first PlayerState.
        stateMachine.Initialize(walkingState);

        //Initialize PlayerCamera
        playerCamera.Initialize(this);

        GamePlayerManager.RegisterPlayer(this);
    }

    private void Update()
    {
        stateMachine.currentState.Tick();
        playerCamera.stateMachine.currentState.Tick();
    }
    #endregion

    #region Input Listeners
    // (Region summary) All of those Input Listener Events are called from the PlayerInput
    // component attached to the Player Prefab, and they are set up in the inspector as Unity Events.

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
        if (context.phase != InputActionPhase.Canceled)
        {
            moveInputVector = context.ReadValue<Vector2>();
        }
        playerCamera.stateMachine.currentState.OnMoveRelay(context);
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        playerCamera.stateMachine.currentState.OnLookRelay(context);
        //Debug.LogWarning($"Look value: {context.ReadValue<Vector2>()}");
        //Debug.LogWarning($"{context.control.device}");
    }
    public void OnZoom(InputAction.CallbackContext context)
    {
        playerCamera.stateMachine.currentState.OnZoomRelay(context);
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        playerCamera.stateMachine.currentState.OnBackRelay(context);
        if (context.phase == InputActionPhase.Started)
        {
            
        }
    }
    #endregion

    /// <summary>
    /// This should be obsolete once the IInputRelayer and IInputListener are added as I will reset in the states
    /// </summary>
    public void ResetVariables()
    {
        moveValue = Vector2.zero;

        moveInputVector = Vector2.zero;
    }

    #region Getters
    public PlayerStateMachine GetStateMachine()
    {
        return stateMachine;
    }
    #endregion
}
