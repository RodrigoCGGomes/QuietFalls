using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameStateMachine stateMachine;

    public void Initialize()
    {
        instance = this;
        // Instantiate the GameStateMachine.
        stateMachine = new GameStateMachine();
    }

    public void Update()
    {
        stateMachine.CurrentState?.UpdateStates();
    }

    public static void ChangeState(GameBaseState newState)
    {
        if (instance?.stateMachine?.CurrentState == null)
            return;

        var current = instance.stateMachine.CurrentState;

        // Let the current state decide how to switch
        current.SwitchStates(newState);
    }
}