using QuietFallsGameManaging;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : CharacterEngine
{
    [Header("Temporary")]
    public int framerate;
    public bool overrideFramerate;


    [Header("Player Class References")]
    public CameraRig playerCamera;

    [Header("Player Class Properties")]
    public float defaultMoveSpeed = 1f;

    [Header("Runtime Values")]
    public Vector2 inputDirection;
    public float inputDirectionProcessed;
    public Quaternion charRelRotation;
    public Quaternion charRelRotationSmooth;
    public Quaternion test;

    private Vector2 moveValue;
    private Vector2 moveValueSmooth;

    #region MonoBehaviour Calls
    private void OnEnable ()
    {
        GameManager.instance.playerInputManager.JoinPlayer();
        if (overrideFramerate == true)
        {
            Application.targetFrameRate = framerate;
        }
    }

    private void OnDisable()
    {
        Destroy(playerCamera);
    }
    private void Update()
    {
        inputDirectionProcessed = Mathf.Atan2(-inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
        inputDirectionProcessed = (inputDirectionProcessed + 450) % 360; // Normalize to 0-360 degrees
        charRelRotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y + inputDirectionProcessed, 0);

        moveValueSmooth = Vector2.Lerp(moveValueSmooth, moveValue, 35 * Time.deltaTime);

        Move(charRelRotation.normalized * Vector3.forward,  defaultMoveSpeed * moveValue.magnitude);


        test = Quaternion.Euler(transform.forward);

        //Starting to implement character rotation:
        charRelRotationSmooth = Quaternion.Slerp(charRelRotationSmooth, charRelRotation, 20f * Time.deltaTime * moveValueSmooth.magnitude);
        transform.rotation = charRelRotationSmooth;
        playerCamera.Tick();
    }
    #endregion

    #region Input Listeners
    /// <summary>
    /// All of those Input Listener Events are called from the PlayerInput component
    /// attached to the Player Prefab, and they are set up in the inspector as Unity Events.
    /// </summary>

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();

        //Starting to implement character rotation:
        if (context.phase != InputActionPhase.Canceled)
        {
            inputDirection = context.ReadValue<Vector2>();  

            
            

        }


    }
    public void OnLook (InputAction.CallbackContext context)
    {
        playerCamera.OnLookRelay(context);
    }
    public void OnZoom(InputAction.CallbackContext context)
    {
        Debug.LogWarning($"Phase '{context.phase}', Value: {context.ReadValue<float>()}");
        playerCamera.RelayZoom(context);
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
        SceneManager.LoadScene("TitleScene");
        }
    }
    #endregion
}