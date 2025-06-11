using UnityEngine;
using UnityEngine.InputSystem;
using GameModules.Systems;

namespace GameModules.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// Singleton instance of the GameManager.
        /// </summary>
        private static GameManager instance;

        // Sub Game Managers
        private GameStateManager stateManager;
        private GamePlayerManager playerManager;
        public PlayerInputManager playerInputManager;
        private GamePreferencesSystem preferencesSystem;
        public GameDebugger gameDebugger;
        #endregion

        #region Entry Points
        /// <summary>
        /// This method is the very first thing that happens when the game starts.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart() // Called once when Unity starts the game.
        {
            //Standard singleton check.
            if (instance != null)
            {
                Debug.LogError("Somehow more than one GameManager instance exists.");
                return;
            }

            SpawnManagersAndSystems();
            InitializeManagersAndSystems();
            OnEverySceneLoad();
        }

        /// <summary>
        /// This method is called every time a new scene is loaded. Could be useful for adding logic that is common to all scene loads.
        /// It is manually called once by OnGameStart() and then munually called by SceneManager (NotImplemented Yet).
        /// </summary>
        private static void OnEverySceneLoad()
        {
            // Will only work after Implementing a SceneManager and having it call this method.
        }
        #endregion

        #region Instructions
        /// <summary>
        /// This method is only called once at the beginning of the game by GameManager.cs and it spawns the GameManagers.
        /// The Game Managers are scene persistent and are static references to the game's main systems.
        /// Each one is a GameObject named after the component it contains for readability.
        /// </summary>
        private static void SpawnManagersAndSystems()
        {
            #region Instantiate Gameobjects

            // This code creates those gameobjects in the hierarchy:

            //      [Managers and Systems]
            //      |-- [Managers]
            //      |   |-- [Game Manager]
            //      |   |-- [State Manager]
            //      |   |-- [Player Manager]
            //      |   `-- [PlayerInputManager]
            //      |-- [Systems]
            //      |   `-- [Preferences System]
            //      |   '-- [Canvas System]
            //      |-- [Debuggers and Helpers]
            //          `-- [OnScreenDebugSystem]

            // [Managers and Systems]
            GameObject managersAndSystemsGO = new GameObject("[Managers and Systems]");

            // [Managers]
            GameObject managersContainerGO = new GameObject("[Managers]");          // Creates the empty gameobject.
            managersContainerGO.transform.parent = managersAndSystemsGO.transform;  // Make it a child of the root gameobject.

            // [Game Manager]
            GameObject gameManagerGO = new GameObject("[Game Manager]");                    // Creates the GameObject.         
            instance = gameManagerGO.AddComponent<GameManager>();                           // Adds the GameManager component.
            gameManagerGO.transform.parent = managersContainerGO.transform;                 // Makes it a child of the "[Managers]" go.

            // [State Manager]
            GameObject gameStateManagerGO = new GameObject("[State Manager]");              // Creates the GameObject.     
            instance.stateManager = gameStateManagerGO.AddComponent<GameStateManager>();    // Adds the GameStateManager component.
            gameStateManagerGO.transform.parent = managersContainerGO.transform;            // Makes it a child of the "[Managers]" go.

            // [Player Manager]
            GameObject gamePlayerManagerGO = new GameObject("[Player Manager]");            // Creates the GameObject.
            instance.playerManager = gamePlayerManagerGO.AddComponent<GamePlayerManager>(); // Adds the GamePlayerManager component.
            gamePlayerManagerGO.transform.parent = managersContainerGO.transform;           // Makes it a child of the "[Managers]" go.
            
            // [Player Input Manager]
            GameObject playerInputManagerGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/PlayerInputManager"));     // Creates the GameObject.
            instance.playerInputManager = playerInputManagerGO.GetComponent<PlayerInputManager>();                                  // Adds the PlayerInputManager component.
            playerInputManagerGO.name = "[Player Input Manager]";                                                                   // Gets rid of the '(Clone)' suffix caused by Instantiate.
            playerInputManagerGO.transform.parent = managersContainerGO.transform;                                                  // Makes it a child of the "[Managers]" go.

            // [Systems]
            GameObject systemsContainerGO = new GameObject("[Systems]");            // Creates the empty gameobject.
            systemsContainerGO.transform.parent = managersAndSystemsGO.transform;   // Make it a child of the root gameobject.

            // [Preferences System]
            GameObject gamePreferencesSystemGO = new GameObject("[Preferences System]");                      // Creates the empty gameobject.
            instance.preferencesSystem = gamePreferencesSystemGO.AddComponent<GamePreferencesSystem>();
            gamePreferencesSystemGO.transform.parent = systemsContainerGO.transform;

            // [Canvas System]
            GameObject canvasSystemGO = new GameObject("[Canvas System]"); // Creates the empty gameobject.
            canvasSystemGO.transform.parent = systemsContainerGO.transform; // Make it a child of the "[Systems]" go.   
            CanvasSystem canvasSystem = canvasSystemGO.AddComponent<CanvasSystem>();                  // Adds the CanvasSystem component.
            canvasSystem.SpawnInGameCanvas(); // Spawns the in-game canvas.

            // [Debuggers and Helpers]
            GameObject debuggersAndHelpersGO = new GameObject("[Debuggers and Helpers]"); // Creates the empty gameobject.
            debuggersAndHelpersGO.transform.parent = managersAndSystemsGO.transform;      // Make it a child of the root gameobject.
            instance.gameDebugger = debuggersAndHelpersGO.AddComponent<GameDebugger>(); // Adds the GameDebugger component.

            // [OnScreenDebugSystem]
            GameObject onScreenDebugSystemGO = new GameObject("[OnScreenDebugSystem]"); // Creates the empty gameobject.
            onScreenDebugSystemGO.AddComponent<OnScreenDebugSystem>();                  // Adds the OnScreenDebugSystem component.
            onScreenDebugSystemGO.transform.parent = debuggersAndHelpersGO.transform;    // Make it a child of the "[Debuggers and Helpers]" go.
            #endregion
            DontDestroyOnLoad(managersAndSystemsGO);    // Makes the container scene-persistent
        }

        /// <summary>
        /// The reason this exists is because it's not desired to have their Initial configuration be handled by
        /// their MonoBehaviour Start() methods. This way, the GameManager can control the order of initialization.
        /// </summary>
        private static void InitializeManagersAndSystems() // Calls their initialization instructions
        {
            GameManager.instance.gameDebugger.Initialize();
            //GameManager.instance.inputManager.Initialize();
            GameManager.instance.stateManager.Initialize();
            GameManager.instance.playerManager.Initialize();
            GameManager.instance.preferencesSystem.Initialize();
        } 
        #endregion
    }
}