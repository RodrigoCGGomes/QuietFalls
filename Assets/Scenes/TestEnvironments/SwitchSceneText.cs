using QuietFallsGameManaging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchSceneText : MonoBehaviour
{
    private void Awake()
    {
        GameInputManager.instance.OnPauseEvent += OnSwitchScenes;
    }

    private void OnDestroy()
    {
        GameInputManager.instance.OnPauseEvent -= OnSwitchScenes;
    }

    private void OnSwitchScenes(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log($"Gameobject: '{this.gameObject.name}' listened to a PauseButton press");

            if (SceneManager.GetActiveScene().name == "UI_Controls")
            {
                SceneManager.LoadScene("ControlsTest");
                return;
            }
            if (SceneManager.GetActiveScene().name == "ControlsTest")
            {
                SceneManager.LoadScene("UI_Controls");
                return;
            }
        }

    }
}