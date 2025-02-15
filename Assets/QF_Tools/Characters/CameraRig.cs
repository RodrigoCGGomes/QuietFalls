using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform target;
    public Vector3 lerpedTarget;
    public Vector3 cameraTransform;
    private Vector2 lookVector, lookVectorSmooth;

    //Camera Orbiting stuff
    private float minPitch = -20f;
    private float maxPitch = 10f;

    private float yaw, pitch;

    private float orbitDistance = 10f;

    #region MonoBehaviour Calls
    private void OnEnable()
    {
        if (target != null)
        {
            lerpedTarget = target.transform.position;
        }
    }
    private void OnDisable()
    {
        
    }
    private void Start()
    {
        target = GetComponent<PlayerCamera>().playerItBelongsTo.transform;
        lerpedTarget = target.transform.position;
    }

    private void Update()
    {
        lerpedTarget = CalculateTargetSmoothFollow();
        CalculateCameraOrbit(lookVectorSmooth);
        transform.LookAt(target);
    }
    #endregion

    private Vector3 CalculateTargetSmoothFollow()
    {
        Vector3 calculation = Vector3.Lerp(lerpedTarget, target.transform.position, 10 * Time.deltaTime);
        return calculation;
    }
    private void CalculateCameraOrbit(Vector2 parLookVector)
    {
        yaw += parLookVector.x * Time.deltaTime * 70f;
        pitch += parLookVector.y * Time.timeScale;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset = rotation * new Vector3(0, 0, orbitDistance);
        transform.position = target.position + offset;

    }
    private void CalculateLookAt()
    { 
        
    }

    public void RelayLookVector(Vector2 relayedLookVector, Vector2 relayedLookVectorSmooth)
    {
        lookVector = relayedLookVector;
        lookVectorSmooth =  relayedLookVectorSmooth;
    }
}