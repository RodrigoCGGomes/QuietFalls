using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuietFallsGameManaging
{
    public class GameDeviceManager : MonoBehaviour
    {
        public static GameDeviceManager instance;

        public List<GameDevice> devices;

        private void Awake()
        {
            SubscribeToEvents();
            EnsureSingleton();
            devices = GetGameDeviceList();
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }
        public static List<GameDevice> GetGameDeviceList()
        {
            List<GameDevice> devicesFound = new List<GameDevice>();
            foreach (var device in InputSystem.devices)
            {
                devicesFound.Add(new GameDevice(device, GameDevice.AssignedPlayer.NotAssigned));
            }
            return devicesFound;
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Enabled)
            {
                Debug.Log($"New device detected: {device.displayName}");
            }
        }
        public void AssignPlayerToDevice(InputDevice dev, GameDevice.AssignedPlayer player)
        {
            bool couldFind = false;
            foreach (var gameDevice in devices)
            {
                if (gameDevice.device == dev)
                {
                    gameDevice.assignedPlayer = player;
                    couldFind = true;
                }
            }
            if (couldFind == true)
            {
                Debug.LogWarning($"Successfully assigned {player} to {dev.device.displayName} (id {dev.device.deviceId})");
            }
        }
        public static bool RegisterDevices (InputDevice dev1, InputDevice dev2) //Should implement player registraton
        {
            bool result = false; //Was the registration successfull?
            GameDeviceManager gameDeviceManager = GameDeviceManager.instance;
            foreach (var device in gameDeviceManager.devices)
            {
                Debug.Log($"Interating through {device.deviceName}");

                if (device.deviceId == dev1.deviceId)
                {
                    Debug.Log($"Registered {device.deviceName} to player 1 (id : {device.deviceId})");
                }

                result = true;
            }
            return result;
        }
        

        #region Private Methods
        private void EnsureSingleton()
        {
            instance = this;
        }
        private void SubscribeToEvents()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
        }
        private void UnsubscribeToEvents()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }
        #endregion

        [System.Serializable]
        public class GameDevice
        {
            public string deviceName;
            public int deviceId;
            public InputDevice device;
            public enum AssignedPlayer { NotAssigned, Player1, Player2 }
            public AssignedPlayer assignedPlayer;

            public GameDevice(InputDevice dev, AssignedPlayer player)
            {
                deviceName = dev.displayName;
                deviceId = dev.deviceId;
                device = dev;
                assignedPlayer = player;
                Debug.Log($"{dev.displayName} has the layout : '{dev.layout}'");
            }
        }
    }
}
