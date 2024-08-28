using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class Scene3Events : MonoBehaviour
{
    // Dialogue
    public DialogueRunner dialogueRunner;
    private int objectCount = 0; // Initialize object count
    private bool[] hasBeenClicked = new bool[5];

    public List<Sprite> images;
    public Image displayImage;
    public GameObject displayImageObj;

    // Objects Bar
    public Image ObjetcBar;
    public List<Sprite> barSprites; // List to hold the sprites for the object bar
    public Sprite defaultSprite; // Default sprite to show when no objects are collected

    public CameraRotation cameraRotation;
    public PlayerMovement playerMovement;

    //variable to check if all the objects were collected
    public bool allObjectsCollected;

    //scene name
    public string SceneName;


    private void Start()
    {
        // Set the object bar sprite to the default sprite
        ObjetcBar.sprite = defaultSprite;
        cameraRotation.enabled = false;
        playerMovement.enabled = false;
    }

    // Yarn Commands
    [YarnCommand("enablePlayerMovement")]
    public void EnablePlayerMovement()
    {
        dialogueRunner.Stop();
        cameraRotation.enabled = true;
        playerMovement.enabled = true;
    }

    public void DisablePlayerMovement()
    {
        cameraRotation.enabled = false;
        playerMovement.enabled = false;
    }

    // Scene Objects
    public void TableObject()
    {
        if (!hasBeenClicked[0])
        {
            FindObjectOfType<SoundManager>().Play("Correct");
            DisablePlayerMovement();
            CollectObject(0, "tableObject");
        }
    }

    public void CabinetObject()
    {
        if (!hasBeenClicked[1])
        {
            FindObjectOfType<SoundManager>().Play("Correct");
            DisablePlayerMovement();
            CollectObject(1, "cabinetObject");
        }
    }

    public void BedObject()
    {
        if (!hasBeenClicked[2])
        {
            FindObjectOfType<SoundManager>().Play("Correct");
            DisablePlayerMovement();
            CollectObject(2, "bedObject");
        }
    }

    public void GuitarObject()
    {
        if (!hasBeenClicked[3])
        {
            FindObjectOfType<SoundManager>().Play("Correct");
            DisablePlayerMovement();
            CollectObject(3, "guitarObject");
        }
    }
    public void DragonObject()
    {
        if (!hasBeenClicked[4])
        {
            FindObjectOfType<SoundManager>().Play("Correct");
            DisablePlayerMovement();
            CollectObject(4, "dragonObject");
        }
    }
    public void RandomObject()
    {
        DisablePlayerMovement();
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("randomObject");
        Debug.Log("RandomObject Found");
    }

    public void ObjectAlreadyFound()
    {
        DisablePlayerMovement();
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("objectAlreadyFound");
        Debug.Log("Object found for the second time");
    }

    // Actions to be made after finding an object
    private void CollectObject(int index, string dialogueName)
    {
        // Mark the object as clicked
        hasBeenClicked[index] = true;

        // Stop the current dialogue and start a new one
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue(dialogueName);

        // Increase the object count
        objectCount++;
        Debug.Log(objectCount);

        // Change the object bar sprite
        UpdateObjectBarSprite();

        // If the object count reaches 5, trigger the transition dialogue
        if (objectCount >= 5)
        {
            Debug.Log("OBJECT COUNT IS 5");
            allObjectsCollected = true;
            dialogueRunner.Stop();
            dialogueRunner.StartDialogue("transitionStart");

        }
    }
    // Update barSprite
    private void UpdateObjectBarSprite()
    {
        if (objectCount > 0 && objectCount <= barSprites.Count)
        {
            ObjetcBar.sprite = barSprites[objectCount - 1];
        }
        else
        {
            // If no objects collected, show the default sprite
            ObjetcBar.sprite = defaultSprite;
        }
    }

    // Coroutine to display images of the transition
    public IEnumerator DisplayImagesForTransition()
    {
        displayImageObj.SetActive(true);
        Debug.Log("Starting Coroutine");

        foreach (Sprite image in images)
        {
            Debug.Log("Sprite: " + image);
            displayImage.sprite = image;
            yield return new WaitForSeconds(3f);
        }

        Debug.Log("Changing scene");
        SceneManager.LoadScene(SceneName);
    }

}
