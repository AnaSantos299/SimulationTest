using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using UnityEngine.UI;

public class Scene3Events : MonoBehaviour
{
    //Dialogue
    public DialogueRunner dialogueRunner; // dialogue runner
    public GameObject[] objectsicons; // array of the icons of the objects, this icons appear on the right side of the screen
    private int objectCount; //variable to keep track of how many icons are visible (this means how many objects the player have found in the room)


    public List<Sprite> images; // Array to hold all images from returningHome
    public Image displayImage; //The UI element where the images will be displayed
    public GameObject displayImageObj; //Temporary to turn the gameobject active

    //----------------------------------SHOW/HIDE IMAGES----------------------------------------
    [YarnCommand("transition_Scene4Images")]
    public void Transition_Scene4Images()
    {
        //coroutine for the transaction from Scene3 to Scene4.
        StartCoroutine(DisplayImagesForTransition());
    }

    //----------------------------------ACTIONS FROM CLICKS----------------------------------------
    public void TableObject()
    {
        //stop the dialogue running
        dialogueRunner.Stop();
        //Start new dialogue
        dialogueRunner.StartDialogue("tableObject");
        //show the first object of the array objectsicons
        objectsicons[0].SetActive(true);
        //add one to the object Count
        objectCount++;
        Debug.Log(objectCount);

        //if the objectcount is >= 3, it will call the dialogue from the Scene3_EN.yarn and start the transition to the next scene.
        if (objectCount >= 3)
        {
            Debug.Log("OBJECT COUNT IS 3");
            //stop the dialogue running
            dialogueRunner.Stop();
            //Start new dialogue
            dialogueRunner.StartDialogue("transitionStart");
        }
    }

    public void CabinetObject()
    {
        //stop the dialogue running
        dialogueRunner.Stop();
        //Start new dialogue
        dialogueRunner.StartDialogue("cabinetObject");
        //show the second object of the array objectsicons
        objectsicons[1].SetActive(true);
        //add one to the object count
        objectCount++;
        Debug.Log(objectCount);

        //if the objectcount is >= 3, it will call the dialogue from the Scene3_EN.yarn and start the transition to the next scene.
        if (objectCount >= 3)
        {
            Debug.Log("OBJECT COUNT IS 3");
            //stop the dialogue running
            dialogueRunner.Stop();
            //Start new dialogue
            dialogueRunner.StartDialogue("transitionStart");
        }
    }

    public void BedObject()
    {
        //stop the dialogue running
        dialogueRunner.Stop();
        //Start new dialogue
        dialogueRunner.StartDialogue("bedObject");
        //show the third object of the array objectsicons
        objectsicons[2].SetActive(true);
        //add one to the object count
        objectCount++;
        Debug.Log(objectCount);

        //if the objectcount is >= 3, it will call the dialogue from the Scene3_EN.yarn and start the transition to the next scene.
        if (objectCount >= 3)
        {
            Debug.Log("OBJECT COUNT IS 3");
            //stop the dialogue running
            dialogueRunner.Stop();
            //Start new dialogue
            dialogueRunner.StartDialogue("transitionStart");
        }
    }

    //----------------------------------COROUTINES----------------------------------------

    private IEnumerator DisplayImagesForTransition()
    {
        //Turn the gameobject displayimage active true
        displayImageObj.SetActive(true);
        Debug.Log("Entrou na Coroutine");
        foreach (Sprite image in images) //For each image, display one at a time with the interval of 3 seconds
        {
            Debug.Log("Sprite" + image);
            displayImage.sprite = image; // Set the current image
            yield return new WaitForSeconds(3f); // Wait for 3s
        }
        //Chance scene after finishing showing all the images
        Debug.Log("Mudança de scene");
        SceneManager.LoadScene("Scene4");
    }

}
