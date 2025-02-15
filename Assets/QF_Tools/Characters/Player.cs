using QuietFallsGameManaging;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : CharacterEngine
{
    [Header("Player Class")]
    public PlayerCamera playerCamera;

    private Vector2 moveValue;
    private Vector2 moveValueSmooth;

    private Vector2 lookValue;
    private Vector2 lookValueSmooth;

    

    private void OnEnable ()
    {
        playerCamera = PlayerCamera.SpawnPlayerCamera(this);
        //playerCamera = GetComponent<PlayerInput>().camera.GetComponent<PlayerCamera>(); 
        GetComponent<PlayerInput>().camera = playerCamera.GetComponent<Camera>();
        GameManager.instance.playerInputManager.JoinPlayer();
        
    }

    private void OnDisable()
    {
        Destroy(playerCamera);
    }
    private void Update()
    {
        // HOW THE FUCK DO I CHECK IF ONLOOK WAS CALLED?
        moveValueSmooth = Vector2.Lerp(moveValueSmooth, moveValue, 25 * Time.deltaTime);
        lookValueSmooth = Vector2.Lerp(lookValueSmooth, lookValue, 25 * Time.deltaTime);
        Move(transform.forward, moveValueSmooth.magnitude * 2f);
    }

    #region Input Listeners
    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
        
    }
    public void OnLook (InputAction.CallbackContext context)
    {
        lookValue = context.ReadValue<Vector2>();
        playerCamera.GetCameraRig().RelayLookVector(lookValue, lookValueSmooth);

    }
    #endregion
}