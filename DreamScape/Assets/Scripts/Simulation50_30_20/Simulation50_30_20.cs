using UnityEngine;
using TMPro;
using Yarn.Unity;

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
    private float allocatedSavings;

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
            if ((allocatedNeeds + allocatedWants + allocatedSavings) < income)
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
        allocatedSavings = float.Parse(savingsInputField.text);
    }

    bool IsAllocationValid()
    {
        // Check if the sum of allocated amounts is within income
        return (allocatedNeeds + allocatedWants + allocatedSavings) == income;
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
                Debug.Log("Regular month");
                NeedsText.text = "Needs: Your car broke down, and the repair cost was 200 euros. ";
                WantsText.text = "Wants: A new gadget you wanted was released and you couldn't resist buying it, which cost 150 euros more than your planned budget.";
                SavingsText.text = "Savings: You managed to save an extra 200 euros by cutting down on dining out.";
                break;
            case 1:
                Debug.Log("Extra savings");
                NeedsText.text = "Needs: This month, your electricity bill was higher than expected due to a heatwave, costing you an extra 50 euros. ";
                WantsText.text = "Wants: You found a great deal on a concert ticket, but it was still 80 euros more than you had planned for your wants.";
                SavingsText.text = "Savings: You received a small bonus at work of 100 euros, which you decided to add to your savings.";
                break;
            case 2:
                Debug.Log("Emergency expense");
                NeedsText.text = "Needs: You had a medical emergency, and the out-of-pocket expenses were 500 euros. ";
                WantsText.text = "Wants: You planned a spontaneous weekend getaway, which cost you 300 euros more than your wants budget. ";
                SavingsText.text = "Savings: Your savings account earned higher-than-expected interest, adding 250 euros to your savings this month.";
                break;
        }
    }

    void ShowResults()
    {
        // Display the allocation and simulation results
        resultsText.text = $"Income: {income}\n" +
                           $"Allocated - Needs: {allocatedNeeds}, Wants: {allocatedWants}, Savings: {allocatedSavings}";
        BGResults.SetActive(true);
    }
}
