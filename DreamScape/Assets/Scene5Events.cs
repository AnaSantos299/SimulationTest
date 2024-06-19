using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using UnityEngine.UI;

public class Scene5Events : MonoBehaviour
{
    //DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;
    //The UI element where the images will be displayed
    public Image displayImage;
    // Array to hold all images from going to the computer
    public List<Sprite> images; 

    //----------------------------------SHOW/HIDE IMAGES----------------------------------------
    [YarnCommand("transitionToScene6")]
    public void TransitionToScene6()
    {
        //start coroutine
        StartCoroutine(DisplayImagesForTransition());
    }

    //----------------------------------COROUTINES----------------------------------------

    private IEnumerator DisplayImagesForTransition()
    {
        Debug.Log("Entrou na Coroutine");
        foreach (Sprite image in images) //For each image, display one at a time with the interval of 3 seconds
        {
            Debug.Log("Sprite" + image);
            displayImage.sprite = image; // Set the current image
            yield return new WaitForSeconds(2f); // Wait for 3s
        }
        //Chance scene after finishing showing all the images
        Debug.Log("Mudança de scene");
        SceneManager.LoadScene("Scene6");
    }

}
