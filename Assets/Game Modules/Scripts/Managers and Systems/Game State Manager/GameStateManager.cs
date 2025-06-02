using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Global tracking of the game state.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameStateMachine stateMachine;


    public void Initialize()
    {
        Debug.Log("GameStateManager.Initialize()");

        instance = this;                                    // Singleton instance.
        stateMachine = new GameStateMachine();              // Instantiate the GameStateMachine

        #region Determine the initial state

        // Check the current scene and set the initial state accordingly.
        switch (SceneManager.GetActiveScene().name)
        {
            case "SplashSequence":
                ChangeState(new PreGameState(stateMachine, true));  // Set the initial state to PreGameState
                return;
            case "SampleScene":
                ChangeState(new SandboxState(stateMachine, true)); // Set the initial state to Sandbox, meaning no story will be run.
                return;
            default: 
                ChangeState(new SandboxState(stateMachine, true)); // Set the initial state to Sandbox, meaning no story will be run.
                Debug.LogWarning($"GameStateManager.Initialize() - Unknown scene '{SceneManager.GetActiveScene().name}' detected. Defaulting to PreGameState.");
                break;
        }   

        #endregion
    }

    public void Update()
    {
        stateMachine.CurrentState?.UpdateStates();
    }

    public static void ChangeState(GameBaseState newState)
    {
        Debug.Log($"GameStateManager.ChangeState (new State = {newState});");

        if (instance?.stateMachine?.CurrentState == null)
            return;

        var current = instance.stateMachine.CurrentState;

        // Let the current state decide how to switch
        current.SwitchStates(newState);
    }

    public static GameStateFactory GetFactory()
    {
        if (instance?.stateMachine == null)
        {
            Debug.LogError("GameStateManager.GetFactory() - GameStateMachine is not initialized.");
            return null;
        }
        return instance.stateMachine.factory;
    }
}