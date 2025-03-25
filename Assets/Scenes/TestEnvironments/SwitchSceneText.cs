using QuietFallsGameManaging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is absolutely a placeholder. It was created to be put in the following scenes:
/// - ControlsTest, UI_Controls
/// It was just a quick way to switch between the two scenes in those test scenes when you press Escape.
/// The way it works is that those scripts are attached to the header UI element
/// that says "Press (Start / Esc) to switch scenes."
/// </summary>
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