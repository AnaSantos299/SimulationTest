using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 initialPosition;
    private string correctDropSpot; // Identifier of the correct drop spot
    private bool isDragging;
    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        initialPosition = rectTransform.anchoredPosition;
        isDragging = false;

        // Assign the correct drop spot identifier based on the draggable image's name
        string imageName = gameObject.name.ToLower(); // Assuming the image name matches the drop spot name
        correctDropSpot = imageName;

        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        // Check if the dragged image is dropped on the correct drop spot
        if (eventData.pointerEnter != null)
        {
            GameObject dropTarget = eventData.pointerEnter.gameObject;
            if (dropTarget.CompareTag("DropSpot"))
            {
                // Check if the drop target matches the correct drop spot for this image
                if (dropTarget.name.ToLower() == correctDropSpot)
                {
                    // Correct drop, call GameManager to update drop status
                    UnityEngine.Debug.Log("Correct drop! Drop target name: " + dropTarget.name);
                    gameManager.UpdateDropStatus(true);
                    return;
                }
            }
        }

        // Incorrect drop, return the dragged image to its initial position and call GameManager to update drop status
        rectTransform.anchoredPosition = initialPosition;
        UnityEngine.Debug.Log("Incorrect drop, returning to initial position.");
        gameManager.UpdateDropStatus(false);
    }

}

