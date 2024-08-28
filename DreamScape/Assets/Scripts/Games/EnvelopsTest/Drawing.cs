using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;

    [SerializeField] private float minDistance = 0.1f; // Minimum distance to add a new point
    [SerializeField] private RectTransform canvasRectTransform; // Reference to the Canvas RectTransform
    [SerializeField] private List<Vector2> screenKeyPoints; // Key points in screen space to be set in the Inspector

    private List<Vector3> keyPoints; // Key points converted to world space
    private int currentPointIndex = 0;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;

        // Example screen space key points for an envelope shape
        screenKeyPoints = new List<Vector2>
        {
            new Vector2(-296, -191),  // Bottom-left corner
            new Vector2(292, -191),   // Bottom-right corner
            new Vector2(291, 189),    // Top-right corner
            new Vector2(-294, 185),   // Top-left corner
        };

        // Initialize the keyPoints list
        keyPoints = new List<Vector3>();

        // Convert screen space key points to world space
        foreach (var screenPoint in screenKeyPoints)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, Camera.main.nearClipPlane));
            worldPoint.z = 0f; // Ensure the z-coordinate is appropriate for the LineRenderer's layer
            keyPoints.Add(worldPoint);
        }

        // Debug: Print key points in world space
        Debug.Log("Key Points in World Space:");
        foreach (var point in keyPoints)
        {
            Debug.Log(point);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) // Check if the left mouse button is pressed
        {
            // Get the current mouse position in world space
            Vector3 mousePosition = Input.mousePosition;
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            currentPosition.z = 0f; // Ensure the z-coordinate matches the LineRenderer's z-plane

            // Debug: Log current positions
            Debug.Log($"Mouse Position: {mousePosition}, World Position: {currentPosition}, Canvas Position: {WorldToCanvasPosition(currentPosition)}");

            if (Vector3.Distance(currentPosition, previousPosition) > minDistance) // Check distance threshold
            {
                if (currentPointIndex < keyPoints.Count && Vector3.Distance(currentPosition, keyPoints[currentPointIndex]) < minDistance) // Check proximity to the current key point
                {
                    // Add the point to the LineRenderer
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPosition);
                    currentPointIndex++;

                    // Debug: Print when a point is added to LineRenderer
                    Debug.Log($"Added point to LineRenderer. Position Count: {line.positionCount}, Canvas Position: {WorldToCanvasPosition(currentPosition)}");

                    // Stop drawing when all key points are hit
                    if (currentPointIndex >= keyPoints.Count)
                    {
                        enabled = false; // Disable script after completing the drawing

                        // Debug: Print when drawing is complete
                        Debug.Log("Drawing completed");
                    }
                }

                previousPosition = currentPosition;
            }
        }
    }

    // Helper method to convert world position to canvas position
    private Vector2 WorldToCanvasPosition(Vector3 worldPosition)
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        return new Vector2(
            (viewportPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f),
            (viewportPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)
        );
    }

    // Optional: Visualize key points in the Scene view for debugging
    private void OnDrawGizmos()
    {
        if (keyPoints != null)
        {
            Gizmos.color = Color.red;
            foreach (var point in keyPoints)
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
