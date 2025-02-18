using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRig : MonoBehaviour
{
    #region Variables
    [Header("Dependencies")]
    public Transform target;
    private Vector3 lerpedTargetPos;

    [Header("Settings")]
    public float cameraHeight = 1.5f;       //Camera height     
    public float rotationSpeed = 240f;      //How fast should the camera orbit?
    public float smoothing = 0.08f;         //Amount of rotation smoothing effect
    public float targetSmoothing = 0.01f;   //Amount of target smoothing effect  
    public float orbitDistance = 10f;       //Camera distance from character
    public float orbitZoomSpeed = 4f;       //Zoom Speed
    public float minPitchAngle = -20f;      //Limit to moving camera down
    public float maxPitchAngle = 50f;       //Limit to moving camera up

    [Header("Runtime Values")]
    public float yaw;                   //Current horizontal rotation of the rig     
    public float pitch;                 //Current vertical rotation of the rig
    public float orbitDistanceMoveSpeed;


    [Header("Please God")]
    public bool isStickActive = false; // Track if the stick is actively moving

    //Private Runtime Values
    private Vector2 lookVector, lookVectorSmooth;
    private Quaternion orbitRotation;
    #endregion Variables

    public void Tick()
    {
        // Smooth follow effect on the target's position
        lerpedTargetPos = Vector3.Lerp(lerpedTargetPos, target.position + (Vector3.up * cameraHeight), targetSmoothing * Time.deltaTime);

        orbitDistance += orbitDistanceMoveSpeed * Time.deltaTime * orbitZoomSpeed * -1f;

        SmoothCameraOrbit(orbitDistance);
    }


    #region MonoBehaviour Calls
    private void Start()
    {
        if (target == null) //We can't move on if there is no target
        {
            Debug.LogError("CameraRig's target is null, please assign a Target");
            return;
        }
        SetLerpedTargetPosition(target.position); 
    }
    #endregion

    #region Private Instructions
    private void SmoothCameraOrbit(float parOrbitDistance)
    {
        // Smoothly interpolate look vector for smoother camera movement
        lookVectorSmooth = Vector2.Lerp(lookVectorSmooth, lookVector, smoothing * Time.deltaTime);

        // Apply rotation
        yaw += lookVectorSmooth.x * rotationSpeed * Time.deltaTime ;
        pitch -= lookVectorSmooth.y * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitchAngle, maxPitchAngle);

        orbitRotation = Quaternion.Euler(pitch, yaw, 0);

        // Compute new position based on orbit distance
        Vector3 offset = orbitRotation * new Vector3(0, 0, -parOrbitDistance);
        transform.position = lerpedTargetPos + offset;

        // Ensure camera is looking at the smoothed target position
        transform.LookAt(lerpedTargetPos);
    }
    private void SetLerpedTargetPosition(Vector3 position)
    {
        lerpedTargetPos = position;
    }
    #endregion

    #region Input Relays
    /// <summary>
    /// "Relays" is a a way of passing the Player Input Listeners to this class.
    /// </summary>

    public void OnMoveRelay(InputAction.CallbackContext context)
    {
        
    }

    public void OnLookRelay(InputAction.CallbackContext context)
    {
        Vector2 newInput = context.ReadValue<Vector2>();

        if (context.performed) // When actively moving
        {
            lookVector = newInput;
            isStickActive = true; // Track movement
        }
        else if (context.canceled) // When stick is released
        {
            lookVector = Vector2.zero;
            isStickActive = false;
        }
    }
    public void RelayZoom(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            orbitDistanceMoveSpeed = context.ReadValue<float>();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            orbitDistanceMoveSpeed = 0f;
        }
    }
    #endregion
}
