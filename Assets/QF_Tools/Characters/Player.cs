using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is attached to the player prefab to make it playable.
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
    }
    private void OnEnable ()
    {
        //GameManager.instance.playerInputManager.JoinPlayer();
        if (overrideFramerate == true)
        {
            Application.targetFrameRate = framerate;
        }
    }
    private void OnDisable()
    {
        Destroy(playerCamera);
    }
    #endregion

    #region Private Methods
    private void Tick() 
    {
        moveInputAngle = CalculateAngleFromVector2(moveInputVector);
        playerRelRot = CalculateRelativeRotation(playerCamera.transform.rotation, moveInputAngle);

        moveValueSmooth = Vector2.Lerp(moveValueSmooth, moveValue, 0.1f * Time.deltaTime);

        Move(playerRelRot.normalized * Vector3.forward, defaultMoveSpeed * moveValue.magnitude);

        //Starting to implement character rotation:
        playerRelRotSmooth = Quaternion.Slerp(playerRelRotSmooth, playerRelRot, rotationSmoothingSpeed * Time.deltaTime * moveValue.magnitude);
        transform.rotation = playerRelRotSmooth;
    }
    private float CalculateAngleFromVector2 (Vector2 direction) //Converts a Vector2 into a 0-360º Value
    {
        float result = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg; //It's 90º off
        result = (result + 450f) % 360f; //90º Degree Correction
        return result;
    }
    private Quaternion CalculateRelativeRotation (Quaternion cameraRotation, float angle)
    {
        Quaternion result = Quaternion.Euler(0f, cameraRotation.eulerAngles.y + angle, 0f);
        return result;
    }

    #endregion

    #region Input Listeners
    /// All of those Input Listener Events are called from the PlayerInput component
    /// attached to the Player Prefab, and they are set up in the inspector as Unity Events.


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