
using QuietFallsGameManaging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Collections.Generic;


public class PlayerSpawner : MonoBehaviour
{
    public int deviceId1, deviceId2;
    public GameObject playerGameObject;

    void Start()
    {
        SpawnPlayer(deviceId1);
        SpawnPlayer(deviceId2);
    }

    private void SpawnPlayer(int deviceId)
    {

        InputDevice device = InputSystem.GetDeviceById(deviceId);
        if (device != null)
        {
            PlayerInput playerInput = PlayerInput.Instantiate(playerGameObject, -1, null, -1, device);

            if (playerInput != null)
            {
                playerInput.user.UnpairDevices();
                InputUser.PerformPairingWithDevice(InputSystem.GetDeviceById(deviceId), playerInput.user);
            }
        }
        else
        {
            Debug.LogError($"No device found with ID: {deviceId}");
        }
    }

}
