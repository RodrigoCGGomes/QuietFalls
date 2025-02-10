using QuietFallsGameManaging;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class SplitCharacter : MonoBehaviour
{
    public PlayerInput playerInput;
    public Vector2 moveVec;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(GameInputManager.instance.moveValueSmoothed * Time.deltaTime);
    }

    public void MoveCharacter(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
        foreach (var device in playerInput.devices)
        {
           // Debug.Log($"{device.displayName}");
        }

        //Debug.Log($"move with {context.control.device.name}, id: {context.control.device.deviceId}");
    }


}
