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
    private bool isInEnvelope = false;
    private static int textsInEnvelopes = 0;
    private static List<bool> isInteractableList = new List<bool>();

    public DialogueRunner dialogueRunner;
    public BudgetManager budgetManager;  // Add reference to BudgetManager

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;

        if (isInteractableList.Count == 0)
        {
            isInteractableList.Clear();
            for (int i = 0; i < 10; i++)
            {
                isInteractableList.Add(true);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInteractableList[GetIndex()])
        {
            return;
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
            if (!isInEnvelope)
            {
                textsInEnvelopes++;
                isInEnvelope = true;
            }
        }

        if (IsReturnedToOriginalPosition())
        {
            if (isInEnvelope)
            {
                textsInEnvelopes--;
                isInEnvelope = false;
            }
        }

        // Debug log to track the count
        Debug.Log("Texts in envelopes: " + textsInEnvelopes);

        if (textsInEnvelopes == 10)
        {
            Debug.Log("All texts are in envelopes!");
            budgetManager.StartSimulation();
        }

        UpdateInteractivity();
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
        float distance = Vector2.Distance(rectTransform.anchoredPosition, originalPosition);
        return distance < 10f;
    }

    private void UpdateInteractivity()
    {
        for (int i = 0; i < isInteractableList.Count; i++)
        {
            isInteractableList[i] = textsInEnvelopes < 10;
        }
    }

    private int GetIndex()
    {
        return transform.GetSiblingIndex();
    }
}

