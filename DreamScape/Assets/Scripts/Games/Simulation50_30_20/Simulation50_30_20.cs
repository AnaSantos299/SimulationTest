using UnityEngine;
using UnityEngine.SceneManagement; // Add this to handle scene management
using TMPro;
using Yarn.Unity;
using System.Collections; // Add this to use coroutines

public class Simulation50_30_20 : MonoBehaviour
{
    // Input fields
    public TMP_InputField incomeInputField;
    public TMP_InputField needsInputField;
    public TMP_InputField wantsInputField;
    public TMP_InputField savingsInputField;

    // Text for the results
    public TextMeshProUGUI resultsText;

    // Black background for the results
    public GameObject BGResults;

    // Button submit choices
    public GameObject SubmitChoicesBT;

    // Simulation text
    public TextMeshProUGUI NeedsText;
    public TextMeshProUGUI WantsText;
    public TextMeshProUGUI SavingsText;

    // Variables to save the information from the input fields
    private float income;
    private float allocatedNeeds;
    private float allocatedWants;
    private float initialSavings; // Changed to initialSavings to distinguish from adjustedSavings

    // Variables for simulation
    private float needsExpense;
    private float wantsExpense;
    private float adjustedSavings;

    // DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;

    // Call this method when the player submits their allocation
    public void OnSubmit()
    {
        dialogueRunner.Stop();

        // Check if any input field is empty
        if (IsAnyInputEmpty())
        {
            dialogueRunner.StartDialogue("emptyTextFields");
            Debug.LogError("Please fill in all input fields.");
            return;
        }

        GetPlayerInput();

        if (IsAllocationValid())
        {
            SimulateExpenses();
            ShowResults();
        }
        else
        {
            // Check if the allocation is less than the income
            if ((allocatedNeeds + allocatedWants + initialSavings) < income)
            {
                dialogueRunner.StartDialogue("belowIncome");
                Debug.LogError("Allocation is less than income! Please allocate the full income.");
            }
            else
            {
                dialogueRunner.StartDialogue("exceedIncome");
                Debug.LogError("Allocation exceeds income! Please allocate within your income.");
            }
        }
    }

    void GetPlayerInput()
    {
        // Convert player input from strings to floats
        income = float.Parse(incomeInputField.text);
        allocatedNeeds = float.Parse(needsInputField.text);
        allocatedWants = float.Parse(wantsInputField.text);
        initialSavings = float.Parse(savingsInputField.text);
    }

    bool IsAllocationValid()
    {
        // Check if the sum of allocated amounts is within income
        return (allocatedNeeds + allocatedWants + initialSavings) == income;
    }

    bool IsAnyInputEmpty()
    {
        // Check if any of the input fields are empty
        return string.IsNullOrEmpty(incomeInputField.text) ||
               string.IsNullOrEmpty(needsInputField.text) ||
               string.IsNullOrEmpty(wantsInputField.text) ||
               string.IsNullOrEmpty(savingsInputField.text);
    }

    void SimulateExpenses()
    {
        SubmitChoicesBT.SetActive(false);

        // Example scenarios with random expense values
        int scenario = Random.Range(0, 3);
        switch (scenario)
        {
            case 0:
                needsExpense = 200;
                wantsExpense = 150;
                adjustedSavings = 200; // Bonus or additional savings
                NeedsText.text = $"Needs: Your car broke down, and the repair cost was {needsExpense} euros.";
                WantsText.text = $"Wants: A new gadget you wanted was released and you couldn't resist buying it, which cost {wantsExpense} euros more than your planned budget.";
                SavingsText.text = $"Savings: You managed to save an extra {adjustedSavings} euros by cutting down on dining out.";
                break;
            case 1:
                needsExpense = 50;
                wantsExpense = 80;
                adjustedSavings = 100; // Bonus or additional savings
                NeedsText.text = $"Needs: This month, your electricity bill was higher than expected due to a heatwave, costing you an extra {needsExpense} euros.";
                WantsText.text = $"Wants: You found a great deal on a concert ticket, but it was still {wantsExpense} euros more than you had planned for your wants.";
                SavingsText.text = $"Savings: You received a small bonus at work of {adjustedSavings} euros, which you decided to add to your savings.";
                break;
            case 2:
                needsExpense = 500;
                wantsExpense = 300;
                adjustedSavings = 250; // Bonus or additional savings
                NeedsText.text = $"Needs: You had a medical emergency, and the out-of-pocket expenses were {needsExpense} euros.";
                WantsText.text = $"Wants: You planned a spontaneous weekend getaway, which cost you {wantsExpense} euros more than your wants budget.";
                SavingsText.text = $"Savings: Your savings account earned higher-than-expected interest, adding {adjustedSavings} euros to your savings this month.";
                break;
        }

        // Calculate the total remaining money
        float finalAmount = income - needsExpense - wantsExpense + adjustedSavings;

        // Update total savings with the final amount
        float previousTotalSavings = PlayerPrefs.GetFloat("TotalSavings", 0);
        float newTotalSavings = previousTotalSavings + finalAmount;

        // Save the new total savings to PlayerPrefs
        PlayerPrefs.SetFloat("TotalSavings", newTotalSavings);
        PlayerPrefs.Save();

        // Show the results
        ShowResults();
    }

    void ShowResults()
    {
        // Display the user's initial allocation choices
        resultsText.text = $"Income: {income}\n" +
                           $"Needs Allocation: {allocatedNeeds}\n" +
                           $"Wants Allocation: {allocatedWants}\n" +
                           $"Savings Allocation: {initialSavings}\n\n";

        // Calculate and display results after expenses
        float finalAmount = income - needsExpense - wantsExpense + adjustedSavings;
        float totalSavings = PlayerPrefs.GetFloat("TotalSavings", 0);

        resultsText.text += $"Results after simulation:\n" +
                            $"Total Savings after expenses: {totalSavings}";

        // Show the results background
        BGResults.SetActive(true);
    }
    private void OnApplicationQuit()
    {
        // Reset total savings when the application quits
        Debug.Log("Application is quitting. Resetting total savings.");
        PlayerPrefs.SetFloat("TotalSavings", 0);
        PlayerPrefs.Save();
    }
}
