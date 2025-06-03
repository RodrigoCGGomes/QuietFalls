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

        switch (SceneManager.GetActiveScene().name)
        {
            case "SplashSequence":
                stateMachine.ChangeState(factory.PreGameState(stateMachine));
                break;
            case "SampleScene":
                stateMachine.ChangeState(factory.SandboxState(stateMachine));
                break;
            default:
                stateMachine.ChangeState(factory.SandboxState(stateMachine));
                Debug.LogWarning($"GameStateManager.Initialize() - Unknown scene '{SceneManager.GetActiveScene().name}' detected. Defaulting to SandboxState.");
                break;
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        stateMachine.GetRootState()?.CascadeTick();
    }

    public static GameStateFactory GetFactory()
    {
        if (instance?.stateMachine == null)
        {
            Debug.LogError("GameStateManager.GetFactory() - GameStateMachine is not initialized.");
            return null;
        }
        return (GameStateFactory)instance.stateMachine.factory;
    }
}