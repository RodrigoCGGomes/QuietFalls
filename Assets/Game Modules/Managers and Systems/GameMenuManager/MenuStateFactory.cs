public class MenuStateFactory : BaseStateFactory<GameState>
{
    public static GameState InGameState(BaseStateMachine<GameState> context)
    {
        return new InGameMenus(context, isRoot: true);
    } 

}