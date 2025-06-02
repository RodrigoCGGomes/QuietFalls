using System;
using UnityEngine;

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

    public Quaternion playerRelRotSmooth;       // Smooth value for PlayerRelRot variable.
    #endregion

    #region Constructors
    public Walking(GamePlayer parPlayer) : base(parPlayer)
    {
        // Debug.LogWarning("Created a Walking State");
    }
    #endregion

    #region Abstract Class Implementations
    public override void EnterState()
    {
        Debug.Log($"PlayerState.EnterState(); - {this}");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void ExitState()
    {
        throw new NotImplementedException();
    }

    public override void Tick()
    {
        if (player.moveInputVector != Vector2.zero)
        { 
            ProcessPlayerRotation();

            player.Move(
                player.GetStateMachine().currentState.GetPlayerCameraRelativeRotation().normalized * Vector3.forward,
                defaultMoveSpeed * base.player.moveValue.magnitude
                );
        }
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
        //Debug.Log($"playerRelRot = {playerRelRot.eulerAngles.y}");

        playerRelRotSmooth = Quaternion.Slerp(playerRelRotSmooth, player.GetStateMachine().currentState.GetPlayerCameraRelativeRotation(), rotationSmoothingSpeed * Time.deltaTime * player.moveValue.magnitude);
        player.transform.rotation = playerRelRotSmooth;
        // Debug.Log($"{playerRelRotSmooth.eulerAngles.y} , {playerRelRot.eulerAngles.y}");
    }


    


    #endregion
}