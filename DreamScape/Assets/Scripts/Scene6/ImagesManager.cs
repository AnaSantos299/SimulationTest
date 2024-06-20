using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class ImagesManager : MonoBehaviour
{
    public List<Sprite> images; // List to hold the images
    public Image displayImage;  // UI Image component to display the images
    public Button nextButton;   // UI Button to navigate through images
    public string nextSceneName; // Name of the scene to switch to

    //DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;

    private int currentIndex = 0;

    private void Start()
    {
        if (images.Count > 0 && displayImage != null && nextButton != null)
        {
            displayImage.sprite = images[currentIndex]; // Set the first image
            nextButton.onClick.AddListener(OnNextButtonClick); // Add listener to button
        }
        else
        {
            Debug.LogError("Images list, displayImage, or nextButton is not assigned.");
        }
    }

    private void OnNextButtonClick()
    {
        currentIndex++; // Move to the next image

        if (currentIndex < images.Count)
        {
            displayImage.sprite = images[currentIndex]; // Set the current image
        }
        else
        {
            //Stop previous dialogue
            dialogueRunner.Stop();
            //Start Scene5Transaction Dialogue
            dialogueRunner.StartDialogue("Scene6_EN");
        }
    }
    [YarnCommand("changeToScene7")]
    public void ChangeToScene7()
    {
            SceneManager.LoadScene("Scene7");
    }
}
