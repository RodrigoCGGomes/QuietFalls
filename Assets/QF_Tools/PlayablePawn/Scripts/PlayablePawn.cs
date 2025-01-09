using UnityEngine;
using UnityEngine.InputSystem;

public class PlayablePawn : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
    }

}
