using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the walking player State of the player.
/// </summary>
public class Walking : PlayerState
{
    public int framerate; //Apagar

    #region Variables
    //Constant Values
    private float defaultMoveSpeed = 6f;        // Main character speed multiplier.
    public float rotationSmoothingSpeed = 10f;

    //Runtime Values
    public float moveInputAngle;                // WASD/Left Stick 0-360 value   /   Processed in Update.
    public Quaternion playerRelRot;             // Stores the player Camera in relation to it's Camera.
    public Quaternion playerRelRotSmooth;       // Smooth value for PlayerRelRot variable.

    #endregion

    #region Constructors
    public Walking(GamePlayer parPlayer) : base(parPlayer)
    {
        Debug.LogWarning("Created a Walking State");
    }
    #endregion

    #region Abstract Class Implementations
    public override void EnterState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.LogWarning("Walking State Enter State()");
    }

    public override void ExitState()
    {
        throw new NotImplementedException();
    }

    public override void Tick()
    {
        // Debug.LogWarning("Walking State Tick()");   
        CalculateMoveInputAngle();
        CalculateRelativeRotation();
        LerpMoveValue();
        ProcessPlayerRotation();

        player.Move(playerRelRot.normalized * Vector3.forward, defaultMoveSpeed * base.player.moveValue.magnitude);
        //Debug.Log($"moveValue magnitude is : {moveValue.magnitude}, and Vector2 value is : {moveValue}");
    }
    #endregion

    #region MonoBehaviour Calls (Erase)
    private void Update()
    {


        Application.targetFrameRate = framerate;
    }
    #endregion

    #region Private Instructions
    /// <summary>
    /// Lerps playerRelRotSmooth and assigns player rotation.
    /// </summary>
    private void ProcessPlayerRotation()
    {
        playerRelRotSmooth = Quaternion.Slerp(playerRelRotSmooth, playerRelRot, rotationSmoothingSpeed * Time.deltaTime * player.moveValue.magnitude);
        player.transform.rotation = playerRelRotSmooth;
    }
    
    /// <summary>
    /// Calculates the direction of the WASD input (moveInputVector) and
    /// assigns it into moveInputAngle as a 0-360 value.
    /// </summary>
    private void CalculateMoveInputAngle()
    {
        float result = Mathf.Atan2(-player.moveInputVector.y, player.moveInputVector.x) * Mathf.Rad2Deg; //It's 90� off
        result = (result + 450f) % 360f; //90� Degree Correction
        moveInputAngle =  result;
    }

    /// <summary>
    /// Calculates the relative direction between the player input (moveInputAngle) and
    /// the camera (playerCamera).
    /// </summary>
    private void CalculateRelativeRotation ()
    {
        Quaternion result = Quaternion.Euler(0f, player.playerCamera.transform.rotation.eulerAngles.y + moveInputAngle, 0f);
        playerRelRot =  result;
    }

    /// <summary>
    /// Calculates the smooth version of moveValue (moveValueSmooth) with a simple Vector2.Lerp.
    /// </summary>
    private void LerpMoveValue()
    {
        player.moveValueSmooth = Vector2.Lerp(player.moveValueSmooth, player.moveValue, 0.1f * Time.deltaTime);
    }
    #endregion
}