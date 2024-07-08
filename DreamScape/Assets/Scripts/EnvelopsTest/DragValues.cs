using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class DragValues : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private bool isDragging = false;
    // Flag to track if the text is in an envelope
    private bool isInEnvelope = false;
    // Static variable to count the number of texts placed in envelopes
    private static int textsInEnvelopes = 0;
    // List to track interactability state of each text
    private static List<bool> isInteractableList = new List<bool>();
    //dialogue runner
    public DialogueRunner dialogueRunner;

    //GameObject to disapear after the 10 values 

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;

        // Initialize isInteractableList based on the number of draggable texts
        if (isInteractableList.Count == 0)
        {
            isInteractableList.Clear();
            // Add your initialization logic here for each draggable text
            // For example, assuming you have 10 draggable texts:
            for (int i = 0; i < 10; i++)
            {
                isInteractableList.Add(true); // Initially all texts are interactable
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInteractableList[GetIndex()])
        {
            return; // If not interactable, return
        }

        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (!IsDroppedOnValidDropZone(eventData))
        {
            ResetPosition();
        }
        else
        {
            // Check if the text was previously in an envelope
            if (!isInEnvelope)
            {
                textsInEnvelopes++;
                isInEnvelope = true; // Set isInEnvelope flag to true if dropped on valid drop zone
            }
        }

        // Check if the text was dropped back to the original position
        if (IsReturnedToOriginalPosition())
        {
            if (isInEnvelope)
            {
                textsInEnvelopes--;
                isInEnvelope = false; // Set isInEnvelope flag to false if returned to original position
            }
        }

        if (textsInEnvelopes == 10)
        {
            // Perform your action here
            Debug.Log("All texts are in envelopes!");
        }

        // Update interactability based on textsInEnvelopes count
        UpdateInteractivity();

        // Log the current count of texts in envelopes
        Debug.Log("Texts in envelopes: " + textsInEnvelopes);
    }

    private bool IsDroppedOnValidDropZone(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<DropZone>() != null)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    private bool IsReturnedToOriginalPosition()
    {
        // Calculate the distance between current position and original position
        float distance = Vector2.Distance(rectTransform.anchoredPosition, originalPosition);
        // Return true if distance is less than 10 units (arbitrary threshold)
        return distance < 10f;
    }

    private void UpdateInteractivity()
    {
        // Loop through all draggable texts and disable interaction if all are in envelopes
        for (int i = 0; i < isInteractableList.Count; i++)
        {
            isInteractableList[i] = textsInEnvelopes < 10; // Enable interaction if textsInEnvelopes < 10
        }
    }

    // Helper method to get index of the current draggable text
    private int GetIndex()
    {
        //retrieve the position index of a gameobject within its parents list of children (siblings).
        return transform.GetSiblingIndex();
    }
}
