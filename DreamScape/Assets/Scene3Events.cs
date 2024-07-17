using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using UnityEngine.UI;

public class Scene3Events : MonoBehaviour
{
    // Dialogue
    public DialogueRunner dialogueRunner; // dialogue runner
    public GameObject[] objectsicons; // array of the icons of the objects, these icons appear on the right side of the screen
    private int objectCount; // variable to keep track of how many icons are visible (this means how many objects the player has found in the room)
    private bool[] hasBeenClicked; // array to keep track if an object has been clicked

    public List<Sprite> images; // Array to hold all images from returningHome
    public Image displayImage; // The UI element where the images will be displayed
    public GameObject displayImageObj; // Temporary to turn the gameobject active

    void Start()
    {
        // Initialize the hasBeenClicked array with the same length as objectsicons
        hasBeenClicked = new bool[objectsicons.Length];
    }

    //----------------------------------SHOW/HIDE IMAGES----------------------------------------
    [YarnCommand("transition_Scene4Images")]
    public void Transition_Scene4Images()
    {
        // Coroutine for the transaction from Scene3 to Scene4.
        StartCoroutine(DisplayImagesForTransition());
    }

    //----------------------------------ACTIONS FROM CLICKS----------------------------------------
    public void TableObject()
    {
        if (!hasBeenClicked[0])
        {
            // stop the dialogue running
            dialogueRunner.Stop();
            // Start new dialogue
            dialogueRunner.StartDialogue("tableObject");
            // show the first object of the array objectsicons
            objectsicons[0].SetActive(true);
            // add one to the object Count
            objectCount++;
            hasBeenClicked[0] = true; // mark this object as clicked
            Debug.Log(objectCount);

            // if the object count is >= 3, it will call the dialogue from the Scene3_EN.yarn and start the transition to the next scene.
            if (objectCount >= 3)
            {
                Debug.Log("OBJECT COUNT IS 3");
                // stop the dialogue running
                dialogueRunner.Stop();
                // Start new dialogue
                dialogueRunner.StartDialogue("transitionStart");
            }
        }
    }

    public void CabinetObject()
    {
        if (!hasBeenClicked[1])
        {
            // stop the dialogue running
            dialogueRunner.Stop();
            // Start new dialogue
            dialogueRunner.StartDialogue("cabinetObject");
            // show the second object of the array objectsicons
            objectsicons[1].SetActive(true);
            // add one to the object count
            objectCount++;
            hasBeenClicked[1] = true; // mark this object as clicked
            Debug.Log(objectCount);

            // if the object count is >= 3, it will call the dialogue from the Scene3_EN.yarn and start the transition to the next scene.
            if (objectCount >= 3)
            {
                Debug.Log("OBJECT COUNT IS 3");
                // stop the dialogue running
                dialogueRunner.Stop();
                // Start new dialogue
                dialogueRunner.StartDialogue("transitionStart");
            }
        }
    }

    public void BedObject()
    {
        if (!hasBeenClicked[2])
        {
            // stop the dialogue running
            dialogueRunner.Stop();
            // Start new dialogue
            dialogueRunner.StartDialogue("bedObject");
            // show the third object of the array objectsicons
            objectsicons[2].SetActive(true);
            // add one to the object count
            objectCount++;
            hasBeenClicked[2] = true; // mark this object as clicked
            Debug.Log(objectCount);

            // if the object count is >= 3, it will call the dialogue from the Scene3_EN.yarn and start the transition to the next scene.
            if (objectCount >= 3)
            {
                Debug.Log("OBJECT COUNT IS 3");
                // stop the dialogue running
                dialogueRunner.Stop();
                // Start new dialogue
                dialogueRunner.StartDialogue("transitionStart");
            }
        }
    }

    //----------------------------------COROUTINES----------------------------------------

    private IEnumerator DisplayImagesForTransition()
    {
        // Turn the gameobject displayimage active true
        displayImageObj.SetActive(true);
        Debug.Log("Entrou na Coroutine");
        foreach (Sprite image in images) // For each image, display one at a time with the interval of 3 seconds
        {
            Debug.Log("Sprite" + image);
            displayImage.sprite = image; // Set the current image
            yield return new WaitForSeconds(3f); // Wait for 3s
        }
        // Change scene after finishing showing all the images
        Debug.Log("Mudança de scene");
        SceneManager.LoadScene("Scene4");
    }
}

