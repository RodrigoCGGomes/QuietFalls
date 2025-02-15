using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Player playerItBelongsTo;
    private CameraRig cameraRig;
    private Camera cameraComponent;

    private void OnEnable()
    {
        cameraRig = transform.gameObject.AddComponent<CameraRig>();
    }

    private void OnDisable()
    {
        Destroy(cameraRig);
    }

    
    public static PlayerCamera SpawnPlayerCamera(Player player)
    {
        
        /*GameObject cameraGO = new GameObject("Player Camera");
        Camera cameraComponent = cameraGO.AddComponent<Camera>();
        PlayerCamera component = cameraGO.AddComponent<PlayerCamera>();
        component.playerItBelongsTo = player;
        component.cameraComponent = cameraComponent;
        */

        GameObject cameraGO = Instantiate(Resources.Load<GameObject>("GameManagerPrefabs/GamePlayerCamera"));
        PlayerCamera component = cameraGO.GetComponent<PlayerCamera>();
        component.playerItBelongsTo = player;

        return component;
    }
    

    public CameraRig GetCameraRig()
    {
        return cameraRig;
    }


}