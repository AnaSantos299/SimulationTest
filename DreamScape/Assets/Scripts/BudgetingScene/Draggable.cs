using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Cached references
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameManager gameManager;

    // initial position
    private Vector2 initialPosition;
    // correct drop spot
    private string correctDropSpot; 

    void Start()
    {
        // Initialize component references
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

        // Save the initial position of the draggable item
        initialPosition = rectTransform.anchoredPosition;

        // Determine the correct drop spot based on the draggable item's name, Assuming the image name matches the drop spot name
        correctDropSpot = gameObject.name.ToLower();

        // Cache reference to the GameManager
        gameManager = FindObjectOfType<GameManager>();

        // Log warning if critical references are missing
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found in the scene.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Disable raycast blocking during drag
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the draggable item
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Re-enable raycast blocking after drag ends
        canvasGroup.blocksRaycasts = true;

        // Check if dropped on a valid drop spot
        if (eventData.pointerEnter != null)
        {
            GameObject dropTarget = eventData.pointerEnter.gameObject;
            if (dropTarget.CompareTag("DropSpot"))
            {
                // Check if the drop target matches the correct drop spot
                if (dropTarget.name.ToLower() == correctDropSpot)
                {
                    // Correct drop
                    Debug.Log("Correct drop! Drop target name: " + dropTarget.name);
                    gameManager?.UpdateDropStatus(true);

                    // Optionally, disable further interactions (make it non-interactable)
                    SetInteractable(false);

                    return;
                }
            }
        }

        // Incorrect drop: return to initial position
        rectTransform.anchoredPosition = initialPosition;
        Debug.Log("Incorrect drop, returning to initial position.");
        gameManager?.UpdateDropStatus(false);
    }

    // Method to make the object non-interactable
    private void SetInteractable(bool interactable)
    {
        canvasGroup.blocksRaycasts = interactable;
    }
}
