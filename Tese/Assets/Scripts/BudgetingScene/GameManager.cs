using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_InputField incomeInputField; // Assign in the inspector
    public TextMeshProUGUI needsText; // Assign in the inspector
    public TextMeshProUGUI wantsText; // Assign in the inspector
    public TextMeshProUGUI savingsText; // Assign in the inspector

    public GameObject needsBG; // Assign in the inspector
    public GameObject wantsBG; // Assign in the inspector
    public GameObject savingsBG; // Assign in the inspector

    public GameObject SubmitButton; // Assign in the inspector

    private int correctDropCount = 0; // Counter to keep track of correct drops
    private int totalDropCount; // Total number of drop spots

    public TextMeshProUGUI subtitlesScene8;
    public GameObject BG;

    void Start()
    {
        // Count the total number of drop spots
        totalDropCount = GameObject.FindGameObjectsWithTag("DropSpot").Length;
        StartCoroutine(Scene8Subtitles());
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
            // All correct drops, display input field and text fields
            incomeInputField.gameObject.SetActive(true);
            needsBG.gameObject.SetActive(true);
            wantsBG.gameObject.SetActive(true);
            savingsBG.gameObject.SetActive(true);
            SubmitButton.gameObject.SetActive(true);

        }
        else
        {
            // Not all correct drops, hide input field and text fields
            incomeInputField.gameObject.SetActive(false);
            needsBG.gameObject.SetActive(false);
            wantsBG.gameObject.SetActive(false);
            savingsBG.gameObject.SetActive(false);
            SubmitButton.gameObject.SetActive(false);
        }
    }

    public void CalculateBudgetDistribution()
    {
        if (float.TryParse(incomeInputField.text, out float income))
        {
            float needsAmount = income * 0.50f;
            float wantsAmount = income * 0.30f;
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

    private IEnumerator Scene8Subtitles()
    {
        subtitlesScene8.text = ("Ah...What is this? Again?");
        yield return new WaitForSeconds(4);
        subtitlesScene8.text = ("Am I going crazy?");
        yield return new WaitForSeconds(4);
        subtitlesScene8.text = ("Oh, this is about what i was searching...let me try connecting the things by dragging them.");
        yield return new WaitForSeconds(6);
        BG.SetActive(false);
        subtitlesScene8.SetText("");
    }
}


