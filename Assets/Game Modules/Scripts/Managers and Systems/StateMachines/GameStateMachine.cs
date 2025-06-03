using UnityEngine;

public class GameStateMachine : BaseStateMachine<GameState>
{
    public GameStateMachine(GameState initialState, BaseStateFactory<GameState> factory) : base(initialState, factory)
    {

    }
}