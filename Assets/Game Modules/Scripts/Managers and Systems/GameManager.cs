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
                Debug.LogError("Somehow more than one GameInputManager instance exists.");
                return;
            }

            SpawnGameManagers();    
            SetUpGameManagers();    
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
        /// </summary>
        private static void SpawnGameManagers()
        {
            #region Instantiate Gameobjects
            GameObject globalContainerGO = new GameObject("[Managers and Systems]");

            GameObject managersContainerGO = new GameObject("[Managers]");
            GameObject systemsContainerGO = new GameObject("[Systems]");

            managersContainerGO.transform.parent = globalContainerGO.transform;
            systemsContainerGO.transform.parent = globalContainerGO.transform;

            GameObject gameManagerGO = new GameObject("Game Manager");
            GameObject gameStateManagerGO = new GameObject("State Manager");
            GameObject gamePlayerManagerGO = new GameObject("Player Manager");
            GameObject playerInputManagerGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/PlayerInputManager"));

            GameObject gamePreferencesSystemGO = new GameObject("Preferences System");

            instance = gameManagerGO.AddComponent<GameManager>();
            gameManagerGO.transform.parent = managersContainerGO.transform;

            instance.stateManager = gameStateManagerGO.AddComponent<GameStateManager>();
            gameStateManagerGO.transform.parent = managersContainerGO.transform;

            instance.playerManager = gamePlayerManagerGO.AddComponent<GamePlayerManager>();
            gamePlayerManagerGO.transform.parent = managersContainerGO.transform;

            instance.playerInputManager = playerInputManagerGO.GetComponent<PlayerInputManager>();
            playerInputManagerGO.transform.parent = managersContainerGO.transform;

            instance.preferencesSystem =gamePreferencesSystemGO.AddComponent<GamePreferencesSystem>();
            gamePreferencesSystemGO.transform.parent = systemsContainerGO.transform;
            #endregion
            
            DontDestroyOnLoad(globalContainerGO);
        }

        /// <summary>
        /// The reason this exists is because it's not desired to have their Initial configuration be handled by
        /// their MonoBehaviour Start() methods. This way, the GameManager can control the order of initialization.
        /// </summary>
        private static void SetUpGameManagers() // Calls their initialization instructions
        {
            //GameManager.instance.inputManager.Initialize();
            GameManager.instance.stateManager.Initialize();
            GameManager.instance.playerManager.Initialize();
            GameManager.instance.preferencesSystem.Initialize();
        } 
        #endregion
    }

}