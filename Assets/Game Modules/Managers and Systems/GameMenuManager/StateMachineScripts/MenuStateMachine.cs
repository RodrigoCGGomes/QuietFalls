using UnityEngine;

public class MenuStateMachine : GameStateMachine
{
    public MenuStateMachine(GameState _initialState, BaseStateFactory<GameState> _factory, BaseStateMachineType _type) : base(_initialState, _factory, _type)
    {

    }
}
