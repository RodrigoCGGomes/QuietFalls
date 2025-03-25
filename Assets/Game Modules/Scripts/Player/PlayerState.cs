using UnityEngine;
// UNDER IMPLEMENTATION! TODO: TURN PLAUYER.CS INTO THE WALKING AROUND STATE AND TURN PLAYER.CS INTO THE STATE MACHINE.

/// <summary>
/// This is the base class for all of the Player States (WalkingAround, InCutScene and so on...)
/// </summary>
public abstract class PlayerState
{
    #region Variables
    /// <summary>
    /// Hard coded enum to identify the state in case we need it in the future.
    /// </summary>
    public PlayerStateType enumPlayerState;

    public GamePlayer player;   // Reference to the player that this state belongs to.

    #endregion

    #region Constructors
    /// <summary>
    /// You should not create an instance of a PlayerState directly, but rather create a new instance of a derived class.
    /// </summary>
    /// <param name="parGamePlayer"> Reference to what Player this </param>
    public PlayerState(GamePlayer parPlayer)
    {
        player = parPlayer;
    }
    #endregion

    #region Abstract methods declarations
    /// <summary>
    /// Beginning of the life cycle of a PlayerState
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// Must be called exclusively by the Player.cs instance.
    /// </summary>
    public abstract void Tick();

    /// <summary>
    /// End of the life cycle of a Player State
    /// </summary>
    public abstract void ExitState();
    #endregion 
}
public enum PlayerStateType
{
    WalkingAround,
    Blocked,
    InCutScene
}