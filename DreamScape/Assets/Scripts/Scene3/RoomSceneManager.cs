using UnityEngine;
using TMPro;

public class RoomSceneManager : MonoBehaviour
{
    // Text for displaying total savings
    public TextMeshProUGUI totalSavingsText;

    void Start()
    {
        // Load total savings from PlayerPrefs
        float totalSavings = PlayerPrefs.GetFloat("TotalSavings", 0);

        // Display total savings in the UI
        if (totalSavingsText != null)
        {
            totalSavingsText.text = $"Total Savings: {totalSavings} €";
        }
    }

    // Optionally reset total savings at the start of the room scene
    public void ResetTotalSavings()
    {
        PlayerPrefs.SetFloat("TotalSavings", 0);
        PlayerPrefs.Save();
        Debug.Log("Total Savings has been reset.");
    }
}

