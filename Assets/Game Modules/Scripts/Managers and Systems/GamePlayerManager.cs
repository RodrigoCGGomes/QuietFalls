using System;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager instance;
    public Canvas virtualGamepadCanvas;

    public GamePlayer currentRegisteredPlayer;
    public PlayerCamera currentPlayerCamera;

    public void Initialize()
    {
        instance = this;
        // Temporary.
        if (Application.platform == RuntimePlatform.Android)
        {
            Canvas gamepadCanvas = Resources.Load<Canvas>("Canvases/CANVAS_VirtualGamepad");
            virtualGamepadCanvas = Instantiate(gamepadCanvas);
            DontDestroyOnLoad(virtualGamepadCanvas);
        }
    }

    /// <summary>
    /// Simple player register implementation to test cutscene
    /// </summary>
    public static void RegisterPlayer(GamePlayer _player)
    {
        instance.currentRegisteredPlayer = _player;
    }

    /// <summary>
    /// Simple player register implementation to test cutscene
    /// </summary>
    public static GamePlayer GetRegisteredPlayer()
    {
        if (instance.currentRegisteredPlayer == null)
        {
            Debug.LogError("GamePlayerManager.GetRegisteredPlayer() failed because there is no registered player.");
        }
        return instance.currentRegisteredPlayer;
    }

    public static void RegisterCamera(PlayerCamera _camera)
    {
        instance.currentPlayerCamera = _camera;
    }

    public static PlayerCamera GetRegisteredCamera()
    {
        if (instance.currentPlayerCamera == null)
        {
            Debug.LogError("GamePlayerManager.GetRegisteredPlayer() failed because there is no registered player.");
        }
        return instance.currentPlayerCamera;
    }

    public static void PlayerResumePlaying()
    {
        GameStateManager.ChangeState(GameStateManager.instance.stateMachine.factory.PlayerControlledState());
        instance.currentRegisteredPlayer.GetStateMachine().ChangeState(new Walking(GetRegisteredPlayer()));
        ThirdPersonCamera tp_camera = new ThirdPersonCamera(GetRegisteredPlayer());
        instance.currentPlayerCamera.stateMachine.ChangeState(tp_camera);
        tp_camera.EnterState();
        Debug.Log("PlayerResumePlaying()");
    }
}