using UnityEngine;
using TMPro;
using Yarn.Unity;

public class BudgetManager : MonoBehaviour
{
    public TMP_InputField budgetInputField;
    public TMP_Text[] valueTexts;
    public GameObject ValuesUI;
    public DialogueRunner dialogueRunner;

    public float minValue = 50.0f;

    // Total budget entered by the user
    private float totalBudget;

    // Simulation text
    public TextMeshProUGUI RentText;
    public TextMeshProUGUI FoodText;
    public TextMeshProUGUI FunText;
    public TextMeshProUGUI SavingsText;
    public TextMeshProUGUI results;
    public GameObject Simualtionresults;

    public void DistributeBudget()
    {
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("valuesAppear");

        if (float.TryParse(budgetInputField.text, out totalBudget))
        {
            ValuesUI.SetActive(true);
            int[] values = GenerateRandomValues(totalBudget, valueTexts.Length);

            for (int i = 0; i < values.Length; i++)
            {
                valueTexts[i].text = values[i].ToString("D"); // Format as whole numbers
            }
        }
        else
        {
            Debug.LogWarning("Invalid budget input. Please enter a valid number.");
        }
    }

    private int[] GenerateRandomValues(float totalBudget, int numberOfValues)
    {
        int[] values = new int[numberOfValues];
        int remainingBudget = Mathf.RoundToInt(totalBudget); // Convert totalBudget to an integer

        for (int i = 0; i < numberOfValues - 1; i++)
        {
            int maxValue = remainingBudget - Mathf.RoundToInt(minValue) * (numberOfValues - 1 - i);
            values[i] = Random.Range(Mathf.RoundToInt(minValue), Mathf.Min(maxValue, remainingBudget));
            remainingBudget -= values[i];
        }

        // Assign the remaining budget to the last value to ensure the sum equals the total budget
        values[numberOfValues - 1] = remainingBudget;

        return values;
    }

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
                rentExpense = 300;
                foodExpense = 150;
                funExpense = 100;
                savings = 50;
                RentText.text = $"Rent: Your rent was,{rentExpense} euros.";
                FoodText.text = $"Food: You spent {foodExpense} euros in food this month.";
                FunText.text = $"Fun: You spend {funExpense} euros in entertainment.";
                SavingsText.text = $"Savings: You managed to save an extra {savings} euros by cutting down on dining out.";
                break;
            case 1:
                rentExpense = 350;
                foodExpense = 100;
                funExpense = 50;
                savings = 100;
                RentText.text = $"Rent: Your rent was,{rentExpense} euros.";
                FoodText.text = $"Food: You spent {foodExpense} euros in food this month.";
                FunText.text = $"Fun: You spend {funExpense} euros in entertainment.";
                SavingsText.text = $"Savings: You managed to save an extra {savings} euros by cutting down on dining out.";
                break;
            case 2:
                rentExpense = 200;
                foodExpense = 200;
                funExpense = 200;
                savings = 150;
                RentText.text = $"Rent: Your rent was,{rentExpense} euros.";
                FoodText.text = $"Food: You spent {foodExpense} euros in food this month.";
                FunText.text = $"Fun: You spend {funExpense} euros in entertainment.";
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
