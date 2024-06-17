using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Scene7Subtitles : MonoBehaviour
{
    public List<Sprite> images; // List to hold the images
    public Image displayImage;   // UI Image component to display the images
    public float displayTime = 2f; // Time each image will be displayed
    public TextMeshProUGUI Subtitles;
    public GameObject BG;

    // Start is called before the first frame update
    void Start()
    {
        if (images.Count > 0 && displayImage != null)
        {
            StartCoroutine(SlideshowCoroutine());
            StartCoroutine(Scene5Subtitles());
        }
        else
        {
            Debug.LogError("Images list is empty or displayImage is not assigned.");
        }

    }
    private IEnumerator SlideshowCoroutine()
    {
        for (int i = 0; i < images.Count; i++)
        {
            displayImage.sprite = images[i]; // Set the current image
            yield return new WaitForSeconds(displayTime); // Wait for the displayTime
        }
        SceneManager.LoadScene("Scene8");
    }

    private IEnumerator Scene5Subtitles()
    {
        Subtitles.text = ("UGH...I will get some rest, and tomorrow i will try to do a budgeting.");
        yield return new WaitForSeconds(4);
        BG.SetActive(false);
        Subtitles.SetText("");
    }

}

