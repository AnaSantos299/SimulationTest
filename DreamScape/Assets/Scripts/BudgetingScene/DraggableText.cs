using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public class DraggableText : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 initialPosition;
    private string correctDropSpot; // Identifier of the correct drop spot
    private bool isDragging;

    private static int correctDropsCount = 0;
    private static int totalImagesCount = 0;

    public ObjectsManager objectsManager;

    //UI After inserting the text in the spots
    [SerializeField] private TextMeshProUGUI UIText;
    [SerializeField] private GameObject bgUIText;

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
                        // All images are in correct places, start coroutine
                        StartCoroutine(AllImagesCorrectCoroutine());
                    }
                    return;
                }
            }
        }

        // Incorrect drop, return the dragged image to its initial position
        rectTransform.anchoredPosition = initialPosition;
        Debug.Log("Incorrect drop, returning to initial position.");
    }

    IEnumerator AllImagesCorrectCoroutine()
    {
        Debug.Log("All images are in correct places!");
        UIText.text = "Take a moment to read the envelops";
        bgUIText.SetActive(true);
        yield return new WaitForSeconds(4f);
        UIText.text = "";
        bgUIText.SetActive(false);
        yield return new WaitForSeconds(4f);
        objectsManager.HideObjects();
        objectsManager.ShowObjects();

        yield return null;
    }
}

