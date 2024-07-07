using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Reference to the player's transform (assign in Unity Editor)

    private float rotationX = 0f; // Current rotation around the X-axis

    void Start()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
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
