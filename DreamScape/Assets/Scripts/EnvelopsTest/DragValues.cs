using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragValues : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    //original position of the values
    private Vector2 originalPosition;
    //variable to check if the value is being dragged
    private bool isDragging = false;

    private void Awake()
    {
        // Get the RectTransform component of the GameObject this script is attached to
        rectTransform = GetComponent<RectTransform>();
        // Get the Canvas component from the parent GameObject
        canvas = GetComponentInParent<Canvas>();
        // Store the starting position of the RectTransform
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // The value is being dragged
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Adjust the position of the RectTransform based on the drag delta, scaled by the canvas scale factor
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //reset the dragging to false
        isDragging = false;

        // Check if the draggable value was dropped in a DropZone
        if (!IsDroppedOnValidDropZone(eventData))
        {
            // if not it will reset the position of the value
            ResetPosition();
        }
    }
    // Check if the object was dropped on a valid drop zone
    private bool IsDroppedOnValidDropZone(PointerEventData eventData)
    {
        // Perform a raycast to determine if the pointer is over a valid drop zone
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        // Iterate through all raycast results
        foreach (var result in results)
        {
            // Check if the raycast hit a valid drop zone (envelops)
            if (result.gameObject.GetComponent<DropZone>() != null)
            {
                return true;
            }
        }

        return false;
    }

    //reset the value to the original position
    private void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }
}