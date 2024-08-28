using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZeroRuleManager : MonoBehaviour
{
    // Input field for user input
    public TMP_InputField entryInputField;
    // Submit button
    public Button submitButton;
    // List of GameObjects that contain TMP_Text components
    public List<GameObject> valueUIObjects;
    // List of TMP_Text components to display the allocated values
    private List<TMP_Text> valueTexts;

    // Text to display the remaining budget
    public TMP_Text remainingBudgetText;

    // Initialize total budget
    private float totalBudget = 1000f;
    private float remainingBudget;
    private int currentUIIndex = 0;

    // Simulation text
    public TextMeshProUGUI RentText;
    public TextMeshProUGUI FoodText;
    public TextMeshProUGUI FunText;
    public TextMeshProUGUI SavingsText;
    public TextMeshProUGUI results;
    public GameObject Simualtionresults;
    void Start()
    {
        // Initialize valueTexts list from valueUIObjects
        valueTexts = new List<TMP_Text>();
        foreach (var uiObject in valueUIObjects)
        {
            TMP_Text textComponent = uiObject.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                valueTexts.Add(textComponent);
                // Initially set all UI objects inactive
                uiObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"No TMP_Text component found in children of {uiObject.name}");
            }
        }

        // Ensure that the number of valueUIObjects matches the number of valueTexts
        if (valueUIObjects.Count != valueTexts.Count)
        {
            Debug.LogError("Mismatch between valueUIObjects and valueTexts counts.");
        }

        // Setup button click listener
        submitButton.onClick.AddListener(OnSubmitButtonClicked);

        // Start budgeting
        StartBudgeting();
    }

    // Start budgeting process
    public void StartBudgeting()
    {
        // Initialize remaining budget
        remainingBudget = totalBudget;
        UpdateRemainingBudgetText();
    }

    // Handle button click to submit input
    private void OnSubmitButtonClicked()
    {
        // Retrieve input value
        string input = entryInputField.text;

        // Clear the input field after submission
        entryInputField.text = "";

        // Check if the input is valid and parse it
        if (float.TryParse(input, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float entryAmount))
        {
            if (entryAmount > 0 && entryAmount <= remainingBudget)
            {
                if (currentUIIndex < valueUIObjects.Count)
                {
                    GameObject currentUIObject = valueUIObjects[currentUIIndex];
                    TMP_Text currentTextField = valueTexts[currentUIIndex];

                    if (currentTextField != null)
                    {
                        currentTextField.text = entryAmount.ToString("F2"); // Set the exact input value
                        currentUIObject.SetActive(true); // Activate the UI object
                    }
                    else
                    {
                        Debug.LogWarning("No TMP_Text component found for the current UI object.");
                    }

                    remainingBudget -= entryAmount;
                    UpdateRemainingBudgetText();

                    currentUIIndex++; // Move to the next UI object

                    if (remainingBudget <= 0)
                    {
                        EndBudgeting(); // End budgeting if budget is fully allocated
                    }
                }
                else
                {
                    Debug.LogWarning("No more available UI objects to allocate values.");
                    EndBudgeting(); // Optionally handle the case where there are no more UI objects
                }
            }
            else
            {
                Debug.LogWarning("Invalid entry amount. Ensure it's within the remaining budget.");
            }
        }
        else
        {
            Debug.LogWarning($"Invalid entry input: '{input}'. Ensure it's a valid number.");
        }
    }

    // Update remaining budget display
    private void UpdateRemainingBudgetText()
    {
        if (remainingBudgetText != null)
        {
            remainingBudgetText.text = $"Remaining Budget: {remainingBudget:F2}";
        }
    }

    // End the budgeting process
    private void EndBudgeting()
    {
        Debug.Log("Budget has been fully allocated.");
        entryInputField.interactable = false;
        SimulateExpenses();
    }

    public void StartSimulation()
    {
        SimulateExpenses();
    }

    void SimulateExpenses()
    {
        // Define expenses for Rent, Food, Fun, and Savings
        float rentExpense = 0, foodExpense = 0, funExpense = 0, savings = 0;

        // Randomize the scenario
        int scenario = Random.Range(0, 3);

        switch (scenario)
        {
            case 0:
                rentExpense = 500;
                foodExpense = 200;
                funExpense = 200;
                savings = 50;
                RentText.text = $"Housing/Utilities: Your rent and utilities were,{rentExpense} euros.";
                FoodText.text = $"Food: You spent {foodExpense} euros in food this month.";
                FunText.text = $"Entertainment: You spend {funExpense} euros in entertainment.";
                SavingsText.text = $"Savings: You managed to save an extra {savings} euros by cutting down on cloths.";
                break;
            case 1:
                rentExpense = 350;
                foodExpense = 100;
                funExpense = 50;
                savings = 100;
                RentText.text = $"Housing/Utilities: Your rent and utilities were,{rentExpense} euros.";
                FoodText.text = $"Food: You spent {foodExpense} euros in food this month.";
                FunText.text = $"Entertainment: You spend {funExpense} euros in entertainment.";
                SavingsText.text = $"Savings: You managed to save an extra {savings} euros by cutting down on dining out.";
                break;
            case 2:
                rentExpense = 300;
                foodExpense = 200;
                funExpense = 200;
                savings = 150;
                RentText.text = $"Housing/Utilities: Your rent and utilities were,{rentExpense} euros.";
                FoodText.text = $"Food: You spent {foodExpense} euros in food this month.";
                FunText.text = $"Entertainment: You spend {funExpense} euros in entertainment.";
                SavingsText.text = $"Savings: You managed to save an extra {savings} euros by cutting down on dining out.";
                break;
        }

        // Calculate the final amount left after expenses
        float finalAmount = totalBudget - rentExpense - foodExpense - funExpense + savings;

        // Show the results
        ShowResults(rentExpense, foodExpense, funExpense, savings, finalAmount);
    }

    void ShowResults(float rentExpense, float foodExpense, float funExpense, float savings, float finalAmount)
    {
        // Assuming you have a UI Text or TMP_Text field to show the results
        results.text = $"Income: {totalBudget} €\n" +
                       $"Final amount of savings after paying all the expenses: {finalAmount} €\n";

        Simualtionresults.SetActive(true);
    }
}
