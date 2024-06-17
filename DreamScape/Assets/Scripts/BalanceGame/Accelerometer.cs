using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    public float speed = 4f;
    public float gravityMultiplier = 9.8f;
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the cube
        rb = GetComponent<Rigidbody>();

        // Disable Unity's built-in gravity
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        // Get the input acceleration
        float inputAccelerationX = Input.acceleration.x;

        // Calculate translation based on the input acceleration
        float translation = inputAccelerationX * speed * Time.deltaTime;

        // Apply translation to move the cube horizontally
        transform.Translate(0, 0, -translation);

        // Apply gravity manually
        Vector3 gravity = new Vector3(0f, -gravityMultiplier, 0f);
        rb.AddForce(gravity, ForceMode.Acceleration);

    }
}