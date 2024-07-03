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
    private CanvasGroup canvasGroup; // For controlling raycast blocking
    private int originalSiblingIndex; // To store the original sibling index for resetting

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
        canvasGroup = GetComponent<CanvasGroup>();

        // If CanvasGroup is not attached, add it dynamically
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        // Save the original sibling index
        originalSiblingIndex = rectTransform.GetSiblingIndex();

        // Bring the draggable item to the front of its parent
        rectTransform.SetSiblingIndex(rectTransform.parent.childCount - 1);

        // Disable raycast blocking for the draggable item during drag
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the draggable item
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        // Re-enable raycast blocking after drag ends
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        // Reset the draggable item to its original sibling index
        rectTransform.SetSiblingIndex(originalSiblingIndex);

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
