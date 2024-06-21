using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using Yarn.Unity;

public class DraggableText : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    //initial position of the text
    private Vector2 initialPosition;
    private string correctDropSpot; // Identifier of the correct drop spot
    private bool isDragging;
    //count of the drops to check when all the texts are in the correct drop
    private static int correctDropsCount = 0;
    //total of images
    private static int totalImagesCount = 0;
    //has access to the script objects manager, where there is a list showing and hiding objects
    public ObjectsManager objectsManager;

    //DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        initialPosition = rectTransform.anchoredPosition;
        isDragging = false;

        // Assign the correct drop spot identifier based on the draggable image's name
        string imageName = gameObject.name.ToLower(); // Assuming the image name matches the drop spot name
        correctDropSpot = imageName;

        // Increment the total images count
        totalImagesCount++;
    }
    //when dragging the texts
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }
    //Dragging logic
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
            if (dropTarget.CompareTag("DropSpotT"))
            {
                // Check if the drop target matches the correct drop spot for this image
                if (dropTarget.name.ToLower() == correctDropSpot)
                {
                    // Correct drop, increment the correct drops count
                    Debug.Log("Correct drop!");
                    correctDropsCount++;
                    Debug.Log(correctDropsCount);
                    Debug.Log(correctDropSpot);

                    // Check if all images are in correct places
                    if (correctDropsCount == totalImagesCount)
                    {
                        dialogueRunner.Stop();
                        dialogueRunner.StartDialogue("fillBlanks");
                        // All images are in correct places, start coroutine
                        Debug.Log("All images are in correct places!");
                        objectsManager.HideObjects();
                        objectsManager.ShowObjects();
                    }
                    return;
                }
            }
        }

        // Incorrect drop, return the dragged image to its initial position
        rectTransform.anchoredPosition = initialPosition;
        Debug.Log("Incorrect drop, returning to initial position.");
    }
}