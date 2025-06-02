using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private Vector2 mouseInput;

    void Start()
    {
        // Lock cursor to center of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Called by the Input System when mouse movement is detected
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        // Apply mouse sensitivity and frame-rate independence
        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (looking up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation

        // Get current rotation for horizontal (yaw) adjustment
        float yRotation = transform.eulerAngles.y + mouseX;

        // Apply both rotations to the camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}