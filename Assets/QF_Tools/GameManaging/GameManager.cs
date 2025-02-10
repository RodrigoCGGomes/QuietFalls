using QuietFallsGameManaging;
using UnityEngine;

namespace QuietFallsGameManaging
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager instance;

        private GameInputManager inputManager;
        private GameStateManager stateManager;
        private GamePlayerManager playerManager;

        private GameDebugger gameDebugger;

        #endregion

        #region Entry Points

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart() // Called once when Unity starts the game.
        {
            if (instance != null)
            {
                Debug.LogError("Somehow more than one GameInputManager instance exists.");
                return;
            }

            

            SpawnGameManagers();
            Debug.Log("GameManager's OnGameStart() procedure success.");
            SetUpGameManagers();
            OnEverySceneLoad();

            
        } 

        private static void OnEverySceneLoad() // Called every time a new scene loads.
        {
            //GameManager calls this first time, then GameSceneManager takes over.
        } 

        #endregion

        #region Instructions

        private static void SpawnGameManagers() // Instantiate the GameManager (container) Gameobject.
        {
            #region Instantiate Gameobjects
            GameObject globalContainerGO = new GameObject("GAME MANAGERS");


            GameObject gameManagerGO = new GameObject("Game Manager");
            GameObject gameInputManagerGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/GameInputManager"));
            GameObject gameStateManagerGO = new GameObject("Game State Manager");
            GameObject gamePlayerManagerGO = new GameObject("Game Player Manager");
            GameObject gameDebuggerManagerGO = new GameObject("Game Debugger");

            instance = gameManagerGO.AddComponent<GameManager>();
            gameManagerGO.transform.parent = globalContainerGO.transform;

            instance.inputManager = gameInputManagerGO.GetComponent<GameInputManager>();
            gameInputManagerGO.transform.parent = globalContainerGO.transform;

            instance.stateManager = gameStateManagerGO.AddComponent<GameStateManager>();
            gameStateManagerGO.transform.parent = globalContainerGO.transform;

            instance.playerManager = gamePlayerManagerGO.AddComponent<GamePlayerManager>();
            gamePlayerManagerGO.transform.parent = globalContainerGO.transform;

            instance.gameDebugger = gameDebuggerManagerGO.AddComponent<GameDebugger>();

            #endregion
            
            DontDestroyOnLoad(globalContainerGO);
        } 

        private static void SetUpGameManagers() // Calls their initialization instructions
        {
            GameManager.instance.inputManager.Initialize();
            GameManager.instance.stateManager.Initialize();
            GameManager.instance.playerManager.Initialize();
            GameManager.instance.gameDebugger.Initialize();
        } 
        #endregion
    }

}