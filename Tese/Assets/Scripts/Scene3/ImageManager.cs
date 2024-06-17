using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ImageManager : MonoBehaviour
{
    public GameObject[] imagesToCheck; // Array to hold images to check visibility
    public GameObject[] additionalImagesToShow; // Array to hold additional images to display
    public string nextSceneName; // Name of the scene to load after showing the additional images

    void Start()
    {
        // Start checking for visible images
        StartCoroutine(CheckImagesVisibility());
    }

    IEnumerator CheckImagesVisibility()
    {
        while (true)
        {
            // Check if all the images are visible
            if (AreAllImagesVisible())
            {
                // Show additional images
                yield return StartCoroutine(ShowAdditionalImagesWithDelay());

                // Wait x seconds after showing the final image
                yield return new WaitForSeconds(3f);

                // Change scene
                SceneManager.LoadScene(nextSceneName);
                break; // Exit the loop
            }
            yield return null; // Wait for the next frame
        }
    }

    bool AreAllImagesVisible()
    {
        foreach (GameObject image in imagesToCheck)
        {
            if (!image.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator ShowAdditionalImagesWithDelay()
    {
        yield return new WaitForSeconds(3f); // Wait for x seconds before showing the first image

        foreach (GameObject additionalImage in additionalImagesToShow)
        {
            additionalImage.SetActive(true); // Show the additional image
            yield return new WaitForSeconds(3f); // Wait for x seconds before showing the next image
        }
    }
}
