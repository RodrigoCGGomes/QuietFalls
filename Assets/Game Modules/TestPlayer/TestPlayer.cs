using QuietFallsGameManaging;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    public Vector2 moveVector;
    public int playerId;
    public PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();

        ListDevices();
        
    }

    private void Update()
    {
        transform.Translate(transform.up * moveVector.y * Time.deltaTime);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void ListDevices()
    {
        Debug.LogWarning($"Devices list: {playerInput.devices}");

        foreach (InputDevice i in playerInput.devices)
        {
            Debug.LogWarning($"Device : {i.device.name}");
        }
    }
}
