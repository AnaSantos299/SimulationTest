using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 75f;
    public Transform playerBody; // Reference to the player's transform (assign in Unity Editor)
    public float dragThreshold = 2f; // Threshold to start dragging

    private float rotationX = 0f; // Current rotation around the X-axis
    private bool isDragging = false;
    private Vector3 initialMousePosition;

    void Update()
    {
        // Check if the right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            initialMousePosition = Input.mousePosition;
        }

        // Check if the right mouse button is released
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Check if the right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            // Calculate the distance moved from the initial position
            float distance = (Input.mousePosition - initialMousePosition).magnitude;

            // Start dragging if the distance is greater than the threshold
            if (distance > dragThreshold)
            {
                isDragging = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // Only rotate the camera if the user is dragging
        if (isDragging)
        {
            // Input for mouse movement
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the player horizontally (Y-axis)
            playerBody.Rotate(Vector3.up * mouseX);

            // Calculate vertical rotation
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Clamp to prevent camera flipping

            // Rotate the camera vertically (X-axis)
            transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }
}