using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Scene4Events : MonoBehaviour
{
    public PuzzleLogic puzzlelogic;
    //list of essential choices
    public List<string> essentialList = new List<string>();
    //list of non-essential choices
    public List<string> nonEssentialList = new List<string>();
    //essential and non-essential buttons
    public GameObject BNon_Essential;
    public GameObject BEssential;
    //Count the times the Essential and non-essential buttons have been clicked.
    private int ClickCount = 0;
    //DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;

    //--------------------------------ESSENTIAL AND NON-ESSENTIAL BUTTON LOGIC--------------------------------
    public void Essential()
    {
        //Stop the dialogue active
        dialogueRunner.Stop();
        //add the word shoes to the essential list
        essentialList.Add("shoes");
        Debug.Log("Add to essential List");
        if (puzzlelogic != null)
        {
            puzzlelogic.StartCoroutine(puzzlelogic.WaitShuffle(0.5f));
            Debug.Log("The shuffle was done!");
            BEssential.SetActive(false);
            BNon_Essential.SetActive(false);
            ClickCount++;

            if (ClickCount >= 1)
            {
                //Stop previous dialogue
                dialogueRunner.Stop();
                //Start Scene5Transaction Dialogue
                dialogueRunner.StartDialogue("Scene5Transaction");
            }
        }
        else
        {
            Debug.LogError("GameManager reference not set in ButtonScript!");
        }
    }
    //Non-essential button
    public void Non_essential()
    {
        //Stop the dialogue active
        dialogueRunner.Stop();
        //Add the shoes to the non-essential list
        nonEssentialList.Add("shoes");
        Debug.Log("Add to Non-essential List");

        if (puzzlelogic != null)
        {
            puzzlelogic.StartCoroutine(puzzlelogic.WaitShuffle(0.5f));
            Debug.Log("The shuffle was done!");
            BEssential.SetActive(false);
            BNon_Essential.SetActive(false);
            ClickCount++;

            if (ClickCount >= 1)
            {
                //Stop previous dialogue
                dialogueRunner.Stop();
                //Start Scene5Transaction Dialogue
                dialogueRunner.StartDialogue("Scene5Transaction");
            }
        }
        else
        {
            Debug.LogError("GameManager reference not set in ButtonScript!");
        }
    }
    //--------------------------------Transition Scene 5--------------------------------
    [YarnCommand("changeToScene5")]
    public void TransitionToScene5()
    {
        SceneManager.LoadScene("Scene5");
    }
}
