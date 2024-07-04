using System.Collections;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    //input field of the income
    public TMP_InputField incomeInputField;
    //text for the needs
    public TextMeshProUGUI needsText;
    //text for the wants
    public TextMeshProUGUI wantsText;
    //text for the savings
    public TextMeshProUGUI savingsText;
    //button to subtim the income
    public GameObject SubmitButton;
    //list to keep the game objects of the values, make it cleaner to activate and deactivate them
    public GameObject[] TextDragAndDrops;



    private int correctDropCount = 0; // Counter to keep track of correct drops
    private int totalDropCount; // Total number of drop spots

    //DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;

    void Start()
    {
        // Count the total number of drop spots
        totalDropCount = GameObject.FindGameObjectsWithTag("DropSpot").Length;
    }

    public void UpdateDropStatus(bool isCorrectDrop)
    {
        if (isCorrectDrop)
        {
            correctDropCount++;
        }

        Debug.Log("Correct Drop Count: " + correctDropCount);
        Debug.Log("Total Drop Count: " + totalDropCount);

        // Check if all draggable images are in the correct spots
        if (correctDropCount == totalDropCount)
        {
            //Stop previous dialogue
            dialogueRunner.Stop();
            //Start Dialogue
            dialogueRunner.StartDialogue("incomeDialogue");
            // All correct drops, display input field and text fields
            incomeInputField.gameObject.SetActive(true);
            SubmitButton.gameObject.SetActive(true);
        }
    }
    //caculate the distrubition of the income given by the player
    public void CalculateBudgetDistribution()
    {
        //set all the objects of the array setactive true
        foreach (GameObject obj in TextDragAndDrops)
        {
            obj.SetActive(true);
        }

        //parse the text of the inputfield to the income variable
        if (float.TryParse(incomeInputField.text, out float income))
        {
            //stop previous dialogue
            dialogueRunner.Stop();
            //Start new dialogue
            dialogueRunner.StartDialogue("confirmIncome");
            //sets 50% of the income to the needs
            float needsAmount = income * 0.50f;
            //sets 30% of the income to the wants
            float wantsAmount = income * 0.30f;
            //sets 20% of the income to the savings
            float savingsAmount = income * 0.20f;

            // Display calculated values
            needsText.text = needsAmount.ToString("F2");
            wantsText.text = wantsAmount.ToString("F2");
            savingsText.text = savingsAmount.ToString("F2");
        }
        else
        {
            Debug.LogError("Invalid input for income.");
        }
    }
}