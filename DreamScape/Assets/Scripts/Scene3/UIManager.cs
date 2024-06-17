using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] objects; // Array to hold all the object GameObjects
    public GameObject[] additionalImages; // Array to hold all additional images to display
    public GameObject[] objectsImages; // Array to hold images that will stay visible
    private bool[] isImageShown; // Array to keep track of whether each additional image is shown

    void Start()
    {
        // Initialize isImageShown array
        isImageShown = new bool[objects.Length];
        for (int i = 0; i < isImageShown.Length; i++)
        {
            isImageShown[i] = false;
        }
    }

    void Update()
    {
        // Handle mouse input
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }

        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                HandleInput(touch.position);
            }
        }
    }

    private void HandleInput(Vector3 inputPosition)
    {
        // Perform a raycast from the input position
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Find the index of the hit object
            int index = System.Array.IndexOf(objects, hit.transform.gameObject);
            if (index != -1 && !isImageShown[index])
            {
                // Show the additional image
                objectsImages[index].SetActive(true);
                additionalImages[index].SetActive(true);
                isImageShown[index] = true;
            }
        }
    }

    public void HideAdditionalImages()
    {
        for (int i = 0; i < additionalImages.Length; i++)
        {
            if (additionalImages[i].activeSelf)
            {
                additionalImages[i].SetActive(false);
                isImageShown[i] = false;
            }
        }
    }
}
