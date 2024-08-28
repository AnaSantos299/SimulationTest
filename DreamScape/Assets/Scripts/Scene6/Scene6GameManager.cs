using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Scene6GameManager : MonoBehaviour
{
    // Variable to track the current page/index
    private int browserPage = 0;

    // Back arrow button reference
    [SerializeField] private GameObject backArrow;

    // The image component to change
    public Image targetImage;
    public string SceneName;

    // List of sprites to browse through
    public List<Sprite> imageSprites;


    void Start()
    {
        // Initialize the first image
        UpdateImage();
        // Back arrow should be inactive on the first image
        backArrow.SetActive(false);
    }

    public void FowardArrow()
    {
        Debug.Log("Forward Arrow Clicked");

        // Check if we're at the last image
        if (browserPage >= imageSprites.Count - 1)
        {
            // If it's the last image, load the next scene
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            // Otherwise, move to the next image
            browserPage++;
            UpdateImage();
            backArrow.SetActive(true); // Enable the back arrow as we're not on the first image
        }
    }

    public void BackArrow()
    {
        Debug.Log("Back Arrow Clicked");

        // Move to the previous image if not the first one
        if (browserPage > 0)
        {
            browserPage--;
            UpdateImage();
        }

        // Back arrow should be inactive if we are at the first image
        backArrow.SetActive(browserPage > 0);
    }

    private void UpdateImage()
    {
        // Update the image to the current sprite in the list
        if (browserPage >= 0 && browserPage < imageSprites.Count)
        {
            targetImage.sprite = imageSprites[browserPage];
        }
    }
}

