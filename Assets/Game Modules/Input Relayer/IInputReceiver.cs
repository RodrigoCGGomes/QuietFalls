using UnityEngine.InputSystem;

public interface IInputReceiver
{
    abstract void ReceiveMove(InputAction.CallbackContext context);
    abstract void ReceiveLook(InputAction.CallbackContext context);
    abstract void ReceiveZoom(InputAction.CallbackContext context);
    abstract void ReceiveBack(InputAction.CallbackContext context);
}
