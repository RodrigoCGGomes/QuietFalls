using UnityEngine.InputSystem;

public interface IInputRelayer
{
    abstract void RelayMove(InputAction.CallbackContext context);
    abstract void RelayLook(InputAction.CallbackContext context);
    abstract void RelayZoom(InputAction.CallbackContext context);
    abstract void RelayBack(InputAction.CallbackContext context);
}