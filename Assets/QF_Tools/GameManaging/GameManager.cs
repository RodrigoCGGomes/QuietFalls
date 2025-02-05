using QuietFallsGameManaging;
using Unity.VisualScripting;
using UnityEngine;

namespace QuietFallsGameManaging
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private GameInputManager inputManager;
        

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {
            EnsureSingleton();
            SetUpGameManagers();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnEverySceneLoad()
        {
            Debug.Log("A new scene has loaded.");
        }

        private void Awake()
        {
            Debug.Log("GameManagerInScene");
        }

        #region Instructions
        private static void EnsureSingleton()
        {
            Debug.Log("EnsureSingleton");
            if (instance == null) //If there is no GameManager instance, create the GameManager
            {
                GameObject gameManagerContainer = new GameObject("GameManagers");
                GameObject gameManagerGO = new GameObject("GameManager");
                GameObject gameInputManagerGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/GameInputManager"));


                instance = gameManagerGO.AddComponent<GameManager>();
                gameManagerGO.transform.parent = gameManagerContainer.transform;

                instance.inputManager = gameInputManagerGO.GetComponent<GameInputManager>();
                gameInputManagerGO.transform.parent = gameManagerContainer.transform;

                DontDestroyOnLoad(gameManagerContainer);
            }
        }
        private static void SetUpGameManagers() //First time setup of GameManager gameobject.
        {
            GameManager.instance.inputManager.Initialize();
        }
        private void SceneStartDueDiligence() //Checks that ought to be performed at every scene start.
        { 
            
        }
        #endregion
    }
}