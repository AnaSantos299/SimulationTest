using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene2Events : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public GameObject phone; //Image from the phone
    public GameObject phoneOpenMessage; //Image from the phone showing the messages
    public GameObject phoneApps; //Image showing the apps on the phone
    public GameObject phoneBank; //Image showing the bank account

    public GameObject returnHomeButton; //Button to return home

    public List<Sprite> images; // Array to hold all images from returningHome
    public Image displayImage; //The UI element where the images will be displayed

    //Show the initial Phone image
    [YarnCommand("makePhoneVisible")]
    public void MakePhoneVisible()
    {

        phone.SetActive(true);
    }

    //Show PhoneOpenMessage Image and Hide initial phone image
    [YarnCommand("makePhoneOpenMessageVisible")]
    public void MakePhoneOpenMessageVisible()
    {
        phone.SetActive(false);
        phoneOpenMessage.SetActive(true);
    }

    //Show PhoneApps and Hide PhoneOpenMessage Image
    [YarnCommand("makePhoneAppsVisible")]
    public void MakePhoneAppsVisible()
    {
        phoneOpenMessage.SetActive(false);
        phoneApps.SetActive(true);
    }

    //----------------------------------BUTTONS----------------------------------------
    public void OpenBankApp()
    {
        //Stop previous dialogue
        dialogueRunner.Stop();
        //Start OpenBankApp Dialogue
        dialogueRunner.StartDialogue("OpenBankApp");
        phoneApps.SetActive(false);
        phoneBank.SetActive(true);

    }
    //
    [YarnCommand("returnHome")]
    public void ReturnHome()
    {
        phoneBank.SetActive(false);
        StartCoroutine(DisplayImages());
    }

    //----------------------------------COROUTINES----------------------------------------

    private IEnumerator DisplayImages()
    {
        returnHomeButton.SetActive(false); //Disactivate the returnHome Button
        dialogueRunner.Stop(); //Stop Dialogue
        Debug.Log("Entrou na Coroutine");
        foreach (Sprite image in images) //For each image, display one at a time with the interval of 3 seconds
        {
            displayImage.sprite = image; // Set the current image
            yield return new WaitForSeconds(3f); // Wait for 3s
        }
        //Chance scene after finishing showing all the images
        Debug.Log("Mudança de scene");
        SceneManager.LoadScene("Scene3");
    }

}