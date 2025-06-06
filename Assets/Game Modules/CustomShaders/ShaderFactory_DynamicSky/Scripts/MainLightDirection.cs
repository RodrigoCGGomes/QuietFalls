using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MainLightDirection : MonoBehaviour
{
    [Header("Settings")]
    [Range (0,1)] public float fogAmount;
    public Gradient horizonGradient;
    [Range(0,1)] public float fogLuminosity;
    [Range(0, 1)] public float fogWashOut;

    [Header("References")]
    [SerializeField] private Material skyMaterial;
    [SerializeField] private Light lightComponent;
    [SerializeField] private SimpleSunRotate sunRotate;
    [SerializeField] private Material poleLightMaterial;
    private float DayNightRatio;

    [Header("Curves")]
    public AnimationCurve sunsetDisappearanceCurve;

    [Header("Lights")]
    public List<Light> poleLights;

    private void Update()
    {
        if (skyMaterial != null)
        {
            skyMaterial.SetVector("_MainLightDirection", transform.forward);
        }

        // Shader Graph logic:
        float dot = Vector3.Dot(transform.forward, Vector3.up);
        DayNightRatio = Mathf.Clamp01(Mathf.InverseLerp(-1f, 1f, dot));

        // Evaluate intensity curve
        lightComponent.intensity = sunsetDisappearanceCurve.Evaluate(DayNightRatio) * 4f;


        SetFogColor();
        LightLogic();

        DynamicGI.UpdateEnvironment();
    }

    private void SetFogColor()
    {
        Color rawHorizonColor = horizonGradient.Evaluate(DayNightRatio);
        Color horizonColorDarkened = rawHorizonColor * fogLuminosity;

        Color grey = new Color(fogWashOut, fogWashOut, fogWashOut, 1f);

        RenderSettings.fogColor = horizonColorDarkened + grey;

        RenderSettings.fogDensity = fogAmount * 0.2f;

    }   

    private void LightLogic()
    {
        if (DayNightRatio > 0.6f)
        { 
            foreach(Light light in poleLights)
            {
                light.enabled = true;
                if (poleLightMaterial != null)
                { 
                    poleLightMaterial.EnableKeyword("_EMISSION");
                }
            }
        }
        else
        {
            foreach (Light light in poleLights)
            {
                light.enabled = false;
                if (poleLightMaterial != null)
                {
                    poleLightMaterial.DisableKeyword("_EMISSION");
                }
            }
        }
    }

}