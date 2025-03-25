using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public enum GameState {InCutscene, CharacterAction, };
    public GameState state;

    public void Initialize()
    {
        //Standard singleton check.
        if (instance != null)
        {
            Debug.LogError("Somehow more than one GameInputManager instance exists.");
            return;
        }
    }

    #region Public Static Methods
    public static GameState GetCurrentState()
    {
        return GameStateManager.instance.state;
    }

    #endregion

    #region Private Instructions
    // Not Implemented Yet
    #endregion

}