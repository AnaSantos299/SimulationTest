using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Transform PlayerBody;
    public FixedJoystick joystick;
    public Vector2 sensitivity = new Vector2(40f, 40f);
    private float currentRotationX = 0f;

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Calculate rotation amount based on joystick input
        float rotationX = horizontalInput * sensitivity.x * Time.deltaTime;
        float rotationY = verticalInput * sensitivity.y * Time.deltaTime;

        // Rotate the player body horizontally
        PlayerBody.Rotate(Vector3.up * rotationX);

        // Update the current rotation for vertical rotation
        currentRotationX -= rotationY;
        currentRotationX = Mathf.Clamp(currentRotationX, -45f, 45f);

        // Rotate the camera vertically (assuming the camera is a child of PlayerBody)
        transform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
    }
}
