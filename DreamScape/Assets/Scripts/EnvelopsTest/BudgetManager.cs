using UnityEngine;
using TMPro;

public class BudgetManager : MonoBehaviour
{
    //inputfield for the user budgeting
    public TMP_InputField budgetInputField; 
    //array containing the values that will be displayed
    public TMP_Text[] valueTexts; 

    // Dritibute the user budget into x random values, in this case 6
    public void DistributeBudget()
    {
        // Parse the input field text into a float (totalBudget)
        if (float.TryParse(budgetInputField.text, out float totalBudget))
        {
            float[] values = GenerateRandomValues(totalBudget, 6); //generate 6 random values

            for (int i = 0; i < values.Length; i++)
            {
                //Display each value in the array to the texts
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
        float[] randomValues = new float[numberOfValues - 1];

        // Generate random values within the range [0, totalBudget]
        for (int i = 0; i < randomValues.Length; i++)
        {
            randomValues[i] = Random.Range(0, totalBudget);
        }

        // Sort the random values
        System.Array.Sort(randomValues);

        // Calculate the values based on the sorted random points
        for (int i = 0; i < numberOfValues; i++)
        {
            if (i == 0)
            {
                values[i] = randomValues[i]; // First value from 0 to the first value
            }
            else if (i == numberOfValues - 1)
            {
                values[i] = totalBudget - randomValues[i - 1]; // Last value from the last value to the total budget
            }
            else
            {
                values[i] = randomValues[i] - randomValues[i - 1]; // Intermediate values
            }
        }

        return values;
    }
}


