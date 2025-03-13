using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public enum GameState {InCutscene, CharacterAction, };
    public GameState state;

    public void Initialize()
    {
        EnsureSingleton();
    }

    #region Public Static Methods

    public static GameStateManager GetCurrentState()
    {
        return GameStateManager.instance;
    }

    #endregion

    #region Private Instructions

    private void EnsureSingleton()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Somehow more than one GameStateManager instance exists. Being deleted.");
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    #endregion

}