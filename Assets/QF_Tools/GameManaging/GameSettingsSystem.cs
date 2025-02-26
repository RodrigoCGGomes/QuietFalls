using UnityEditor;
using UnityEngine;

public class GameSettingsSystem : MonoBehaviour
{
    private int targetDPI;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        QualitySettings.resolutionScalingFixedDPIFactor = 0.6f;
        Debug.Log($"GameSettingSystem {this.gameObject.name}");
    }
}
