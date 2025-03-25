using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Base class for the camera rigs (Third Person, First Person, Top Down View).
/// This class also works as a State and PlayerCamera acts like a state machine.
/// </summary>
public abstract class CameraState
{
    #region Variables
    public GamePlayer player;
    public Transform target;        //What should this camera rig follow? (Player)
    #endregion

    #region Constructors
    /// <summary>
    /// Teeeeest
    /// </summary>
    /// <param name="parGamePlayer"></param>
    public CameraState(GamePlayer parGamePlayer)
    {
        player = parGamePlayer;
        target = parGamePlayer.transform;
    }
    #endregion

    #region Abstract methods declarations
    /// <summary> Must be called exclusively by the Player.cs instance. </summary>
    public abstract void Tick();

    /// <summary> Start the camera rig life cycle </summary>
    public abstract void EnterState();

    /// <summary> End the camera rig life cycle </summary>
    public abstract void ExitState();

    // Called by Player.cs to forward input actions to the camera rig
    public abstract void OnMoveRelay(InputAction.CallbackContext context);
    public abstract void OnLookRelay(InputAction.CallbackContext context);
    public abstract void OnZoomRelay(InputAction.CallbackContext context);
    public abstract void OnBackRelay(InputAction.CallbackContext context);
    #endregion

    #region Tools
    /// <summary>
    /// Returns a ratio (-1 to 1) indicating how much the player is facing sideways relative to the camera.
    /// 0 = Facing forward/backward, -1 = Fully left, 1 = Fully right.
    /// </summary>
    /// <param name="parMoveMultiplier">Magnitude of the player's movement, used to scale the effect.</param>
    /// <param name="parRatioScale">Multiplier that adjusts the overall influence of the sideways ratio.</param>
    /// <param name="parPlayerRelativeRotation">Player's rotation relative to the camera.</param>
    public float CalculateSidewaysRatio(Quaternion parPlayerRelativeRotation, float parMoveMultiplier, float parRatioScale)
    {
        //Debug.Log($"{parPlayerRot}");

        float angleDifference = Mathf.DeltaAngle(                           // Calculate the shortest angle difference (-180 to 180)
            parPlayerRelativeRotation.eulerAngles.y,                        // Use the RAW rotation, not the smoothed one
            player.playerCamera.transform.rotation.eulerAngles.y);   
        
        float sidewaysRatio = Mathf.Sin(-angleDifference * Mathf.Deg2Rad);  // Normalize: -1 for RIGHT, 1 for LEFT, 0 when aligned (0° or 180°)
        sidewaysRatio *= parMoveMultiplier * parRatioScale;                 // Process the value, should be zero if character is moving, should be scaled.
        return sidewaysRatio;
    }
    #endregion
}
