using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Base class for the camera rigs (Third Person, First Person, Top Down View).
/// This class also works as a State and PlayerCamera acts like a state machine.
/// </summary>
public abstract class CameraRigEngine
{
    #region Variables
    public Transform target;        //What should this camera rig follow? (Player)
    public Camera camera;           //What camera this engine relates to
    #endregion

    #region Constructors
    public CameraRigEngine(Transform parTarget, Camera parCamera)
    {
        target = parTarget;
        camera = parCamera;
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
    /// <param name="moveMultiplier">Magnitude of the player's movement, used to scale the effect.</param>
    /// <param name="ratioScale">Multiplier that adjusts the overall influence of the sideways ratio.</param>
    public float CalculateSidewaysRatio(float moveMultiplier, float ratioScale)
    {  
        float angleDifference = Mathf.DeltaAngle(                           // Calculate the shortest angle difference (-180 to 180)
            target.transform.rotation.eulerAngles.y, 
            camera.transform.rotation.eulerAngles.y);   
        
        float sidewaysRatio = Mathf.Sin(-angleDifference * Mathf.Deg2Rad);  // Normalize: -1 for RIGHT, 1 for LEFT, 0 when aligned (0° or 180°)
        sidewaysRatio *= moveMultiplier * ratioScale;                       // Process the value, should be zero if character is moving, should be scaled.
        return sidewaysRatio;
    }
    #endregion
}
