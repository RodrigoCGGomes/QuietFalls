using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : CameraState
{
    #region Variables 


    //Constant variables, settings.
    public float cameraHeight = 0.5f;               //Camera height.
    public float rotationSpeed = 240f;              //How fast should the camera orbit?
    public float smoothing = 20f;                   //Amount of rotation smoothing effect.
    public float targetSmoothing = 5f;              //Amount of target smoothing effect.
    public float targetOrbitDist = 5f;              //Camera distance from character.
    public float orbitZoomSpeed = 8f;               //Zoom Speed.
    public float minPitchAngle = -70f;              //Limit to moving camera down.
    public float maxPitchAngle = 60f;               //Limit to moving camera up.
    private float minCameraDist = 3f;
    private float maxCameraDist = 6f;
    public float sidewaysRatioMultiplier = 0.08f;   //Amount of camera assistance.
    private Vector3 processedTargetPos;             //Target smooth position.

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
    /// <summary>
    /// Creating a new instance via code to use as a state in a state machine is absolutelly correct
    /// </summary>
    /// <param name="parGamePlayer">  </param>
    public ThirdPersonCamera(GamePlayer parGamePlayer) : base(parGamePlayer)
    {
        //Don't really need to do anything here.
    }
    #endregion

    #region Base Class Method Overrides

    public override void EnterState()
    {
        //Reset lerpedTargetPos so it starts at target position.
        processedTargetPos = target.position;
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

        // Compute camera position in orbit.
        Quaternion orbitDirection = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = orbitDirection * new Vector3(0, 0, -targetOrbitDist);
        Vector3 finalOrbitPosition = processedTargetPos + offset;
        player.playerCamera.transform.position = finalOrbitPosition;

        RaycastHit hit;
        Vector3 startPoint = processedTargetPos;
        Vector3 endPoint = finalOrbitPosition;
        
        float sphereRadius = 0.5f;                                  // Determine how big the sphere of the cast will be.
        Vector3 direction = (endPoint - startPoint).normalized;     // Direction of the cast.
        float distance = Vector3.Distance(startPoint, endPoint);    // How far do we want to cast the sphere? (Distance between target and camera).

        //Detect collision!
        if (Physics.SphereCast(startPoint, sphereRadius, direction, out hit, distance))
        {
            // If we simply use hit.normal, it will be the contact point, not the center of the sphere.
            // So we need to move the hit.point back to the sphere surface using the direction of the hit with the lenght of the sphere radius.
            player.playerCamera.transform.position = hit.point + (hit.normal * sphereRadius);

            // Draw collision on scene view if collides (Debug).
            Debug.Log($"Hit : {Vector3.Distance(startPoint, hit.point)}");
            Debug.DrawRay(startPoint, direction * distance, Color.blue);    // Main cast direction
            Debug.DrawLine(startPoint, hit.point, Color.red);               // Show the actual collision
        }
        else
        {
            // Draw collision on scene view if does not collide (Debug).
            Debug.DrawRay(startPoint, direction * distance, Color.green);
        }

        // Make the camera look at the target.
        player.playerCamera.transform.LookAt(processedTargetPos);

        // We calculate the sideways ratio only after the player has updated it's rotation.
        Quaternion playerRawRotation = player.walkingState.playerRelRot;    // Get the player's raw rotation.
        sidewaysRatio = base.CalculateSidewaysRatio(playerRawRotation, player.moveValue.magnitude, sidewaysRatioMultiplier);
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
        float mouseStep = 1f;

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
}