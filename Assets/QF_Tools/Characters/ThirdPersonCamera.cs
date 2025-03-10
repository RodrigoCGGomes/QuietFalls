using System;
using Unity.VisualScripting;
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
    public float targetOrbitDist = 10f;             //Camera distance from character.
    public float orbitZoomSpeed = 20f;               //Zoom Speed.
    public float minPitchAngle = -70f;              //Limit to moving camera down.
    public float maxPitchAngle = 60f;               //Limit to moving camera up.
    private float minCameraDist = 3f;
    private float maxCameraDist = 15f;
    public float sidewaysRatioMultiplier = 0.08f;   //Amount of camera assistance.
    private Vector3 processedTargetPos;                //Target smooth position.

    //Tracking runtime values
    public float yaw;                           //Current horizontal rotation of the rig.
    public float pitch;                         //Current vertical rotation of the rig.
    public float gamepadCameraZoomDirection;    //Input direction for controlling camera distance. 
    private float sidewaysRatio;      

    //Input-Related Variables
    public bool isStickActive = false;              // Track if the stick is actively moving
    private string currentLookScheme;               // Is Look input being done with mouse or gamepad?
    private Vector2 lookVector, lookVectorSmooth;   // Tracks look values.
    #endregion Variables

    #region Constructors
    public ThirdPersonCamera(Transform parTarget, Camera parCamera) : base(parTarget, parCamera)
    {
        //Verify parameters
        if (parTarget == null || parCamera == null)
        {
            throw new ArgumentNullException(nameof(parTarget), "ThirdPersonCamera constructor failed: either target or camera is null.");
        }

        //Reset lerpedTargetPos so it starts at target position.
        processedTargetPos = target.position;

        //Assign variables
        playerCamera = parCamera.GetComponent<PlayerCamera>();
    }
    #endregion

    #region Base Class Method Overrides

    public override void EnterState()
    {
        throw new NotImplementedException();
    }

    public override void ExitState()
    {
        throw new NotImplementedException();
    }

    public override void Tick()
    {
        // Figure out the ACTUAL target position (Smoothing and raising it)
        processedTargetPos = Vector3.Lerp(processedTargetPos, target.position + (Vector3.up * cameraHeight), targetSmoothing * Time.deltaTime);

        //Apply the gamepad zoom
        targetOrbitDist += gamepadCameraZoomDirection * Time.deltaTime * orbitZoomSpeed * -1f;
        targetOrbitDist = Mathf.Clamp(targetOrbitDist, minCameraDist, maxCameraDist);

        //Process lookVector so it feels smooth
        lookVectorSmooth = Vector2.Lerp(lookVectorSmooth, lookVector, smoothing * Time.deltaTime);

        #region Apply Yaw and Pitch
        //All of this is because mouse delta does not require Time.deltaTime fix but gamepad does...
        float mouseMultiplier, stickMultiplier; 
        switch (currentLookScheme)
        {
            case "Mouse":
                mouseMultiplier = 1f;
                stickMultiplier = 0f;
                break;
            case "Gamepad":
                mouseMultiplier = 0f;
                stickMultiplier = 1f;
                break;
            default:
                mouseMultiplier = 0f;
                stickMultiplier = 1f;
                break;
        }
        yaw += ((lookVectorSmooth.x * stickMultiplier) + sidewaysRatio) * rotationSpeed * Time.deltaTime + (lookVectorSmooth.x * mouseMultiplier);
        pitch -= (lookVectorSmooth.y * stickMultiplier * rotationSpeed * Time.deltaTime) + (lookVectorSmooth.y * mouseMultiplier);
        pitch = Mathf.Clamp(pitch, minPitchAngle, maxPitchAngle);
        #endregion

        #region Camera Orbit
        // Compute camera position in orbit.
        Quaternion orbitDirection = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = orbitDirection * new Vector3(0, 0, -targetOrbitDist);
        Vector3 finalOrbitPosition = processedTargetPos + offset;
        camera.transform.position = finalOrbitPosition;


        RaycastHit hit;
        Vector3 startPoint = processedTargetPos;
        Vector3 endPoint = finalOrbitPosition;
        float sphereRadius = 0.5f;

        Vector3 direction = (endPoint - startPoint).normalized;

        float distance = Vector3.Distance(startPoint, endPoint);

        

        //Detect collision!
        if (Physics.SphereCast(startPoint, sphereRadius, direction, out hit, distance))
        {
            camera.transform.position = hit.point;

            // Draw collision on screen
            Debug.Log($"Hit : {Vector3.Distance(startPoint, hit.point)}");
            Debug.DrawRay(startPoint, direction * distance, Color.blue); // Main cast direction
            Debug.DrawLine(startPoint, hit.point, Color.red); // Show the actual collision
        }
        else
        {
            // Draw collision on screen
            Debug.DrawRay(startPoint, direction * distance, Color.green);
        }

        #endregion

        MakeCameraLookAtTarget();
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
        InputDevice device = context.control.device;

        if (context.performed)
        {
            lookVector = newInput;
            isStickActive = true; // Track movement

            // Determine input source
            currentLookScheme = context.control.device switch
            {
                Mouse => "Mouse",
                Gamepad => "Gamepad",
                Keyboard => "Keyboard",
                _ => $"Unknown device: {context.control.device.displayName}"
            };
            //Debug.Log($"Input Source: {currentLookScheme}");
        }
        else if (context.canceled)
        {
            lookVector = Vector2.zero;
            isStickActive = false;
        }
    }
    public override void OnZoomRelay(InputAction.CallbackContext context)
    {
        float mouseStep = 2.5f;

        string scheme = context.control.device switch
        {
            Mouse => "Mouse",
            Gamepad => "Gamepad",
            Keyboard => "Keyboard",
            _ => $"Unknown device: {context.control.device.displayName}"
        };

        if (scheme == "Mouse")
        {
            if (context.phase == InputActionPhase.Started)
            {
                targetOrbitDist = Mathf.Clamp(targetOrbitDist + mouseStep * context.ReadValue<float>() * -1f, minCameraDist, maxCameraDist);
            }
        }
        else
        {
            if (context.phase != InputActionPhase.Canceled)
            {
                gamepadCameraZoomDirection = context.ReadValue<float>();
            }
            else
            { 
                gamepadCameraZoomDirection = 0f;
            }
        }

    }
    public override void OnBackRelay(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #endregion

    #region Private Instructions
    /// <summary>
    /// Ensure camera is looking at the smoothed target position.
    /// </summary>
    private void MakeCameraLookAtTarget()
    {
        
        camera.transform.LookAt(processedTargetPos);
    }
    #endregion
}