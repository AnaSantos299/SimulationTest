using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // tries to get the DragValues script from the object being dragged
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        //if the object has the script DragValues
        if (draggable != null)
        {
            // Set the dropped object as a child of this drop zone and center it
            draggable.transform.SetParent(transform);
            draggable.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}

