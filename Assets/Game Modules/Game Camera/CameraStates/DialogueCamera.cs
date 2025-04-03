using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Simple implementation for testing. Camera facing the character during cutscenes
/// </summary>
public class DialogueCamera : CameraState
{
    /// <summary> Struct that holds the variables that are constantly updated during runtime. </summary>
    [Serializable] public struct CameraVariables
    {
        public Transform _finalCameraPosition;
        public Vector3 forwardAmount;
        public Vector3 rightAmount;
        public Vector3 upAmount;
        public Vector3 offset;

        public static CameraVariables Default => new CameraVariables
        {

        };
    }

    #region Variables 
    private CameraVariables current = CameraVariables.Default;
    #endregion Variables

    #region Constructors
    /// <summary> Creating a new instance via code to use as a state in a state machine is absolutelly correct </summary>
    public DialogueCamera(GamePlayer parGamePlayer) : base(parGamePlayer)
    {

    }
    #endregion

    #region Base Class Method Overrides
    public override void EnterState()
    {
        // Step back a bit and raise the camera slightly
        current.forwardAmount = player.transform.forward * 1f;
        current.rightAmount = player.transform.right * 1f;
        current.offset = current.forwardAmount + current.rightAmount;
    }

    public override void ExitState()
    {

    }

    public override void Tick()
    {
        // Smoothly move towards desired final location...
        player.playerCamera.transform.position = Vector3.Lerp(
            player.playerCamera.transform.position,
            player.transform.position + current.offset,
            5f * Time.deltaTime );

        // ...And throughout the whole time, keep looking at the player.
        player.playerCamera.transform.LookAt(player.transform.position + Vector3.up * 0f); // Look at player's face heightto do anything here.
    }
    #endregion

    #region Input Relays
    public override void OnMoveRelay(InputAction.CallbackContext context)
    {
        
    }
    public override void OnLookRelay(InputAction.CallbackContext context)
    {
        
    }
    public override void OnZoomRelay(InputAction.CallbackContext context)
    {
        
    }
    public override void OnBackRelay(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GamePlayerManager.PlayerResumePlaying();
            Debug.Log("Pressed back on DialogueCamera");
        }
        //Debug.Log("Pressed Esc/Back while on ThirdPersonCamera");
        
    }
    #endregion
}