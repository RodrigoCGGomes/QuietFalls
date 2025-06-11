using UnityEngine;

public class GameStateMachine : BaseStateMachine<GameState>
{
    public static GameStateMachine instance;

    public GameStateMachine(GameState _initialState, BaseStateFactory<GameState> _factory) : base(_initialState, _factory)
    {
        instance = this;
        base.stateMachineInfo = new StateMachineInfo(this, "Game State Machine", 111);
    }
}