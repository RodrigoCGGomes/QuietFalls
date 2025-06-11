using System;
using UnityEngine;

public class CanvasSystem : MonoBehaviour
{
    public static CanvasSystem instance;
    public Canvas inGameCanvas;

    internal void SpawnInGameCanvas()
    {
        inGameCanvas = Instantiate(Resources.Load<Canvas>("Canvases/CANVAS_InGameCanvases")); // Load the in-game canvas from Resources.
    }
}
