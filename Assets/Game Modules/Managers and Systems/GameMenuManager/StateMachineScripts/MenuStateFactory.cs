public class MenuStateFactory : BaseStateFactory<GameState>
{
    public static GameState InGameState()
    {
        return new InGameMenus(isRoot: true);
    } 

}