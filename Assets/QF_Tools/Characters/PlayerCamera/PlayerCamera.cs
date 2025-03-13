using UnityEngine;

/// <summary>
/// PlayerCamera class is the only component needed in the player's camera gameobject.
/// It manages the cameara view in a state machine programming pattern.
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    #region Variables
    public GamePlayer player;

    public CameraStateMachine stateMachine;

    // Possible Camera States
    private ThirdPersonCamera thirdPersonState;

    #endregion

    #region MonoBehaviour Calls
    private void OnEnable()
    {
        //Create the state machine
        stateMachine = new CameraStateMachine(player);

        //Create instances of each CameraState.
        thirdPersonState = new ThirdPersonCamera(player);

        //Initialize first CameraState.
        stateMachine.Initialize(thirdPersonState);
    }

    private void Update()
    {
        stateMachine.currentState.Tick();
    }
    #endregion

    #region Public Static Tools (Temporarially disabled)
    //  (Region summary) Public Static Tools are static functions that are supposed to be called from anywhere in the game
    //  and don't require a reference to an instance, for example: Spawn a new camera, delete all cameras and so on.
    //  It might be a good idea to move those tools to a PlayerCameraSystem/Manager 

    /*public static PlayerCamera SpawnPlayerCamera(GamePlayer player)
    {
        GameObject cameraGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/GamePlayerCamera"));
        PlayerCamera component = cameraGO.GetComponent<PlayerCamera>();
        component.playerItBelongsTo = player;

        return component;
    }
    */
    #endregion

}