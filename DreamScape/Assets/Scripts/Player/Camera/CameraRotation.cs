using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float dragThreshold = 2f;

    private float rotationX = 0f;
    private bool isDragging = false;
    private Vector3 initialMousePosition;

    void Update()
    {
        // Start dragging
        if (Input.GetMouseButtonDown(1))
        {
            initialMousePosition = Input.mousePosition;
            isDragging = false; // reset drag state
        }

        // Holding right mouse button
        if (Input.GetMouseButton(1))
        {
            float distance = (Input.mousePosition - initialMousePosition).magnitude;

            if (!isDragging && distance > dragThreshold)
            {
                isDragging = true;
                LockCursor();
            }

            if (isDragging)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                playerBody.Rotate(Vector3.up * mouseX);
                rotationX -= mouseY;
                rotationX = Mathf.Clamp(rotationX, -90f, 90f);
                transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            }
        }

        // Stop dragging
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
            UnlockCursor(); // always unlock and show cursor
        }

        //If mouse button is NOT pressed but cursor is still locked, unlock it
        if (!Input.GetMouseButton(1) && Cursor.lockState != CursorLockMode.None)
        {
            isDragging = false;
            UnlockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
