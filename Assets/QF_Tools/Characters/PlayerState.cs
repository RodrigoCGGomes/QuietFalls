using UnityEngine;
// UNDER IMPLEMENTATION! TODO: TURN PLAUYER.CS INTO THE WALKING AROUND STATE AND TURN PLAYER.CS INTO THE STATE MACHINE.

/// <summary>
/// This is the base class for all of the Player States (WalkingAround, InCutScene and so on...)
/// </summary>
public abstract class PlayerStateEngine
{
    #region Variables
    /// <summary>
    /// 
    /// </summary>
    public PlayerState enumPlayerState;
    #endregion

    #region Abstract methods declarations
    public abstract void EnterState();
    public abstract void ExitState();
    #endregion 
}
public enum PlayerState 
{ 
    WalkingAround, 
    Blocked, 
    InCutScene 
}