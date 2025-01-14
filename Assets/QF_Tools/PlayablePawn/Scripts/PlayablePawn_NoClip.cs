using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayablePawn_NoClip : MonoBehaviour
{
    //Tracking
    private InputDevice lastInputDevice;
    public string inputDeviceName;

    //Math
    public Vector2 moveVector, lookVector;
    
    //Events
    public static event Action<InputDevice, InputDevice> OnSwitchDevice;

    #region Monobehaviour Calls
    private void Awake()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        inputDeviceName = input.currentActionMap.controlSchemes[0].name.ToString();
    }
    private void Update()
    {
        transform.Translate(new Vector3(moveVector.x,0,moveVector.y) * Time.deltaTime * 10f);

    }
    #endregion

    #region Input Listeners
    public void Listen_Move(InputAction.CallbackContext context)
    {
        //Debug.Log($"Device: {context.control.device}, Value: {context.ReadValue<Vector2>()}");
        CheckDevice(context.control.device);
        moveVector = context.ReadValue<Vector2>();
    }
    public void Listen_Look(InputAction.CallbackContext context)
    {
        //Debug.Log($"Device: {context.control.device}, Value: {context.ReadValue<Vector2>()}");
        CheckDevice(context.control.device);
        lookVector = context.ReadValue<Vector2>();
    }
    public void Listen_Up(InputAction.CallbackContext context)
    {
        //Debug.Log($"Something ");
        CheckDevice(context.control.device);
    }
    public void Listen_Down(InputAction.CallbackContext context)
    {
        //Debug.Log("Something");
        CheckDevice(context.control.device);
    }
    #endregion

    #region StateMachine
    public InputDevice CheckDevice(InputDevice newInputDevice)
    {
        if (newInputDevice != lastInputDevice)
        {
            Debug.Log($"Switched Input Device from [{lastInputDevice}] to [{newInputDevice}]");
            lastInputDevice = newInputDevice;
            inputDeviceName = newInputDevice.name;
        }
        return newInputDevice;
    }
    #endregion


}
