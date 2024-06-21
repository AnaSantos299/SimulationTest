using UnityEngine;
using TMPro;
using Yarn.Unity;

public class Simulation50_30_20 : MonoBehaviour
{
    //inputfields
    public TMP_InputField incomeInputField;
    public TMP_InputField needsInputField;
    public TMP_InputField wantsInputField;
    public TMP_InputField savingsInputField;
    //text for the results
    public TextMeshProUGUI resultsText;
    //black background for the results
    public GameObject BGResults;
    //Button submit choices
    public GameObject SubmitChoicesBT;
    //Simulation text
    public TextMeshProUGUI simulationText;
    // variables to save the information from the input fields
    private float income;
    private float allocatedNeeds;
    private float allocatedWants;
    private float allocatedSavings;

    //DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;

    // Call this method when the player submits their allocation
    public void OnSubmit()
    {
        dialogueRunner.Stop();
        GetPlayerInput();
        if (IsAllocationValid())
        {
            SimulateExpenses();
            ShowResults();
        }
        else
        {
            Debug.LogError("Allocation exceeds income! Please allocate within your income.");
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
        // Check if the sum of allocated amounts exceeds income
        return (allocatedNeeds + allocatedWants + allocatedSavings) <= income;
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
                simulationText.text = "Needs: Your car broke down, and the repair cost was 200 euros. " +
                    "\n Wants: A new gadget you wanted was released and you couldn't resist buying it, which cost 150 euros more than your planned budget." + 
                    "\n Savings: You managed to save an extra 200 euros by cutting down on dining out.";
                break;
            case 1:
                Debug.Log("Extra savings");
                simulationText.text = "Needs: This month, your electricity bill was higher than expected due to a heatwave, costing you an extra 50 euros. " +
                    "\n Wants: You found a great deal on a concert ticket, but it was still 80 euros more than you had planned for your wants." +
                    "\nSavings: You received a small bonus at work of 100 euros, which you decided to add to your savings.";
                break;
            case 2:
                Debug.Log("Emergency expense");
                simulationText.text = "Needs: You had a medical emergency, and the out-of-pocket expenses were 500 euros. " +
                    "\n Wants: You planned a spontaneous weekend getaway, which cost you 300 euros more than your wants budget. " +
                    "\n Savings: Your savings account earned higher-than-expected interest, adding 250 euros to your savings this month.";
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
