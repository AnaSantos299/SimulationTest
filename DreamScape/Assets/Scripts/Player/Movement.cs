using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public FixedJoystick joystick;
    private float moveSpeed = 3;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Make sure the object doesn't rotate due to physics
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + transform.TransformDirection(movement));
    }
}
