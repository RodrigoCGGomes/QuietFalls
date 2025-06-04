using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Third Person Camera View. All logic and behaviour for the third person camera is here. 
/// Player prefab has a GamePlayer component, GamePlayer component has an instance of CameraStateMachine.cs,
/// and CameraStateMachine.cs creates an instance of this class and calls statemachine.Initialize/ChangeState() in the OnEnable method.
/// </summary>
public class ThirdPersonCamera : CameraState
{
    #region Struct Declarations
    /// <summary> Struct that holds all the settings for the camera. </summary>
    [Serializable] public struct Settings
    {
        /// <summary> Distance from the ground. If set to zero, the camera will be at the player's feet. </summary>
        public float cameraHeight;

        /// <summary> View sensitivity. How fast should the camera orbit? </summary>
        public float rotationSpeed;

        /// <summary> Amount of rotation smoothing effect applied to the camera orbit. </summary>
        public float smoothing;

        /// <summary> Amount of smoothing effect applied to the target position. </summary>
        public float targetSmoothing;

        /// <summary> Speed of zooming in and out while using gamepad. </summary>
        public float orbitZoomSpeed;

        /// <summary> Limit to moving camera down. </summary>
        public float minPitchAngle;

        /// <summary> Limit to moving camera up. </summary>
        public float maxPitchAngle;

        /// <summary> Minimum distance the camera can be from the player. </summary>
        public float minCameraDist;

        /// <summary> Maximum distance the camera can be from the player. </summary>
        public float maxCameraDist;

        /// <summary> Amount of camera assistance. Sideways ratio is how much the camera should tend to rotate to the side when the player is moving. </summary>
        public float sidewaysRatioMultiplier;

        public static Settings Default => new Settings
        {
            cameraHeight = 0.5f,
            rotationSpeed = 240f,
            smoothing = 20f,
            targetSmoothing = 10f,
            orbitZoomSpeed = 8f,
            minPitchAngle = -70f,
            maxPitchAngle = 60f,
            minCameraDist = 4f,
            maxCameraDist = 7f,
            sidewaysRatioMultiplier = 0.08f
        };
    
    }

    /// <summary> Struct that holds the variables that are constantly updated during runtime. </summary>
    [Serializable] public struct CameraVariables
    {
        public float yaw;
        public float pitch;
        public float gamepadCameraZoomDirection;
        public float sidewaysRatio, sidewaysRatioSmooth;
        public float targetOrbitDist, smoothOrbitDist;
        public Vector3 processedTargetPos;
        public Vector2 lookVector, lookVectorSmooth;

        public static CameraVariables Default => new CameraVariables
        {
            targetOrbitDist = 5f
        };
    }

    /// <summary> Struct that holds variables related to input and input processing. </summary>
    [Serializable] public struct InputVariables
    {
        /// <summary> Track if the look stick is actively moving </summary>
        public bool isLookStickActive;

        /// <summary> Track if the move stick is actively moving </summary>
        public bool isMoveStickActive;

        /// <summary> Indicates whether the look input is coming from a mouse or a gamepad. </summary>
        public string currentLookScheme;

        /// <summary> Tracks look values. </summary>
        public Vector2 lookVector;

        /// <summary> Processed version of lookVector. </summary>
        public Vector2 lookVectorSmooth;

        public Vector2 moveVector;

        /// <summary> ... Used to have sidewaysRatio assistance start at zero and slowly pickup speed </summary>
        public float smoothSomething;

    }
    #endregion

    #region Variables 
    private Settings settings = Settings.Default;
    private CameraVariables current = CameraVariables.Default;
    private InputVariables input = new InputVariables();
    #endregion Variables

    #region Constructors
    /// <summary> Creating a new instance via code to use as a state in a state machine is absolutelly correct </summary>
    public ThirdPersonCamera(GamePlayer parGamePlayer) : base(parGamePlayer)
    {
        //Don't really need to do anything here.
    }
    #endregion

    #region Base Class Method Overrides

    public override void EnterState()
    {
        Debug.Log($"CameraState.EnterState(); - {this}");

        //Reset lerpedTargetPos so it starts at target position.
        current.processedTargetPos = target.position;

        player.ResetVariables();
    }

    public override void ExitState()
    {
        Debug.Log($"CameraState.ExitState(); - {this}");
    }

    public override void Tick()
    {
        // Test, might like it
        current.sidewaysRatioSmooth = Mathf.Lerp(current.sidewaysRatioSmooth, current.sidewaysRatio, Time.deltaTime * 5f); 

        current.smoothOrbitDist = Mathf.Lerp(current.smoothOrbitDist, current.targetOrbitDist, Time.deltaTime * 5f);

        // Figure out the ACTUAL target position (Smoothing and raising it)
        current.processedTargetPos = Vector3.Lerp(current.processedTargetPos, target.position + (Vector3.up * settings.cameraHeight), settings.targetSmoothing * Time.deltaTime);

        //Apply the gamepad zoom
        current.targetOrbitDist += current.gamepadCameraZoomDirection * Time.deltaTime * settings.orbitZoomSpeed * -1f;
        current.targetOrbitDist = Mathf.Clamp(current.targetOrbitDist, settings.minCameraDist, settings.maxCameraDist);

        //Process lookVector so it feels smooth
        input.lookVectorSmooth = Vector2.Lerp(input.lookVectorSmooth, input.lookVector, settings.smoothing * Time.deltaTime);

        #region Apply Yaw and Pitch
        //All of this is because mouse delta does not require Time.deltaTime fix but gamepad does...
        float mouseMultiplier, stickMultiplier; 
        switch (input.currentLookScheme)
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
        current.yaw += ((input.lookVectorSmooth.x * stickMultiplier) + (current.sidewaysRatioSmooth)) * settings.rotationSpeed * Time.deltaTime + (input.lookVectorSmooth.x * mouseMultiplier);
        current.pitch -= (input.lookVectorSmooth.y * stickMultiplier * settings.rotationSpeed * Time.deltaTime) + (input.lookVectorSmooth.y * mouseMultiplier);
        current.pitch = Mathf.Clamp(current.pitch, settings.minPitchAngle, settings.maxPitchAngle);

        // Slowly straighten up the pitch while walking.
        if (Mathf.Approximately(input.lookVector.y, 0f))
        {
            float moveMagnitude = player.moveValue.magnitude;
            current.pitch = Mathf.Lerp(current.pitch, 0f, Time.deltaTime * moveMagnitude * 0.4f);
        }
        #endregion

        // Compute camera position in orbit.
        Quaternion orbitDirection = Quaternion.Euler(current.pitch, current.yaw, 0);
        Vector3 desiredOffset = orbitDirection * new Vector3(0, 0, current.smoothOrbitDist * -1f);
        Vector3 desiredCameraPosition = current.processedTargetPos + desiredOffset;
        player.playerCamera.transform.position = desiredCameraPosition;

        RaycastHit hit;
        Vector3 startPoint = current.processedTargetPos;
        Vector3 endPoint = desiredCameraPosition;
        
        float sphereRadius = 0.5f;

        // Check if the would-be unobstructed camera position is obstructed.
        bool isObstructed = Physics.CheckSphere(desiredCameraPosition, sphereRadius);
        if (!isObstructed)
        {
            // No obstruction detected
            player.playerCamera.transform.position = desiredCameraPosition;
        }
        //Debug.LogWarning($"{isObstructed}");

        // Determine how big the sphere of the cast will be.
        Vector3 direction = (endPoint - startPoint).normalized;     // Direction of the cast.
        float distance = Vector3.Distance(startPoint, endPoint);    // How far do we want to cast the sphere? (Distance between target and camera).

        //Detect collision!
        if (Physics.SphereCast(startPoint, sphereRadius, direction, out hit, distance))
        {
            // I am also checking if (isObstructed == true) because if the would-be unobstructed camera position is not inside a collider,
            // then we don't want to move the camera.
            if(isObstructed /*|| hit.collider.gameObject.tag != "terrain"*/)
            {
                // If we simply use hit.normal, it will be the contact point, not the center of the sphere.
                // So we need to move the hit.point back to the sphere surface using the direction of the hit with the lenght of the sphere radius.
                player.playerCamera.transform.position = hit.point + (hit.normal * sphereRadius);
            }

            Debug.DrawRay(startPoint, direction * distance, Color.blue);    // Debug main cast direction
            Debug.DrawLine(startPoint, hit.point, Color.red);               // Debug the actual collision
        }
        else
        {
            Debug.DrawRay(startPoint, direction * distance, Color.green);   // Debug main cast direction
        }

        player.playerCamera.transform.LookAt(current.processedTargetPos);   // Camera should be always be looking at lerped target.


        current.sidewaysRatio = base.CalculateSidewaysRatio(
            player.GetStateMachine().currentState.GetPlayerCameraRelativeRotation(),
            player.moveValue.magnitude, 
            settings.sidewaysRatioMultiplier
            );

        // Debug.Log(player.GetStateMachine().currentState.GetPlayerCameraRelativeRotation().eulerAngles.y);  
    }

    #region Input Relays
    public override void OnMoveRelay(InputAction.CallbackContext context)
    {
        Vector2 newInput = context.ReadValue<Vector2>();
        InputDevice device = context.control.device;

        if (context.performed)
        {
            input.moveVector = newInput;
            input.isMoveStickActive = true; // Track movement

            // Determine input source
            // I am aware that storing the scheme in a string is unreliable, I will change it later. 
            input.currentLookScheme = context.control.device switch
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
            input.moveVector = Vector2.zero;
            input.isMoveStickActive = false;
            current.sidewaysRatioSmooth = 0f;
        }
    }
    public override void OnLookRelay(InputAction.CallbackContext context)
    {
        Vector2 newInput = context.ReadValue<Vector2>();
        InputDevice device = context.control.device;

        if (context.performed)
        {
            input.lookVector = newInput;
            input.isLookStickActive = true; // Track movement

            // Determine input source
            // I am aware that storing the scheme in a string is unreliable, I will change it later. 
            input.currentLookScheme = context.control.device switch
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
            input.lookVector = Vector2.zero;
            input.isLookStickActive = false;
        }
    }
    public override void OnZoomRelay(InputAction.CallbackContext context)
    {
        float mouseStep = 1f;

        // I am aware that storing the scheme in a string is unreliable, I will change it later. 
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
                current.targetOrbitDist = Mathf.Clamp(current.targetOrbitDist + mouseStep * context.ReadValue<float>() * -1f, settings.minCameraDist, settings.maxCameraDist);
            }
        }
        else
        {
            if (context.phase != InputActionPhase.Canceled)
            {
                current.gamepadCameraZoomDirection = context.ReadValue<float>();
            }
            else
            {
                current.gamepadCameraZoomDirection = 0f;
            }
        }
    }
    public override void OnBackRelay(InputAction.CallbackContext context)
    {
        //Debug.Log("Pressed Esc/Back while on ThirdPersonCamera");
        /*if (context.phase == InputActionPhase.Started) 
        {
            var currentSubState = GameStateManager.instance.stateMachine.CurrentSubState;
            currentSubState.SwitchStates(GameStateManager.instance.stateMachine.factory.CutSceneState());
        }*/
        Debug.LogWarning("Here we had a code that changed the GameState to CutSceneState");
    }
    #endregion
    #endregion
}