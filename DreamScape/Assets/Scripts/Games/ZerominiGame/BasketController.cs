using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float screenWidth = 15.0f; // Width of the game area

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);
        // Clamp position to screen boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth / 2, screenWidth / 2);
        transform.position = newPosition;
    }
}
