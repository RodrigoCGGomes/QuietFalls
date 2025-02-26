using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region Variables
    public Player playerItBelongsTo;
    public CameraRigEngine currentRig { get; private set; }

    private Camera cameraComponent;
    #endregion

    #region MonoBehaviour Calls
    private void OnEnable()
    {
        cameraComponent = GetComponent<Camera>();
        currentRig = new ThirdPersonCamera(playerItBelongsTo.transform, cameraComponent);
    }
    private void OnDisable()
    {
        
    }
    #endregion

    #region Public Static Tools
    /* Public Static Tools are static functions that are supposed to be called from anywhere in the game
    and don't require a reference to an instance, for example: Spawn a new camera, delete all cameras and so on.
    It might be a good idea to move those tools to a PlayerCameraSystem */
    public static PlayerCamera SpawnPlayerCamera(Player player)
    {
        GameObject cameraGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/GamePlayerCamera"));
        PlayerCamera component = cameraGO.GetComponent<PlayerCamera>();
        component.playerItBelongsTo = player;

        return component;
    }
    #endregion

}