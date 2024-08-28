using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float gravity = 10f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
            // Get input from the player
            float moveDirectionY = moveDirection.y;
            moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += transform.forward * walkSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection -= transform.forward * walkSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection -= transform.right * walkSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += transform.right * walkSpeed;
            }

            // Apply gravity
            moveDirection.y = moveDirectionY;
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the character
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }