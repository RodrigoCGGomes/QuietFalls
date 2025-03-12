using UnityEngine;

/// <summary>
/// PlayerCamera class is the only component needed in the player's camera gameobject.
/// It manages the cameara view in a state machine programming pattern
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    #region Variables
    public Player playerItBelongsTo;

    public CameraStateMachine stateMachine;

    // Possible Camera States
    private ThirdPersonCamera thirdPersonState;

    private Camera cameraComponent;
    #endregion

    #region MonoBehaviour Calls
    private void OnEnable()
    {
        cameraComponent = GetComponent<Camera>();

        //Create the state machine
        stateMachine = new CameraStateMachine();

        //Create instances of each CameraState
        thirdPersonState = new ThirdPersonCamera(playerItBelongsTo.transform, playerItBelongsTo.playerCamera.cameraComponent);

        stateMachine.Initialize(thirdPersonState);
    }
    #endregion

    #region Public Static Tools
    /* Public Static Tools are static functions that are supposed to be called from anywhere in the game
    and don't require a reference to an instance, for example: Spawn a new camera, delete all cameras and so on.
    It might be a good idea to move those tools to a PlayerCameraSystem/Manager */
    public static PlayerCamera SpawnPlayerCamera(Player player)
    {
        GameObject cameraGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/GamePlayerCamera"));
        PlayerCamera component = cameraGO.GetComponent<PlayerCamera>();
        component.playerItBelongsTo = player;

        return component;
    }
    #endregion

}