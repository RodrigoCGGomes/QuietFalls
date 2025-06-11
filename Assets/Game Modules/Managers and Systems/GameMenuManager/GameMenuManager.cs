using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager instance;
    private string testField;

    /// <summary> GlobalCanvas is the master canvas where all menus should reside in. It's scene-persistent and is spawned and managed by GameMenuManager. </summary>
    public Canvas globalCanvas;
    private MenuStateMachine stateMachine;

    /// <summary>
    /// Initial setup of the Preference System. Called only once by GameManager.SetUpGameManagers() at the start of the game.
    /// </summary>
    public void Initialize()
    {
        instance = this;
        SpawnGlobalCanvas();
        instance.testField = "banana";
        stateMachine = new MenuStateMachine(MenuStateFactory.InGameState(), )
    }

    /// <summary>
    /// Spawns the 'Global Canvas' into the scene and makes it scene-persistent.
    /// </summary>
    /// <returns></returns>
    public static Canvas SpawnGlobalCanvas()
    {
        Canvas result;
        result = Instantiate(Resources.Load<Canvas>("Canvases/CANVAS_InGameCanvases"));
        instance.globalCanvas = result;
        DontDestroyOnLoad(result);
        return result;
    }

    public static string GetTestString() //just testing private fields in a class being accessed with static methods
    { 
        return instance.testField;
    }

}
