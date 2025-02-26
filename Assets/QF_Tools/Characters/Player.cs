using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is attached to the player prefab to make it playable.
/// TODO: 
/// </summary>
public class Player : CharacterEngine
{
    #region Variables
    [Header("Temporary")]
    public int framerate;
    public bool overrideFramerate;

    [Header("Player Class References")]
    public PlayerCamera playerCamera;       // Reference to the this player`s PlayerCamera component.

    [Header("Player Class Properties")]
    public float defaultMoveSpeed;          // Main character speed multiplier.
    public float rotationSmoothingSpeed;

    [Header("Runtime Values")]
    public Vector2 moveInputVector;         // WASD/Left Stick Vector2       /   Set by Input Listener.
    public float moveInputAngle;            // WASD/Left Stick 0-360 value   /   Processed in Update.
    public Quaternion playerRelRot;         // Stores the player Camera in relation to it's Camera.
    public Quaternion playerRelRotSmooth;   // Smooth value for PlayerRelRot variable.

    public Vector2 moveValue, moveValueSmooth; //REMEMBER TO CHECK SMOOTHED VERSION AS IT APPEARS TO NEVER BE USED
    #endregion

    #region MonoBehaviour Calls
    private void Update()
    {
        this.Tick();
        playerCamera.currentRig.Tick();

        //Temporary
        if (overrideFramerate == true)
        {
            Application.targetFrameRate = framerate;
        }
    }
    private void OnEnable ()
    {
        //GameManager.instance.playerInputManager.JoinPlayer();

    }
    private void OnDisable()
    {
        Destroy(playerCamera);
    }
    #endregion

    // Run Player Logic
    private void Tick()
    {
        CalculateMoveInputAngle();
        CalculateRelativeRotation();
        CalculateMoveValueSmooth();

        base.Move(playerRelRot.normalized * Vector3.forward, defaultMoveSpeed * moveValue.magnitude);

        //Starting to implement character rotation:
        playerRelRotSmooth = Quaternion.Slerp(playerRelRotSmooth, playerRelRot, rotationSmoothingSpeed * Time.deltaTime * moveValue.magnitude);
        transform.rotation = playerRelRotSmooth;
    }

    #region Private Methods
    /// <summary>
    /// Calculates the direction of the WASD input (moveInputVector) and
    /// assigns it into moveInputAngle as a 0-360 value.
    /// </summary>
    private void CalculateMoveInputAngle()
    {
        float result = Mathf.Atan2(-moveInputVector.y, moveInputVector.x) * Mathf.Rad2Deg; //It's 90º off
        result = (result + 450f) % 360f; //90º Degree Correction
        moveInputAngle =  result;
    }

    /// <summary>
    /// Calculates the relative direction between the player input (moveInputAngle) and
    /// the camera (playerCamera).
    /// </summary>
    private void CalculateRelativeRotation ()
    {
        Quaternion result = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y + moveInputAngle, 0f);
        playerRelRot =  result;
    }

    /// <summary>
    /// Calculates the smooth version of moveValue (moveValueSmooth) with a simple Vector2.Lerp.
    /// </summary>
    private void CalculateMoveValueSmooth()
    {
        moveValueSmooth = Vector2.Lerp(moveValueSmooth, moveValue, 0.1f * Time.deltaTime);
    }
    #endregion

    #region Input Listeners
    /// (Region summary) All of those Input Listener Events are called from the PlayerInput
    /// component attached to the Player Prefab, and they are set up in the inspector as Unity Events.

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();

        //Starting to implement character rotation:
        if (context.phase != InputActionPhase.Canceled)
        {
            moveInputVector = context.ReadValue<Vector2>();
        }
    }
    public void OnLook (InputAction.CallbackContext context)
    {
        playerCamera.currentRig.OnLookRelay(context);
        Debug.LogWarning($"Look value: {context.ReadValue<Vector2>()}");
    }
    public void OnZoom(InputAction.CallbackContext context)
    {
        playerCamera.currentRig.OnZoomRelay(context);
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        playerCamera.currentRig.OnBackRelay(context);
        if (context.phase == InputActionPhase.Started)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
    #endregion
}