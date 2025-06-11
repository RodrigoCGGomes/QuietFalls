using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameStateMachine stateMachine;

    public void Initialize()
    {
        Debug.Log("GameStateManager.Initialize()");

        instance = this;    // Set the singleton instance

        #region StateMachine Instantiation
        GameStateFactory factory = new GameStateFactory();
        //StateMachineInfo info = new StateMachineInfo("Game State Machine");
        stateMachine = new GameStateMachine(null, factory);
        stateMachine.ChangeState(DetermineInitialState());
        #endregion
    }

    #region MonoBehaviour Methods
    private void Update()
    {
        if (stateMachine != null)
        {
            stateMachine.GetRootState()?.CascadeTick();
        }
    }
    #endregion

    #region Private Methods
    private GameState DetermineInitialState()
    {
        GameState resultingState;
        GameStateFactory factory = (GameStateFactory)stateMachine.GetFactory(); // ATTENTION, CHANGE, WIP, REMEMBER, DON'T FORGET, CHANGE THE WAY SO THERE IS NO REASON TO REFERENCE IT'S FACTORY
        string activeScene = SceneManager.GetActiveScene().name;
        string logMessage = $"GameStateManager.DetermineInitialState() : Scene name is '{activeScene}', therefore decided for ";

        switch (activeScene)
        {
            case "SplashSequence":
                resultingState = GameStateFactory.PreGameState(stateMachine);
                logMessage += "PreGameState";
                break;
            case "SampleScene":
                resultingState = GameStateFactory.SandboxState(stateMachine);
                logMessage += "SandboxState";
                break;
            default:
                resultingState = GameStateFactory.SandboxState(stateMachine);
                logMessage += "PreGameState";
                break;
        }

        logMessage += "as the initial state.";
        Debug.LogWarning(logMessage);

        return resultingState;
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