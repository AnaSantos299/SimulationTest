using UnityEngine;

public enum ItemType
{
    Essential,
    NonEssential
}

public class FallingItem : MonoBehaviour
{
    public ItemType itemType;
    public float scoreValue;
    public float fallSpeed = 5.0f;

    void Update()
    {
        // Make the item fall down
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Destroy the item if it goes off-screen
        if (transform.position.y < -6f) // Adjust based on your screen size
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Basket"))
        {
            if (itemType == ItemType.Essential)
            {
                // Increase score for catching an essential item
                ScoreManager.Instance.AddScore(scoreValue);
            }
            else
            {
                // Decrease score or apply penalty for catching a non-essential item
                ScoreManager.Instance.SubtractScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}
