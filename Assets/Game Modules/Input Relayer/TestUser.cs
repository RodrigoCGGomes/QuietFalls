using UnityEngine;
using UnityEngine.InputSystem;

public class TestUser : MonoBehaviour, IInputRelayer
{
    public TestUserSubClass subClass;
    //On the very start of TestUser instantiation, register all the receivers

    private void Awake()
    {
        subClass = new TestUserSubClass();
        subClass.Initialize();
    }

    public void RelayBack(InputAction.CallbackContext context)
    {
        // Do something like desired class instance.ReceiveBack(callbackcontext),
        // But automatically, using the list of registered Receivers... like RelayInputsToReceivers()
    }

    public void RelayLook(InputAction.CallbackContext context)
    {
        // Do something like desired class instance.ReceiveBack(callbackcontext),
        // But automatically, using the list of registered Receivers... like RelayInputsToReceivers()
    }

    public void RelayMove(InputAction.CallbackContext context)
    {
        // Do something like desired class instance.ReceiveBack(callbackcontext),
        // But automatically, using the list of registered Receivers... like RelayInputsToReceivers()
    }

    public void RelayZoom(InputAction.CallbackContext context)
    {
        // Do something like desired class instance.ReceiveBack(callbackcontext),
        // But automatically, using the list of registered Receivers... like RelayInputsToReceivers()
    }
}
