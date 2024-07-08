using UnityEngine;
using TMPro;
using System.Linq;
using Yarn.Unity;

public class BudgetManager : MonoBehaviour
{
    // Input field for the user budgeting
    public TMP_InputField budgetInputField;
    // Array containing the text components that will display values
    public TMP_Text[] valueTexts;

    public GameObject ValuesUI;

    // DialogueRunner for the Yarn dialogue system
    public DialogueRunner dialogueRunner;

    // Minimum value for each generated number
    public float minValue = 50.0f;

    // Distribute the user budget into x random values
    public void DistributeBudget()
    {
        // Stop any ongoing dialogue
        dialogueRunner.Stop();
        // Start new dialogue
        dialogueRunner.StartDialogue("valuesAppear");

        // Parse the input field text into a float (totalBudget)
        if (float.TryParse(budgetInputField.text, out float totalBudget))
        {
            ValuesUI.SetActive(true);
            float[] values = GenerateRandomValues(totalBudget, valueTexts.Length); // Generate random values

            for (int i = 0; i < values.Length; i++)
            {
                // Display each value in the array to the texts
                valueTexts[i].text = values[i].ToString("F2"); // Format the value to 2 decimal places
            }
        }
        else
        {
            Debug.LogWarning("Invalid budget input. Please enter a valid number.");
        }
    }

    // Function to generate x random values that sum up to totalBudget
    private float[] GenerateRandomValues(float totalBudget, int numberOfValues)
    {
        float[] values = new float[numberOfValues];
        float remainingBudget = totalBudget;

        for (int i = 0; i < numberOfValues - 1; i++)
        {
            float maxValue = remainingBudget - minValue * (numberOfValues - 1 - i);
            values[i] = Random.Range(minValue, Mathf.Min(maxValue, totalBudget));
            remainingBudget -= values[i];
        }

        values[numberOfValues - 1] = remainingBudget;

        // Shuffle the array to randomize values' order
        ShuffleArray(values);

        return values;
    }

    // Helper function to shuffle an array
    private void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
