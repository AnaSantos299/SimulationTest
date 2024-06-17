using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;

public class B_Narrative : MonoBehaviour
{
    public GameObject[] images; // Array to hold all image GameObjects
    private int currentIndex = 0; // Index to keep track of the current image being displayed
    public SubtitlesScene2 subtitlesScene2; // Reference to the SubtitlesScene2 script
    private string phoneOpenMessageName = "PhoneOpenMessage"; // Name of the special image
    private GameObject phoneOpenMessage; // Reference to the special image
    private string phoneBankImageName = "PhoneBank"; // Name of the special image
    private GameObject phoneBankImage; // Reference to the special image

    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;

    void Start()
    {
        // Initialize references and setup images
        InitializeImages();

        // Add click event listeners to each image
        AddClickEventListeners();
    }

    void InitializeImages()
    {
        // Find the special images by name
        phoneOpenMessage = GameObject.Find(phoneOpenMessageName);
        phoneBankImage = GameObject.Find(phoneBankImageName);

        // Hide all images except the first one
        for (int i = 1; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
    }

    void AddClickEventListeners()
    {
        for (int i = 0; i < images.Length; i++)
        {
            int index = i; // Capture the current value of i for the listener
            EventTrigger trigger = images[i].AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            entry.callback.AddListener((data) => { OnImageClicked(index); });
            trigger.triggers.Add(entry);
        }
    }

    void OnImageClicked(int index)
    {
        // If the clicked image is the current one to be displayed
        if (index == currentIndex)
        {
            // Check if the image is the special image and if the conditions are met
            if (images[index] == phoneOpenMessage && !subtitlesScene2.isDone || images[index] == phoneBankImage && !subtitlesScene2.levelcompleted)
            {
                return; // Do nothing if conditions are not met
            }

            // Handle the "PhoneBank" image special case
            if (images[index].name == phoneBankImageName)
            {
                DeactivateAllImages();
                StartCoroutine(GoingHomeNarrative());
                return;
            }

            // Show the next image if available
            ShowNextImage();
        }
    }

    void DeactivateAllImages()
    {
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }
    }

    void ShowNextImage()
    {
        if (currentIndex < images.Length - 1)
        {
            images[currentIndex].SetActive(false);
            currentIndex++;
            images[currentIndex].SetActive(true);
        }
    }

    IEnumerator GoingHomeNarrative()
    {
        Image1.SetActive(true);
        yield return new WaitForSeconds(2f);
        Image1.SetActive(false);
        Image2.SetActive(true);
        yield return new WaitForSeconds(2f);
        Image2.SetActive(false);
        Image3.SetActive(true);
        yield return new WaitForSeconds(2f);
        Image3.SetActive(false);
        Image4.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scene3");
    }
}