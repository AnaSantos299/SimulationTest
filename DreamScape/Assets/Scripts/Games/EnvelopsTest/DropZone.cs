using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (draggable != null)
        {
            // Set the dropped object as a child of this drop zone and center it
            draggable.transform.SetParent(transform);
            draggable.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Get the TMP_Text component from the child of the draggable object
            TMP_Text valueText = draggable.GetComponentInChildren<TMP_Text>();
        }
    }
}
