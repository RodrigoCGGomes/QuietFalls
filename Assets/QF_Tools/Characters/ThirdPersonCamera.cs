using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : CameraRigEngine
{
    #region Variables
    //References
    public PlayerCamera playerCamera;       //Refence to PlayerCamera component.   

    //Constant variables, settings.
    public float cameraHeight = 1.5f;               //Camera height.
    public float rotationSpeed = 240f;              //How fast should the camera orbit?
    public float smoothing = 20f;                   //Amount of rotation smoothing effect.
    public float targetSmoothing = 5f;              //Amount of target smoothing effect.
    public float orbitDistance = 10f;               //Camera distance from character.
    public float orbitZoomSpeed = 4f;               //Zoom Speed.
    public float minPitchAngle = -20f;              //Limit to moving camera down.
    public float maxPitchAngle = 50f;               //Limit to moving camera up.
    public float sidewaysRatioMultiplier = 0.1f;    //Amount of camera assistance.
    private Vector3 lerpedTargetPos;                //Target smooth position.

    //Tracking runtime values
    public float yaw;                       //Current horizontal rotation of the rig.
    public float pitch;                     //Current vertical rotation of the rig.
    public float zoomInputDirection;        //Input direction for controlling camera distance. 
    private float sidewaysRatio;

    //Input-Related Variables
    public bool isStickActive = false;              // Track if the stick is actively moving
    private Vector2 lookVector, lookVectorSmooth;   // Tracks look values.

    //Private Runtime Values
    private Quaternion orbitRotation;

    #endregion Variables

    #region Constructors
    public ThirdPersonCamera(Transform parTarget, Camera parCamera) : base(parTarget, parCamera)
    {
        if (parTarget == null || parCamera == null)
        {
            throw new ArgumentNullException(nameof(parTarget), "ThirdPersonCamera constructor failed: either target or camera is null.");
        }

        
        ResetLerpedTargetPos();//lerped target position starts at target position
        playerCamera = parCamera.GetComponent<PlayerCamera>();
    }
    #endregion

    #region Base Class Method Overrides
    public override void Tick()
    {
        // Smooth follow effect on the target's position
        lerpedTargetPos = Vector3.Lerp(lerpedTargetPos, target.position + (Vector3.up * cameraHeight), targetSmoothing * Time.deltaTime);
        orbitDistance += zoomInputDirection * Time.deltaTime * orbitZoomSpeed * -1f;
        SmoothCameraOrbit(orbitDistance);
        sidewaysRatio = base.CalculateSidewaysRatio(playerCamera.playerItBelongsTo.moveValue.magnitude, sidewaysRatioMultiplier);

    }

    #region Input Relays
    public override void OnMoveRelay(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    public override void OnLookRelay(InputAction.CallbackContext context)
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
    public override void OnZoomRelay(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            zoomInputDirection = context.ReadValue<float>();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            zoomInputDirection = 0f;
        }
    }
    public override void OnBackRelay(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #endregion

    #region Private Instructions
    private void SmoothCameraOrbit(float parOrbitDistance)
    {
        // Smoothly interpolate look vector for smoother camera movement
        lookVectorSmooth = Vector2.Lerp(lookVectorSmooth, lookVector, smoothing * Time.deltaTime);

        // Apply rotation
        yaw += (lookVectorSmooth.x + (sidewaysRatio)) * rotationSpeed * Time.deltaTime;  //change 10f
        pitch -= lookVectorSmooth.y * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitchAngle, maxPitchAngle);

        orbitRotation = Quaternion.Euler(pitch, yaw, 0);

        // Compute new position based on orbit distance
        Vector3 offset = orbitRotation * new Vector3(0, 0, -parOrbitDistance);
        camera.transform.position = lerpedTargetPos + offset;

        // Ensure camera is looking at the smoothed target position
        camera.transform.LookAt(lerpedTargetPos);
    }
    /// <summary>
    /// Instantly synchronizes the lerped target position with the actual target position.
    /// </summary>
    private void ResetLerpedTargetPos()
    {
        lerpedTargetPos = target.position;
    }
    #endregion
}