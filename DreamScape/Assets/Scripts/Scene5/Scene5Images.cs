using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Scene5Images : MonoBehaviour
{
    public List<Sprite> images; // List to hold the images
    public Image displayImage;   // UI Image component to display the images
    public float displayTime = 4f; // Time each image will be displayed
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
        SceneManager.LoadScene("Scene6");
    }

    private IEnumerator Scene5Subtitles()
    {
        Subtitles.text = ("UGH...What was that?");
        yield return new WaitForSeconds(2);
        Subtitles.text = ("A dream? I spent a lot of money on those things... ");
        yield return new WaitForSeconds(4);
        Subtitles.text = ("I need to figure out a plan to manage my finances better.");
        yield return new WaitForSeconds(4);
        Subtitles.text = ("Let me go to the computer and look for something...");
        yield return new WaitForSeconds(2);
        BG.SetActive(false);
        Subtitles.SetText("");
    }

}


