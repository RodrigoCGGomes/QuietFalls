using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameStateMachine stateMachine;

    public void Initialize()
    {
        Debug.Log("GameStateManager.Initialize()");

        instance = this;

        var factory = new GameStateFactory();
        stateMachine = new GameStateMachine(null, factory);
        DetermineInitialState();
    }

    #region MonoBehaviour Methods
    private void Update()
    {
        stateMachine.GetRootState()?.CascadeTick();
    }
    #endregion

    #region Private Methods
    private void DetermineInitialState()
    {
        var factory = (GameStateFactory)stateMachine.GetFactory();
        var activeScene = SceneManager.GetActiveScene().name;
        var logMessage = $"GameStateManager.DetermineInitialState() : Scene name is '{activeScene}', therefore decided for ";

        switch (activeScene)
        {
            case "SplashSequence":
                stateMachine.ChangeState(factory.PreGameState(stateMachine));
                logMessage += "PreGameState";
                break;
            case "SampleScene":
                stateMachine.ChangeState(factory.SandboxState(stateMachine));
                logMessage += "SandboxState";
                break;
            default:
                stateMachine.ChangeState(factory.SandboxState(stateMachine));
                logMessage += "PreGameState";
                break;
        }

        logMessage += "as the initial state.";
        Debug.LogWarning(logMessage);
    }
    #endregion

    #region Static Methods
    public static GameStateFactory GetFactory()
    {
        if (instance?.stateMachine == null)
        {
            Debug.LogError("GameStateManager.GetFactory() - GameStateMachine is not initialized.");
            return null;
        }
        return (GameStateFactory)instance.stateMachine.factory;
    }
    #endregion
}