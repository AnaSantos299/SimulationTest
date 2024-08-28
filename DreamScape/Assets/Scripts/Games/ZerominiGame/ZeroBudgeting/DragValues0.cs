using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class DragValues0 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private bool isDragging = false;
    private bool isInEnvelope = false;
    private static int textsInEnvelopes = 0;
    private static List<bool> isInteractableList = new List<bool>();

    //reference to dialogue runner
    public DialogueRunner dialogueRunner;
    // reference to the zero rule Manager
    public ZeroRuleManager zeroRuleManager;

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

        if (!IsDroppedOnValidDropZone(rectTransform))
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
        }

        if (zeroRuleManager.remainingBudget <= 0 && AreAllActiveObjectsInDropZones())
        {
            dialogueRunner.Stop();
            zeroRuleManager.EndBudgeting();  // Call EndBudgeting() from ZeroRuleManager
        }
    }

    private bool IsDroppedOnValidDropZone(Transform obj)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = RectTransformUtility.WorldToScreenPoint(null, obj.position)
        };

        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<DropZone>() != null)
            {
                return true;
            }
        }

        return false;
    }

    private bool AreAllActiveObjectsInDropZones()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.activeSelf && !IsDroppedOnValidDropZone(child))
            {
                return false;
            }
        }
        return true;
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

    private int GetIndex()
    {
        return transform.GetSiblingIndex();
    }
}

