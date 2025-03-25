using System;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    public Canvas virtualGamepadCanvas;

    public void Initialize()
    {
        // Temporary.
        if (Application.platform == RuntimePlatform.Android)
        {
            Canvas gamepadCanvas = Resources.Load<Canvas>("Canvases/CANVAS_VirtualGamepad");
            virtualGamepadCanvas = Instantiate(gamepadCanvas);
            DontDestroyOnLoad(virtualGamepadCanvas);
        }
    }
}