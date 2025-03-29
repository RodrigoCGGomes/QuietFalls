using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MainLightDirection : MonoBehaviour
{
    [SerializeField] private Material skyMaterial;
    [SerializeField] private Light lightComponent;
    [SerializeField] private SimpleSunRotate sunRotate;
    [SerializeField] private Material poleLightMaterial;
    public float DayNightRatio;

    [Header("Environment Colors")]
    public Gradient skyColor;
    public Gradient equatorColor;
    public Gradient groundColor;
    public AnimationCurve sunsetDisappearanceCurve;

    [Header("Lights")]
    public List<Light> poleLights;

    private void Update()
    {
        skyMaterial.SetVector("_MainLightDirection", transform.forward);

        // Shader Graph logic:
        float dot = Vector3.Dot(transform.forward, Vector3.up);
        DayNightRatio = Mathf.Clamp01(Mathf.InverseLerp(-1f, 1f, dot));

        // Evaluate intensity curve
        lightComponent.intensity = sunsetDisappearanceCurve.Evaluate(DayNightRatio) * 4f;

        LightLogic();

        DynamicGI.UpdateEnvironment();
    }

    private void LightLogic()
    {
        if (DayNightRatio > 0.3f)
        { 
            foreach(Light light in poleLights)
            {
                light.enabled = true;
                poleLightMaterial.EnableKeyword("_EMISSION");
                Debug.Log("Lights on");
            }
        }
        else
        {
            foreach (Light light in poleLights)
            {
                light.enabled = false;
                poleLightMaterial.DisableKeyword("_EMISSION");
                Debug.Log("Lights off");
            }
        }
    }
}